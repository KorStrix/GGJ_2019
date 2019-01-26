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
public class AIInput : CObjectBase
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
    CharacterModel _pCharacterModel = null;

    [GetComponent]
    AIPath _pAIPath = null;
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

        while(true)
        {
            yield return StartCoroutine(CoAIState_OnScanTarget());
            yield return StartCoroutine(CoAIState_OnChase_ForAttack());

            yield return null;
        }
    }

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

    IEnumerator CoAIState_OnScanTarget()
    {
        _pPhysicsTrigger.GetComponent<SphereCollider>().radius = _pStat_Mine.fDetectArea;
        _pPhysicsTrigger.enabled = true;
        _pPhysicsTrigger.DoClear_InColliderList();

        while (_pPhysicsTrigger.GetColliderList_3D_Stay().Count == 0)
        {
            yield return null;
        }
        _pPhysicsTrigger.enabled = false;

        List<Collider> listCollider = _pPhysicsTrigger.GetColliderList_3D_Stay();
        DoSetTarget(listCollider[0].transform);
    }


    IEnumerator CoAIState_OnChase_ForAttack()
    {
        _pPhysicsTrigger.GetComponent<SphereCollider>().radius = _pStat_Mine.fDetectArea;
        _pPhysicsTrigger.enabled = true;
        _pPhysicsTrigger.DoClear_InColliderList();

        while (_pPhysicsTrigger.GetColliderList_3D_Stay().Count == 0 || _pCharacterModel._pWeapon_Equied.DoCheck_IsReadyToFire() == false)
        {
            yield return null;
        }
        _pPhysicsTrigger.enabled = false;

        List<Collider> listCollider = _pPhysicsTrigger.GetColliderList_3D_Stay();
        _pCharacterModel.DoAttack_Melee(listCollider[0].gameObject);
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
        }
    }

    #endregion Private
}