using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerInput : CObjectBase {

    [GetComponent]
    CharacterMovement _pCharacterMovement = null;

    public override void OnUpdate()
    {
        base.OnUpdate();

        float speedX = Mathf.Clamp(Input.GetAxis("Horizontal"), -1f, 1f);
        float speedZ = Mathf.Clamp(Input.GetAxis("Vertical"), -1f, 1f);

        _pCharacterMovement.DoMove(new Vector3(speedX, 0f, speedZ)); 
    }
}
