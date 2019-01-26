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
using Pathfinding;

/// <summary>
/// 
/// </summary>
[RequireComponent(typeof(AIPath))]
public class AIMovement_Input : CObjectBase
{
    /* const & readonly declaration             */

    /* enum & struct declaration                */

    /* public - Field declaration            */

    [SerializeField]
    List<Spawner_Jewel> _listJewel;

    /* protected & private - Field declaration         */

    [GetComponent]
    CharacterMovement _pCharacterMovement = null;
    [GetComponent]
    AIPath _pAIPath = null;

    Transform _pTransformTarget;
    int _iJewelIndex;

    // ========================================================================== //

    /* public - [Do] Function
     * 외부 객체가 호출(For External class call)*/

    public void DoInitJewelList(List<Spawner_Jewel> listJewel)
    {
        _iJewelIndex = 0;
        _listJewel = listJewel;

        CalculateNextJewel();
    }

    public void DoSetTarget(Transform pTransformTarget)
    {
        _pTransformTarget = pTransformTarget;
    }

    // ========================================================================== //

    /* protected - Override & Unity API         */

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_pTransformTarget == null)
            return;

        _pAIPath.destination = _pTransformTarget.position;
        _pCharacterMovement.DoMove(_pAIPath.desiredVelocity.normalized);
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    private void CalculateNextJewel()
    {
        bool bIsNotFind_NextJewel = true;
        while (_iJewelIndex < _listJewel.Count)
        {
            if (_listJewel[_iJewelIndex].p_pJewel.p_bIsStolen == false)
            {
                _listJewel[_iJewelIndex].p_pJewel.p_Event_OnStolen.Subscribe += CalculateNextJewel;
                DoSetTarget(_listJewel[_iJewelIndex++].transform);
                bIsNotFind_NextJewel = false;
                break;
            }

            _iJewelIndex++;
        }

        if (bIsNotFind_NextJewel)
        {
            HomeKeeperGameManager.instance.DoGame_Fail();

            //Compo_ExitGate[] arrExitGates = FindObjectsOfType<Compo_ExitGate>();
            //Vector3 vecTransformPos = transform.position;
            //float fMinestDistance = float.MaxValue;
            //Transform pTransform_Closest = null;

            //for(int i = 0; i < arrExitGates.Length; i++)
            //{
            //    float fCurrentDistance = Vector3.Distance(vecTransformPos, arrExitGates[i].transform.position);
            //    if(fCurrentDistance < fMinestDistance)
            //    {
            //        fMinestDistance = fCurrentDistance;
            //        pTransform_Closest = arrExitGates[i].transform;
            //    }
            //}

            //if (pTransform_Closest != null)
            //    DoSetTarget(pTransform_Closest);
            //else
            //    Debug.LogError(name + "Not Found Closest Exit Gate", this);
        }
    }

    #endregion Private
}