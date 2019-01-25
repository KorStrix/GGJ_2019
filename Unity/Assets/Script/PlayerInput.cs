using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public float accelX = 0.1f;
    public float accelZ = 0.1f;

	public void Update()
    {
        float speedX = 0f;
        float speedZ = 0f;

        if (Input.GetAxis("Horizontal") > 0.001f)
        {
            speedX = accelX;
        }
        else if (Input.GetAxis("Horizontal") < -0.001f)
        {
            speedX = -accelX;
        }
        
        if (Input.GetAxis("Vertical") < -0.001f)
        {
            speedZ = -accelZ;
        }
        else if (Input.GetAxis("Vertical") > 0.001f)
        {
            speedZ = accelZ;
        }

        this.gameObject.transform.position += new Vector3(speedX, 0.0f, speedZ);
    }
}
