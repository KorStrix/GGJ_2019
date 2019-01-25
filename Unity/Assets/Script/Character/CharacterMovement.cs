#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-26 오전 3:29:23
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 
/// </summary>
public class CharacterMovement : CObjectBase
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    /* public - Field declaration            */


    /* protected & private - Field declaration         */

    [GetComponent]
    CharacterModel _pCharacterModel = null;
    [GetComponent]
    Transform _pTransform = null;
    [GetComponent]
    Rigidbody _pRigidbody = null;

    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    public void DoMove(Vector3 vecDesireDirection)
    {
        //Vector3 vecPos = _pTransform.position + new Vector3(fSpeedX, 0.0f, fSpeedZ);
        //_pRigidbody.MovePosition(vecPos);

        _pRigidbody.velocity = vecDesireDirection * _pCharacterModel.basics.speed.FValue * Time.deltaTime;
    }

    // ========================================================================== //

    /* protected - Override & Unity API         */


    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}