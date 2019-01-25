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

    /* protected & private - Field declaration         */

    CManagerTimeScale _pManagerTimeScale;

    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    public void DoGame_Start()
    {
        p_Event_OnGameState.DoNotify(EGameState.Start);
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

    // ========================================================================== //

    /* protected - Override & Unity API         */

    protected override void OnAwake()
    {
        base.OnAwake();

        _pManagerTimeScale = CManagerTimeScale.instance;

        if(UIManager.instance == null)
        {
            GameObject.Instantiate(Resources.Load("Prefab/UIRoot"));
        }

        PlayerInput.p_Event_OnMovePlayer.Subscribe += P_Event_OnMovePlayer_Subscribe;
    }

    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        DoGame_Start();
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

    private void P_Event_OnMovePlayer_Subscribe(bool bMovement)
    {
        float fCurrentTimeScale = _pManagerTimeScale.p_fCurrentTimeScale;
        if (bMovement)
            _pManagerTimeScale.DoSetTimeScale(Mathf.Clamp(fCurrentTimeScale + Time.unscaledDeltaTime, 0.1f, 1f)  );
        else
            _pManagerTimeScale.DoSetTimeScale(Mathf.Clamp(fCurrentTimeScale - Time.unscaledDeltaTime, 0.1f, 1f));
    }

    #endregion Private
}