#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-25 오후 11:35:42
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class Panel_Idle : CUGUIPanelBase, IUIObject_HasButton<Panel_Idle.EButton>
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    public enum EText
    {
        Text_Stat_S,
        Text_Stat_D,
        Text_Stat_L,
    }

    public enum ESprite
    {
        Image_HP_Fill,
    }

    public enum EButton
    {
        Button_Attack,
    }

    /* public - Field declaration            */


    /* protected & private - Field declaration         */

    [GetComponentInChildren]
    Dictionary<EText, UnityEngine.UI.Text> _mapText = new Dictionary<EText, UnityEngine.UI.Text>();

    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    public void DoEditText(EText eText, string strText)
    {
        _mapText[eText].text = strText;
    }

    public void IUIObject_HasButton_OnClickButton(EButton eButtonName)
    {
        switch (eButtonName)
        {
            case EButton.Button_Attack:

                Debug.Log("Attack");

                break;
        }
    }

    // ========================================================================== //

    /* protected - Override & Unity API         */


    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}