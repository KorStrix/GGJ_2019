#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-26 오전 7:39:54
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
public class MouseClick_Enemy : CObjectBase, IPointerClickHandler
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    /* public - Field declaration            */


    /* protected & private - Field declaration         */


    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    public void OnPointerClick(PointerEventData eventData)
    {
        HomeKeeperGameManager.instance.Event_OnAction(gameObject);
    }

    // ========================================================================== //

    /* protected - Override & Unity API         */


    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}