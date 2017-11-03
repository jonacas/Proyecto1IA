using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public GameObject cam;
    public Material diamante;
    private float moveX;
    private float moveY;
    private Quaternion targetRotation;
    public GameObject child;
    private const float RotationSpeed = 450f;
	public float moveSpeed = 10;
	public Rigidbody RB;
    public GameCamera cameraScript;
    private float offset;
    private float offsetAux;

    // Use this for initialization
    void Start () {
        diamante.renderQueue = 3800;
    }
	
	// Update is called once per frame
	void Update () {

        ReadInputs();
        
        
	}

    private void FixedUpdate()
    {
		RB.velocity = Vector3.zero;
        RB.angularVelocity = Vector3.zero;
        MovePlayer();
        CameraOffset();
        
        
    }

    void MovePlayer()
    {
        Vector3 currentTranslation = new Vector3(moveX, 0, moveY) * Time.fixedDeltaTime * moveSpeed;
        Quaternion camAdjustedRotation = Quaternion.Euler(0, cam.transform.rotation.eulerAngles.y, 0);
        currentTranslation = camAdjustedRotation * currentTranslation;
		transform.Translate(currentTranslation);
		child.transform.rotation = Quaternion.RotateTowards(child.transform.rotation,targetRotation,RotationSpeed * Time.fixedDeltaTime);

    }

    void CameraOffset() {

  
        offsetAux += cameraScript.cameraAngle;
        offset = offsetAux + transform.rotation.y;
    }

    void ReadInputs() {

		moveX = Input.GetAxisRaw ("Horizontal");
		moveY = Input.GetAxisRaw("Vertical");

		Quaternion cameraRotation = new Quaternion (0f,cam.transform.rotation.y, 0f, cam.transform.rotation.w);

        if (moveX > 0) //Se Mueve en X+
        {

            if (moveY > 0)
            {
				targetRotation = cameraRotation *  Quaternion.Euler(0, -45 , 0); // Era (0, 45, 0)
            }
            else if (moveY < 0)
            {
				targetRotation = cameraRotation * Quaternion.Euler(0, 45 , 0);// Era (0,135, 0)
            }
            else
            {
				targetRotation = cameraRotation * Quaternion.Euler(0, 0 , 0); // Era (0, 90, 0)
            }
        }
        else if (moveX < 0) // Se mueve en Y-
        {
            if (moveY > 0)
            {
				targetRotation = cameraRotation * Quaternion.Euler(0, 225 , 0); // Era (0, 315, 0)
            }
            else if (moveY < 0)
            {
				targetRotation = cameraRotation * Quaternion.Euler(0, 135 , 0); // Era (0, 225, 0)
            }
            else
            {
				targetRotation = cameraRotation * Quaternion.Euler(0, 180 , 0); // Era (0,270, 0)

            }
        }
        else { // X+
            if (moveY > 0)
            {

                targetRotation = cameraRotation * Quaternion.Euler(0, -90 , 0); // Era (0,0,0)
            }
            else if (moveY < 0)
            {
                targetRotation = cameraRotation * Quaternion.Euler(0, 90 , 0); // Era (0, 180, 0)
            }
            else {
                targetRotation = cameraRotation * Quaternion.Euler(0,-90, 0);
            }

        }
    }
}
