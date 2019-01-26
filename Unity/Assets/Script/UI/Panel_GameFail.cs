#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-25 오후 11:25:59
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class Panel_GameFail : CUGUIPanelBase, IUIObject_HasButton<Panel_GameFail.EButton>
{



    /* const & readonly declaration             */

    /* enum & struct declaration                */

    /* public - Field declaration            */
    public enum EButton {

        Button_Exit,
        Button_Restart
    }
    static public CObserverSubject<EButton> p_Event_OnClickButton { get; private set; } = new CObserverSubject<EButton>();

    /* protected & private - Field declaration         */


    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    public void IUIObject_HasButton_OnClickButton(EButton eButtonName) {
        switch (eButtonName) {
        case EButton.Button_Exit:

            break;
        case EButton.Button_Restart:

            break;

        }
        p_Event_OnClickButton.DoNotify(eButtonName);
    }
    // ========================================================================== //

    /* protected - Override & Unity API         */


    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}