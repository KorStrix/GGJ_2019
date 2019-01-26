#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-27 오전 2:51:46
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class CharacterModel_LookMoveDirection : CObjectBase
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

    protected override void OnAwake()
    {
        base.OnAwake();

        GetComponentInParent<CharacterMovement>().p_Event_OnMovePlayer.Subscribe += P_Event_OnMovePlayer_Subscribe; ;
    }

    private void P_Event_OnMovePlayer_Subscribe(bool arg1, Vector3 arg2)
    {
        if(arg1)
        {
            Vector3 vecScale = transform.localScale;
            vecScale.x = Mathf.Abs(vecScale.x);
            if (arg2.x < 0f)
                vecScale.x *= -1f;

            transform.localScale = vecScale;
        }
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}