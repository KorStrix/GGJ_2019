#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-25 오후 11:20:01
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class UIManager : CManagerUGUIBase<UIManager, UIManager.EUIPanel>
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    public enum EUIPanel
    {
        Panel_Idle,
        Panel_GameStart,
        Panel_GameVictory,
        Panel_GameFail,
    }

    /* public - Field declaration            */


    /* protected & private - Field declaration         */


    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/


    // ========================================================================== //

    /* protected - Override & Unity API         */

    protected override void OnDefaultPanelShow()
    {
        DoShowHide_Panel(EUIPanel.Panel_Idle, true);
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        HomeKeeperGameManager.instance.p_Event_OnGameState.Subscribe += P_Event_OnGameState_Subscribe;
    }

    private void P_Event_OnGameState_Subscribe(HomeKeeperGameManager.EGameState eGameState)
    {
        switch (eGameState)
        {
            case HomeKeeperGameManager.EGameState.Start:
                DoShowHide_Panel(EUIPanel.Panel_GameStart, true);
                break;

            case HomeKeeperGameManager.EGameState.Victory:
                DoShowHide_Panel(EUIPanel.Panel_GameVictory, true);
                break;

            case HomeKeeperGameManager.EGameState.Fail:
                DoShowHide_Panel(EUIPanel.Panel_GameFail, true);
                break;
        }
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}