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
    }

    /* public - Field declaration            */

    public CObserverSubject<EGameState> p_Event_OnGameState { get; private set; } = new CObserverSubject<EGameState>();
    public CObserverSubject<GameObject> p_Event_OnAction { get; private set; } = new CObserverSubject<GameObject>();

    [GetComponentInChildren]
    public CTweenPosition_Radial p_pRadialPosition_Button { get; private set; }

    /* protected & private - Field declaration         */

    [GetComponentInChildren("CurrentTarget")]
    Transform _pTransform_CurrentTarget = null;

    CManagerTimeScale _pManagerTimeScale;
    bool _bIsWaitAction;
    int _iJewelCount_Total;
    int _iJewelCount_Current;

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
    }

    public void DoGame_Start()
    {
        StartCoroutine(CoGameStart());
    }

    public void DoGame_Victory()
    {
        p_Event_OnGameState.DoNotify(EGameState.Victory);
    }

    public void DoGame_Fail()
    {
        p_Event_OnGameState.DoNotify(EGameState.Fail);
    }

    public void DoRecoveryTime()
    {
        _pManagerTimeScale.DoSetTimeScale_Fade(1f, 1f);
    }

    public void Event_OnClickPlayerButton()
    {
        Debug.Log("Event_OnClickPlayerButton");

        _bIsWaitAction = true;
    }

    public void Event_OnAction(GameObject pObject)
    {
        if (_bIsWaitAction && pObject != null)
        {
            _bIsWaitAction = false;
            p_Event_OnAction.DoNotify(pObject);

            _pTransform_CurrentTarget.SetActive(true);
            _pTransform_CurrentTarget.SetParent(pObject.transform);
            _pTransform_CurrentTarget.DoResetTransform();

            _pTransform_CurrentTarget.GetComponent<CTweenRotation>()?.DoPlayTween_Forward();
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

        p_pRadialPosition_Button.SetActive(false);
    }

    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        DoGame_Start();
    }

    protected override IEnumerator OnEnableObjectCoroutine()
    {
        yield return null;

        PlayerInput pPlayerInput = FindObjectOfType<PlayerInput>();
        pPlayerInput.p_pCharacterMovement.p_Event_OnMovePlayer.Subscribe += P_Event_OnMovePlayer_Subscribe;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (CheckDebugFilter(EDebugFilter.Debug_Level_App) == false)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            DoGame_Start();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            DoGame_Victory();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            DoGame_Fail();

        if (Input.GetKeyDown(KeyCode.Alpha4))
            DoRecoveryTime();
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    IEnumerator CoGameStart()
    {
        _bIsWaitAction = false;

        yield return null;

        PlayerInput pPlayerInput = FindObjectOfType<PlayerInput>();
        Vector3 vecPlayerPos = pPlayerInput.transform.position;
        vecPlayerPos.y = 10f;

        CFollowObject pObjectFollow = FindObjectOfType<CFollowObject>();
        if(pObjectFollow != null)
        {
            pObjectFollow.transform.position = vecPlayerPos;
            pObjectFollow.DoInitTarget(pPlayerInput.transform);
            pObjectFollow.DoSetFollow(true);
        }

        AstarPath.active?.Scan();
        p_Event_OnGameState.DoNotify(EGameState.Start);

        Spawner_Jewel[] arrJewelSpawn = FindObjectsOfType<Spawner_Jewel>();
        _iJewelCount_Total = 0;
        for(int i = 0; i < arrJewelSpawn.Length; i++)
        {
            if (arrJewelSpawn[i].gameObject != null && arrJewelSpawn[i].gameObject.activeSelf)
                _iJewelCount_Total++;
        }

        _iJewelCount_Current = _iJewelCount_Total;
    }

    private void P_Event_OnMovePlayer_Subscribe(bool bMovement)
    {
        float fCurrentTimeScale = _pManagerTimeScale.p_fCurrentTimeScale;
        if (bMovement)
            _pManagerTimeScale.DoSetTimeScale(Mathf.Clamp(fCurrentTimeScale + Time.unscaledDeltaTime, 0.1f, 1f));
        else
            _pManagerTimeScale.DoSetTimeScale(Mathf.Clamp(fCurrentTimeScale - Time.unscaledDeltaTime, 0.1f, 1f));
    }

    #endregion Private
}