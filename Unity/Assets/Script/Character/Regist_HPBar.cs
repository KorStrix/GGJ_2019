#region Header
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

    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        _pHealthBar = UIManager.instance.GetHealthBar();
    }

    protected override void OnDisableObject()
    {
        base.OnDisableObject();

        UIManager.instance.Return_HealthBar(_pHealthBar);
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}