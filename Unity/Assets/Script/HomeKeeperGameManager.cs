#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-25 오후 11:20:10
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;

/// <summary>
/// 
/// </summary>
public class HomeKeeperGameManager : CSingletonDynamicMonoBase<HomeKeeperGameManager>
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    public enum EGameState
    {
        Start,
        Victory,
        Fail,
        Pause,
        Resume
    }

    /* public - Field declaration            */

    public CObserverSubject<EGameState> p_Event_OnGameState { get; private set; } = new CObserverSubject<EGameState>();
    public CObserverSubject<GameObject> p_Event_OnAction { get; private set; } = new CObserverSubject<GameObject>();
    public CObserverSubject<int, int> p_Event_OnChangeJewel { get; private set; } = new CObserverSubject<int, int>();

    [GetComponentInChildren]
    public Camera p_pInGameCamera { get; private set; }
    [GetComponentInChildren]
    public CTweenPosition_Radial p_pRadialPosition_Button { get; private set; }

    public AudioClip p_pAudioClip_BGM = null;

    /* protected & private - Field declaration         */

    List<CharacterModel> _listCharacterModel = new List<CharacterModel>();

    [GetComponentInChildren("CurrentTarget")]
    Transform _pTransform_CurrentTarget = null;

    PlayerInput _pPlayerInput;
    CManagerTimeScale _pManagerTimeScale;

    int _iJewelCount_Total;
    int _iJewelCount_Current;
    bool isPaused;
    float timescaleBeforePaused;

    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    public void DoLose_Jewel(Jewel pJewel)
    {
        if (pJewel.p_bIsStolen)
            return;

        pJewel.DoSet_Stolen(true);
        if (--_iJewelCount_Current == 0)
            DoGame_Fail();

        p_Event_OnChangeJewel.DoNotify(_iJewelCount_Total, _iJewelCount_Current);
    }

    public void DoGame_Start()
    {
        StartCoroutine(CoGameStart());
    }

    public void DoGame_Victory()
    {
        p_Event_OnGameState.DoNotify(EGameState.Victory);
    }

    public void DoGame_Pause()
    {
        timescaleBeforePaused = _pManagerTimeScale.p_fCurrentTimeScale;
        _pManagerTimeScale.DoSetTimeScale(0);
        p_Event_OnGameState.DoNotify(EGameState.Pause);
    }

    public void DoGame_Resume()
    {
        _pManagerTimeScale.DoSetTimeScale(timescaleBeforePaused);
        p_Event_OnGameState.DoNotify(EGameState.Resume);
    }

    public void DoGame_Fail()
    {
        p_Event_OnGameState.DoNotify(EGameState.Fail);
    }

    public void Event_OnAction(GameObject pObject)
    {
        if (pObject != null)
        {
            p_Event_OnAction.DoNotify(pObject);

            _pTransform_CurrentTarget.SetActive(true);
            _pTransform_CurrentTarget.SetParent(pObject.transform);
            _pTransform_CurrentTarget.DoResetTransform();

            _pTransform_CurrentTarget.GetComponent<CTweenRotation>()?.DoPlayTween_Forward();

            _pPlayerInput.p_pCharacterModel.DoStartAI();
        }
    }

    public void Event_OnActionFinish()
    {
        _pTransform_CurrentTarget?.SetActive(false);
    }

    // ========================================================================== //

    /* protected - Override & Unity API         */

    protected override void OnAwake()
    {
        base.OnAwake();

        _pManagerTimeScale = CManagerTimeScale.instance;
        _pTransform_CurrentTarget?.SetActive(false);

        if (UIManager.instance == null)
        {
            GameObject.Instantiate(Resources.Load("Prefab/UIRoot"));
        }
    }

    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        DoGame_Start();
    }

    protected override IEnumerator OnEnableObjectCoroutine()
    {
        yield return null;

        _pPlayerInput = FindObjectOfType<PlayerInput>();
        _pPlayerInput.p_pCharacterMovement.p_Event_OnMovePlayer.Subscribe += P_Event_OnMovePlayer_Subscribe;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (Input.GetKeyDown(KeyCode.Escape))
            if (!isPaused) {
                DoGame_Pause();
                isPaused = true;
            } else {
                DoGame_Resume();
                isPaused = false;
            }
        
        

        if (CheckDebugFilter(EDebugFilter.Debug_Level_App) == false)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            DoGame_Start();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            DoGame_Victory();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            DoGame_Fail();
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    IEnumerator CoGameStart()
    {
        yield return null;

        _pPlayerInput = FindObjectOfType<PlayerInput>();
        Vector3 vecPlayerPos = _pPlayerInput.transform.position;
        vecPlayerPos.y = 10f;

        CFollowObject pObjectFollow = FindObjectOfType<CFollowObject>();
        if (pObjectFollow != null)
        {
            pObjectFollow.transform.position = vecPlayerPos;
            pObjectFollow.DoInitTarget(_pPlayerInput.transform);
            pObjectFollow.DoSetFollow(true);
        }

        CharacterModel pCharacterModel = _pPlayerInput.p_pCharacterModel;
        pCharacterModel.EventOnAwake();
        pCharacterModel.pStat.p_Event_OnChangeStatus.Subscribe += P_Event_OnChangeStatus_Subscribe_Player;

        AstarPath.active?.Scan();
        p_Event_OnGameState.DoNotify(EGameState.Start);

        Spawner_Jewel[] arrJewelSpawn = FindObjectsOfType<Spawner_Jewel>();
        _iJewelCount_Total = 0;
        for (int i = 0; i < arrJewelSpawn.Length; i++)
        {
            if (arrJewelSpawn[i].gameObject != null && arrJewelSpawn[i].gameObject.activeSelf)
                _iJewelCount_Total++;
        }

        _iJewelCount_Current = _iJewelCount_Total;
        p_Event_OnChangeJewel.DoNotify(_iJewelCount_Total, _iJewelCount_Current);

        PlayBGM();

        _listCharacterModel.Clear();
        Spawner_Character[] arrSpawnCharater = FindObjectsOfType<Spawner_Character>();
        for (int i = 0; i < arrSpawnCharater.Length; i++)
        {
            Spawner_Character pSpawnCharacter = arrSpawnCharater[i];
            if (pSpawnCharacter.p_eCharacterType == Spawner_Character.ECharacterType.Player)
                continue;

            pSpawnCharacter.p_pCharacter.pStat.p_Event_OnChangeStatus.Subscribe += P_Event_OnChangeStatus_Subscribe_Enemy;
            _listCharacterModel.Add(pSpawnCharacter.p_pCharacter);
        }
    }

    private void PlayBGM()
    {
        CManagerSound.instance.DoPlayBGM(p_pAudioClip_BGM.name, PlayBGM);
    }

    private void P_Event_OnChangeStatus_Subscribe_Enemy(Stats pStat)
    {
        if(pStat.iHP <= 0)
        {
            Debug.Log("죽었다");
            pStat.p_pCharacterModel_Owner.SetActive(false);
            //pStat.p_pCharacterModel_Owner.p_pSprite_Renderer_CharacterModel.sprite = pStat.p_pCharacterModel_Owner.p_pSprite_OnDead;
            //pStat.p_pCharacterModel_Owner.enabled = false;

            //var pAIPath = pStat.p_pCharacterModel_Owner.GetComponent<AIPath>();
            //if (pAIPath)
            //    pAIPath.enabled = false;

            //pStat.p_pCharacterModel_Owner.p_pSprite_Renderer_CharacterModel.enabled = false;
        }

        bool bIsClear = false;
        for(int i = 0; i < _listCharacterModel.Count; i++)
        {
            if (_listCharacterModel[i].pStat.iHP > 0)
            {
                bIsClear = false;
                break;
            }
        }

        if (bIsClear)
            DoGame_Victory();
    }

    private void P_Event_OnChangeStatus_Subscribe_Player(Stats sStatus)
    {
        if(sStatus.iHP <= 0)
        {
            DoGame_Fail();
        }
    }

    private void P_Event_OnMovePlayer_Subscribe(bool bMovement, Vector3 vecMove)
    {
        if (isPaused) return;
        float fCurrentTimeScale = _pManagerTimeScale.p_fCurrentTimeScale;
        if (bMovement)
            _pManagerTimeScale.DoSetTimeScale(Mathf.Clamp(fCurrentTimeScale + Time.unscaledDeltaTime, 0.1f, 1f));
        else
            _pManagerTimeScale.DoSetTimeScale(Mathf.Clamp(fCurrentTimeScale - Time.unscaledDeltaTime, 0.1f, 1f));
    }

    #endregion Private
}