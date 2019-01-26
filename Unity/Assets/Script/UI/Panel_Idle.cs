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

    public enum EImage
    {
        Image_HP_Fill,

        Image_Icon_Weapon,
        Image_Icon_Armor,
        Image_Icon_Helmet,
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
    [GetComponentInChildren]
    Dictionary<EImage, UnityEngine.UI.Image> _mapImage = new Dictionary<EImage, UnityEngine.UI.Image>();

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

    protected override IEnumerator OnAwakeCoroutine()
    {
        yield return null;

        CManagerTimeScale.instance.p_Event_OnChangeTimeScale.Subscribe += P_Event_OnChangeTimeScale_Subscribe;

        HomeKeeperGameManager.instance.p_Event_OnAction.Subscribe += P_Event_OnAction_Subscribe;

        PlayerInput pPlayerInput = FindObjectOfType<PlayerInput>();
        CharacterModel pCharacterModel = pPlayerInput.GetComponent<CharacterModel>();
        pCharacterModel.EventOnAwake();
        pCharacterModel.pStat.p_Event_OnChangeStatus.Subscribe_And_Listen_CurrentData += P_Event_OnChangeStatus_Subscribe_And_Listen_CurrentData;
        pCharacterModel.p_Event_OnChange_Weapon.Subscribe += P_Event_OnChange_Weapon_Subscribe;
        pCharacterModel.p_Event_OnChange_Armor.Subscribe += P_Event_OnChange_Armor_Subscribe;
    }

    private void P_Event_OnChange_Weapon_Subscribe(Weapon pWeapon)
    {
        Debug.Log(name + " 이미지 들어오면 작업해야함", this);
        //if (pWeapon != null)
        //    _mapImage[EImage.Image_Icon_Weapon].sprite = pWeapon.GetComponentInChildren<SpriteRenderer>().sprite;
        //else
        //    _mapImage[EImage.Image_Icon_Weapon].sprite = null;

    }

    private void P_Event_OnChange_Armor_Subscribe(Armor pArmor)
    {
        Debug.Log(name + " 이미지 들어오면 작업해야함", this);
        //if (pArmor != null)
        //    _mapImage[EImage.Image_Icon_Weapon].sprite = pArmor.GetComponentInChildren<SpriteRenderer>().sprite;
        //else
        //    _mapImage[EImage.Image_Icon_Weapon].sprite = null;
    }

    private void P_Event_OnChangeStatus_Subscribe_And_Listen_CurrentData(Stats sStats)
    {
        DoEditText(EText.Text_Stat_S, sStats.finalStr.ToString());
        DoEditText(EText.Text_Stat_D, sStats.finalDex.ToString());
        DoEditText(EText.Text_Stat_L, sStats.finalLuk.ToString());
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