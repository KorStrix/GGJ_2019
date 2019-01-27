#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-25 오후 11:57:53
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public class HealthBar : CUIObjectBase
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    /* public - Field declaration            */

    public float p_fRemainHP_0_1 { get; private set; }

    /* protected & private - Field declaration         */

    [GetComponentInChildren("Image_HP_Fill")]
    Image _pImage_Fill = null;

    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    public void DoEdit_HealthBar(float fRemainHP_0_1)
    {
        p_fRemainHP_0_1 = fRemainHP_0_1;
        _pImage_Fill.fillAmount = fRemainHP_0_1;
    }

    // ========================================================================== //

    /* protected - Override & Unity API         */

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}