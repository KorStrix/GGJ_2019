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
        Debug.Log(name + " OnPointerClick");

        var pTweenButton = HomeKeeperGameManager.instance.p_pRadialPosition_Button;
        pTweenButton.gameObject.SetActive(true);

        pTweenButton.transform.SetParent(transform);
        pTweenButton.transform.DoResetTransform();
        pTweenButton.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
        pTweenButton.DoPlayTween_Forward();
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}