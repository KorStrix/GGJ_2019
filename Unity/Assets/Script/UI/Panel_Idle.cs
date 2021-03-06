﻿#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-25 오후 11:35:42
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
        Text_ElapseTime,
        Text_Remain_JewelCount,
    }

    public enum EImage
    {
        Image_HP_Fill,

        Image_Icon_Weapon,
        Image_Icon_Armor,

        ImageFill_Stat_Damage,
        ImageFill_Stat_Armor,
        ImageFill_Stat_Hitrate,
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

    [GetComponentInChildren]
    CTweenScale _pTweenScale_TimeIcon = null;

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

    protected override IEnumerator OnEnableObjectCoroutine()
    {
        yield return new WaitForSeconds(0.1f);

        CManagerTimeScale.instance.p_Event_OnChangeTimeScale.Subscribe += P_Event_OnChangeTimeScale_Subscribe;

        HomeKeeperGameManager.instance.p_Event_OnAction.Subscribe += P_Event_OnAction_Subscribe;
        HomeKeeperGameManager.instance.p_Event_OnChangeJewel.Subscribe_And_Listen_CurrentData += P_Event_OnChangeJewel_Subscribe;

        PlayerInput pPlayerInput = FindObjectOfType<PlayerInput>();
        CharacterModel pCharacterModel = pPlayerInput.GetComponent<CharacterModel>();
        pCharacterModel.EventOnAwake();
        pCharacterModel.pStat.p_Event_OnChangeStatus.Subscribe += P_Event_OnChangeStatus_Subscribe;
        pCharacterModel.p_Event_OnChange_Weapon.Subscribe_And_Listen_CurrentData += P_Event_OnChange_Weapon_Subscribe;
        pCharacterModel.p_Event_OnChange_Armor.Subscribe_And_Listen_CurrentData += P_Event_OnChange_Armor_Subscribe;

        pPlayerInput.p_pCharacterMovement.p_Event_OnMovePlayer.Subscribe += P_Event_OnMovePlayer_Subscribe;
    }

    private void P_Event_OnChangeJewel_Subscribe(int arg1, int arg2)
    {
        _mapText[EText.Text_Remain_JewelCount].text = string.Format("{0}/{1}", arg2, arg1);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        TimeSpan timeSpan = TimeSpan.FromSeconds(Time.time);
        _mapText[EText.Text_ElapseTime].text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }

    private void P_Event_OnMovePlayer_Subscribe(bool bIsMove, Vector3 arg2)
    {
        if(bIsMove && _pTweenScale_TimeIcon.p_bIsPlayingTween)
        {
            _pTweenScale_TimeIcon.DoStopTween();
        }
        else if(bIsMove == false && _pTweenScale_TimeIcon.p_bIsPlayingTween == false)
        {
            _pTweenScale_TimeIcon.DoPlayTween_Forward();
        }
    }

    private void P_Event_OnChange_Weapon_Subscribe(Weapon pWeapon)
    {
        _mapImage[EImage.Image_Icon_Weapon].SetActive(pWeapon != null && pWeapon.Type != WeaponType.fist);
        if (pWeapon != null)
            _mapImage[EImage.Image_Icon_Weapon].sprite = pWeapon.p_pSprite_OnUI;
    }

    private void P_Event_OnChange_Armor_Subscribe(Armor pArmor)
    {
        _mapImage[EImage.Image_Icon_Armor].SetActive(pArmor != null);
        if (pArmor != null)
            _mapImage[EImage.Image_Icon_Armor].sprite = pArmor.p_pSprite_OnUI;
    }

    private void P_Event_OnChangeStatus_Subscribe(Stats sStats)
    {
        _mapImage[EImage.Image_HP_Fill].fillAmount = sStats.GetRemainHP_0_1();

        //DoEditText(EText.Text_Stat_S, sStats.finalStr.ToString());
        //DoEditText(EText.Text_Stat_D, sStats.finalDex.ToString());
        //DoEditText(EText.Text_Stat_L, sStats.finalLuk.ToString());
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