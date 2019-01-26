#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-26 오전 11:42:36
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class Panel_Credit : CUGUIPanelBase, IUIObject_HasButton<Panel_Credit.EUIButton>
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    public enum EUIButton
    {
        Button_Exit,
    }

    /* public - Field declaration            */


    /* protected & private - Field declaration         */


    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/


    // ========================================================================== //

    /* protected - Override & Unity API         */

    public void IUIObject_HasButton_OnClickButton(EUIButton eButtonName)
    {
        switch (eButtonName)
        {
            case EUIButton.Button_Exit:
                TitleManager.instance.DoShowHide_Panel(TitleManager.EUIPanel.Panel_Credit, false);
                TitleManager.instance.DoShowHide_Panel(TitleManager.EUIPanel.Panel_Title, true);
                break;

            default:
                break;
        }
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}