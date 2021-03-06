﻿#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-27 오전 7:22:45
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class Regist_HPBar : CObjectBase
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    /* public - Field declaration            */


    /* protected & private - Field declaration         */

    [SerializeField]
    HealthBar _pHealthBar;

    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/


    // ========================================================================== //

    /* protected - Override & Unity API         */

    protected override IEnumerator OnEnableObjectCoroutine()
    {
        while (UIManager.instance == null)
            yield return null;

        yield return null;

        _pHealthBar = UIManager.instance.GetHealthBar();
        _pHealthBar.transform.SetParent(transform);
        _pHealthBar.transform.DoResetTransform();

        CharacterModel pCharacterModel = GetComponentInParent<CharacterModel>();
        pCharacterModel.pStat.p_Event_OnChangeStatus.Subscribe += P_Event_OnChangeStatus_Subscribe;
    }

    protected override void OnDisableObject()
    {
        base.OnDisableObject();

        if(UIManager.instance)
            UIManager.instance.Return_HealthBar(_pHealthBar);
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    private void P_Event_OnChangeStatus_Subscribe(Stats obj)
    {
        _pHealthBar.DoEdit_HealthBar(obj.GetRemainHP_0_1());
    }

    #endregion Private
}