using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    private float moveX;
    private float moveY;
    private Quaternion targetRotation;
    public GameObject child;
    private const float RotationSpeed = 380f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        ReadInputs();
        
	}

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        transform.Translate(moveX, 0, moveY);
        child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation,targetRotation,RotationSpeed * Time.fixedDeltaTime);

    }

    void ReadInputs() {

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        if (moveX > 0)
        {

            if (moveY > 0)
            {
                targetRotation = Quaternion.Euler(0, 45, 0);
            }
            else if (moveY < 0)
            {
                targetRotation = Quaternion.Euler(0, 135, 0);
            }
            else
            {
                targetRotation = Quaternion.Euler(0, 90, 0);
            }
        }
        else if (moveX < 0)
        {
            if (moveY > 0)
            {
                targetRotation = Quaternion.Euler(0, 315, 0);
            }
            else if (moveY < 0)
            {
                targetRotation = Quaternion.Euler(0, 225, 0);
            }
            else
            {
                targetRotation = Quaternion.Euler(0, 270, 0);

            }
        }
        else {
            if (moveY > 0) {

                targetRotation = Quaternion.Euler(0, 0, 0);
            }
            else if(moveY < 0) {
                targetRotation = Quaternion.Euler(0, 180, 0);
            }

        }

    }
}
