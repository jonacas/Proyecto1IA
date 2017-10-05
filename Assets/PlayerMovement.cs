using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    private float moveX;
    private float moveY;
    private float rotateSpeed = 0.9f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        ReadInputs();
        MovePlayer();
	}

    void MovePlayer()
    {
        transform.Translate(moveX, 0, moveY);
        /*if (moveY < 0 && transform.localEulerAngles.y > -90f) {

            transform.localRotation = new Vector3(0, transform.localEulerAngles.y - rotateSpeed, 0);
        }
        else if (moveY > 0 && transform.localEulerAngles.y < 90f) {

            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + rotateSpeed, 0);

        }*/
    }

    void ReadInputs() {

        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        

    }
}
