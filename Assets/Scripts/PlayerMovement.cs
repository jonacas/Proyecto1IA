using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
	public GameObject camera;
    private float moveX;
    private float moveY;
    private Quaternion targetRotation;
    public GameObject child;
    private const float RotationSpeed = 380f;
	private const float moveSpeed = 10;
	private Rigidbody RB;

    // Use this for initialization
    void Start () {
		RB = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

        ReadInputs();
        
	}

    private void FixedUpdate()
    {
		RB.velocity = Vector3.zero;
        MovePlayer();
    }

    void MovePlayer()
    {
		transform.Translate(moveX * Time.fixedDeltaTime * moveSpeed, 0, moveY * Time.fixedDeltaTime * moveSpeed);
		child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation,targetRotation,RotationSpeed * Time.fixedDeltaTime);

    }

    void ReadInputs() {

		moveX = Input.GetAxisRaw ("Horizontal");
		moveY = Input.GetAxisRaw("Vertical");

		Quaternion cameraRotation = new Quaternion (0f,camera.transform.rotation.y, 0f, camera.transform.rotation.w);

        if (moveX > 0)
        {

            if (moveY > 0)
            {
				targetRotation = cameraRotation * Quaternion.Euler(0, -45, 0); // Era (0, 45, 0)
            }
            else if (moveY < 0)
            {
				targetRotation = cameraRotation * Quaternion.Euler(0, 45, 0);// Era (0,135, 0)
            }
            else
            {
				targetRotation = cameraRotation * Quaternion.Euler(0, 0, 0); // Era (0, 90, 0)
            }
        }
        else if (moveX < 0)
        {
            if (moveY > 0)
            {
				targetRotation = cameraRotation * Quaternion.Euler(0, 225, 0); // Era (0, 315, 0)
            }
            else if (moveY < 0)
            {
				targetRotation = cameraRotation *  Quaternion.Euler(0, 135, 0); // Era (0, 225, 0)
            }
            else
            {
				targetRotation = cameraRotation *  Quaternion.Euler(0, 180, 0); // Era (0,270, 0)

            }
        }
        else {
            if (moveY > 0) {

				targetRotation = cameraRotation * Quaternion.Euler(0, -90, 0); // Era (0,0,0)
            }
            else if(moveY < 0) {
				targetRotation = cameraRotation *  Quaternion.Euler(0, 90, 0); // Era (0, 180, 0)
            }

        }
    }
}
