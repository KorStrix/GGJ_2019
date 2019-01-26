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

        Text_TimeScale,
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

    static public CObserverSubject<EButton> p_Event_OnClickButton { get; private set; } = new CObserverSubject<EButton>();

    /* protected & private - Field declaration         */

    [GetComponentInChildren]
    Dictionary<EText, UnityEngine.UI.Text> _mapText = new Dictionary<EText, UnityEngine.UI.Text>();
    [GetComponentInChildren]
    Dictionary<EButton, UnityEngine.UI.Button> _mapButton = new Dictionary<EButton, UnityEngine.UI.Button>();

    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    public void DoEditText(EText eText, string strText)
    {
        _mapText[eText].text = strText;
    }

    public void IUIObject_HasButton_OnClickButton(EButton eButtonName)
    {
        foreach (var pButton in _mapButton.Values)
            DisableButton(pButton);
        CurrentActivateButton(eButtonName);

        p_Event_OnClickButton.DoNotify(eButtonName);
    }

    // ========================================================================== //

    /* protected - Override & Unity API         */

    protected override void OnAwake()
    {
        base.OnAwake();

        CManagerTimeScale.instance.p_Event_OnChangeTimeScale.Subscribe += P_Event_OnChangeTimeScale_Subscribe;

        HomeKeeperGameManager.instance.p_Event_OnAction.Subscribe += P_Event_OnAction_Subscribe;
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    private void CurrentActivateButton(EButton eButtonName)
    {
        _mapButton[eButtonName].targetGraphic.color = _mapButton[eButtonName].colors.pressedColor;
    }

    private void DisableButton(UnityEngine.UI.Button pButton)
    {
        pButton.targetGraphic.color = pButton.colors.disabledColor;
        pButton.enabled = false;
    }

    private void P_Event_OnChangeTimeScale_Subscribe(float fTimeScale)
    {
        _mapText[EText.Text_TimeScale].text = fTimeScale.ToString("F1");
    }

    private void P_Event_OnAction_Subscribe(GameObject pObjectTarget)
    {
        foreach (var pButton in _mapButton.Values)
        {
            pButton.targetGraphic.color = pButton.colors.normalColor;
            pButton.enabled = true;
        }
    }


    #endregion Private
}