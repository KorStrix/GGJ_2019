#region Header
/*	============================================
 *	작성자 : Strix
 *	작성일 : 2019-01-26 오전 4:11:10
 *	개요 : 
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class AIMovement_Input : CObjectBase
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    /* public - Field declaration            */


    /* protected & private - Field declaration         */

    [GetComponent]
    CharacterMovement _pCharacterMovement = null;
    [GetComponent]
    NavMeshAgent _pNavmeshAgent = null;

    Transform _pTransformTarget;

    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    public void DoSetTarget(Transform pTransformTarget)
    {
        _pTransformTarget = pTransformTarget;
    }

    // ========================================================================== //

    /* protected - Override & Unity API         */

    protected override void OnAwake()
    {
        base.OnAwake();

    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_pTransformTarget == null)
            return;

        _pNavmeshAgent.SetDestination(_pTransformTarget.position);
        _pCharacterMovement.DoMove(_pNavmeshAgent.desiredVelocity.normalized);
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    #endregion Private
}