#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-26 오후 12:51:26
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

/// <summary>
/// 
/// </summary>
public class MouseClick_Player : CObjectBase, IPointerClickHandler
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    /* public - Field declaration            */


    /* protected & private - Field declaration         */


    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    // ========================================================================== //

    /* protected - Override & Unity API         */

    public void OnPointerClick(PointerEventData eventData)
    {
        var tween = GameObject.Find("RadialButton").GetComponent<CTweenPosition_Radial>();
        tween?.DoSetTarget(gameObject);
        tween?.DoPlayTween_Forward();
        
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}