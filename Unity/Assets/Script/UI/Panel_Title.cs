#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-26 오전 11:40:58
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class Panel_Title : CUGUIPanelBase, IUIObject_HasButton<Panel_Title.EUIButton>
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    public enum EUIButton
    {
        Button_GameStart,
        Button_Credit,
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
            case EUIButton.Button_GameStart:
                SceneManager.LoadScene(1);
                break;

            case EUIButton.Button_Credit:
                TitleManager.instance.DoShowHide_Panel(TitleManager.EUIPanel.Panel_Title, false);
                TitleManager.instance.DoShowHide_Panel(TitleManager.EUIPanel.Panel_Credit, true);
                break;

            case EUIButton.Button_Exit:
                Application.Quit();
                break;
        }
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}