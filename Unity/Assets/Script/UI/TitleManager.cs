#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-26 오전 11:44:50
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class TitleManager : CManagerUGUIBase<TitleManager, TitleManager.EUIPanel>
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    public enum EUIPanel
    {
        Panel_TitleVideo,
        Panel_Title,
        Panel_Credit,
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
        DoShowHide_Panel(EUIPanel.Panel_TitleVideo, true);
        DoShowHide_Panel(EUIPanel.Panel_Title, true);
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}