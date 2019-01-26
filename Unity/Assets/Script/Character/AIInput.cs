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
    AIPath _pAIPath = null;
    [GetComponent]
    CharacterModel _pCharacterModel = null;

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

    protected override void OnAwake()
    {
        base.OnAwake();

        _pCharacterModel.p_Event_OnSetTarget.Subscribe += Event_OnSetTarget_Subscribe;
    }

    private void Event_OnSetTarget_Subscribe(GameObject pObject)
    {
        DoSetTarget(pObject.transform);
    }

    protected override void OnEnableObject()
    {
        base.OnEnableObject();

        _pCharacterModel.DoStartAI();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_pTransformTarget == null)
            return;


        float fDistance = Vector3.Distance(transform.position, _pTransformTarget.position); 
        if(fDistance > _pCharacterModel.GetCurrentWeapon().Range)
        {
            _pAIPath.destination = _pTransformTarget.position;
            _pCharacterMovement.DoMove(_pAIPath.desiredVelocity.normalized);
        }
        else
        {
            _pCharacterMovement.DoLookAt((_pTransformTarget.position - transform.position).normalized);
        }
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
        }
    }

    #endregion Private
}