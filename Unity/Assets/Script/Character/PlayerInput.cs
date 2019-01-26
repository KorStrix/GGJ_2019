using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerInput : CObjectBase
{
    [GetComponent]
    public CharacterMovement p_pCharacterMovement { get; private set; }
    [GetComponent]
    AIPath _pNavmeshAgent = null;

    Transform _pTransform_NavmeshTarget;

    protected override void OnAwake()
    {
        base.OnAwake();

        _pTransform_NavmeshTarget = null;
        HomeKeeperGameManager.instance.p_Event_OnAction.Subscribe += P_Event_OnAction_Subscribe;
    }

    private void P_Event_OnAction_Subscribe(GameObject pObject)
    {
        _pTransform_NavmeshTarget = pObject.transform;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        float speedX = Mathf.Clamp(Input.GetAxis("Horizontal"), -1f, 1f);
        float speedZ = Mathf.Clamp(Input.GetAxis("Vertical"), -1f, 1f);

        Vector3 vecDesireVelocity;

        if (speedX != 0f || speedZ != 0f)
        {
            HomeKeeperGameManager.instance.Event_OnActionFinish();
            _pTransform_NavmeshTarget = null;
        }

        if (_pTransform_NavmeshTarget)
        {
            _pNavmeshAgent.destination = _pTransform_NavmeshTarget.position;

            //if(_pNavmeshAgent.desiredVelocity.normalized.Equals(Vector3.zero))
            //    _pNavmeshAgent.
            vecDesireVelocity = _pNavmeshAgent.desiredVelocity.normalized;
        }
        else
        {
            vecDesireVelocity = new Vector3(speedX, 0f, speedZ);
        }

        p_pCharacterMovement.DoMove(vecDesireVelocity);
    }
}
