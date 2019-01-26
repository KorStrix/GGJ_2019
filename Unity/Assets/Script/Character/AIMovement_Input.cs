﻿#region Header
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
    [GetComponent]
    CharacterModel _pCharacterModel = null;
    [GetComponentInChildren]
    CPhysicsTrigger _pPhysicsTrigger = null;

    Transform _pTransformTarget;
    Stats _pStat_Mine;
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

    protected override IEnumerator OnEnableObjectCoroutine()
    {
        yield return null;

        _pStat_Mine = GetComponent<CharacterModel>().pStat;

        while (true)
        {
            yield return StartCoroutine(CoAIState_OnScanTarget());

            yield return null;

            yield return StartCoroutine(CoAIState_OnChase_ForAttack());
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_pTransformTarget == null)
            return;

        float fDistance = Vector3.Distance(transform.position, _pTransformTarget.position);
        if(fDistance > _pCharacterModel.p_pWeapon_Equiped.Range)
        {
            _pAIPath.destination = _pTransformTarget.position;
            _pCharacterMovement.DoMove(_pAIPath.desiredVelocity.normalized);
        }
        else
        {
            _pCharacterMovement.DoLookAt(_pAIPath.desiredVelocity.normalized);
        }
    }

    /* protected - [abstract & virtual]         */


    // ========================================================================== //

    #region Private

    IEnumerator CoAIState_OnScanTarget()
    {
        _pPhysicsTrigger.GetComponent<SphereCollider>().radius = _pStat_Mine.fDetectArea;
        _pPhysicsTrigger.DoClear_InColliderList();

        while (_pPhysicsTrigger.GetColliderList_3D_Stay().Count == 0)
        {
            yield return null;
        }

        List<Collider> listCollider = _pPhysicsTrigger.GetColliderList_3D_Enter();
        DoSetTarget(listCollider[0].transform);
    }


    IEnumerator CoAIState_OnChase_ForAttack()
    {
        while(true)
        {
            while (_pPhysicsTrigger.GetColliderList_3D_Enter().Count == 0)
            {
                yield return null;
            }

            Transform pTransformTarget = _pPhysicsTrigger.GetColliderList_3D_Enter()[0].transform;
            float fDistance = Vector3.Distance(transform.position, pTransformTarget.position);
            if(_pCharacterModel.p_pWeapon_Equiped.DoCheck_IsReadyToFire(fDistance))
            {
                List<Collider> listCollider = _pPhysicsTrigger.GetColliderList_3D_Enter();
                _pCharacterModel.DoAttack_Melee(listCollider[0].gameObject);
                break;
            }

            yield return null;
        }
    }

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