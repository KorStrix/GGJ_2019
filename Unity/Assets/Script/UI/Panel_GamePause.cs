#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-25 오후 11:25:48
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class Panel_GamePause : CUGUIPanelBase, IUIObject_HasButton<Panel_GamePause.EButton> {
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    /* public - Field declaration            */
    public enum EButton {
        Button_Resume,
        Button_Exit,
        Button_Restart
    }
    static public CObserverSubject<EButton> p_Event_OnClickButton { get; private set; } = new CObserverSubject<EButton>();



    /* protected & private - Field declaration         */
    [GetComponentInChildren]
    Dictionary<EButton, UnityEngine.UI.Button> _mapButton = new Dictionary<EButton, UnityEngine.UI.Button>();

    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    public void IUIObject_HasButton_OnClickButton(EButton eButtonName) {

        switch (eButtonName) {
        case EButton.Button_Exit:
            break;
        case EButton.Button_Restart:
            break;
        case EButton.Button_Resume:
            HomeKeeperGameManager.instance.DoGame_Resume();
            break;


        }

        p_Event_OnClickButton.DoNotify(eButtonName);
    }
    // ========================================================================== //

    /* protected - Override & Unity API         */



    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private
    private void CurrentActivateButton(EButton eButtonName) {
        _mapButton[eButtonName].targetGraphic.color = _mapButton[eButtonName].colors.pressedColor;
    }

    private void DisableButton(UnityEngine.UI.Button pButton) {
        pButton.targetGraphic.color = pButton.colors.disabledColor;
        pButton.enabled = false;
    }
    #endregion Private
}