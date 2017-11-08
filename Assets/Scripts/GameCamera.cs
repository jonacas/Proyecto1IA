using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

	public GameObject cameraObject;
	public GameObject player;
	public float cameraAngle = 0f;
	private Vector3 previousMousePosition;
	[Range(-100, 100)]
	public float cameraRotationSpeed;
	[Range(0, 200)]
	public float cameraSpeed;
	private float previousMouseDifference;

	public Material[] wallMaterial;
	[Range(0.3f, 1f)]
	public float wallMaterialAlpha = 1f;


	// Use this for initialization
	void Start () {

		Cursor.lockState = CursorLockMode.Locked;
		cameraSpeed = 100f;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		CheckPlayerVisible ();

		wallMaterial[0].color = new Color(wallMaterial[0].color.r, wallMaterial[0].color.g, wallMaterial[0].color.b, wallMaterialAlpha);
		wallMaterial[1].color = new Color (wallMaterial [1].color.r, wallMaterial [1].color.g, wallMaterial [1].color.b, wallMaterialAlpha);

		cameraObject.transform.LookAt (player.transform);
		//CalculateMousePosition ();
		differenceCameraPositionFromCenter();
		cameraObject.transform.RotateAround(player.transform.position, new Vector3 (0.0f, 1.0f, 0.0f), Time.deltaTime * cameraAngle);
	}

	void differenceCameraPositionFromCenter()
	{
		cameraAngle = Mathf.Lerp(previousMouseDifference, cameraSpeed * Input.GetAxis("Mouse X"), cameraSpeed);
		previousMouseDifference = cameraSpeed * Input.GetAxis("Mouse X");
	}



	void CalculateMousePosition()
	{
		//Aqui tenemos la pos del raton tras el movimiento, respecto el centro de la pantalla.
		Vector3 mouseInputDifferenceFromCenter = new Vector3(Mathf.Abs( Input.mousePosition.x), 0f, 0f);

		//Aqui debemos calcular el movimiento que tiene que hacer para que se mueva a la posicion adecuada.
		if (previousMousePosition.x < mouseInputDifferenceFromCenter.x) 
		{  //Parece obvio pero...
			cameraAngle += Vector3.Lerp (previousMousePosition, mouseInputDifferenceFromCenter, Time.deltaTime).normalized.x * 10;
		}
		else if (previousMousePosition.x > mouseInputDifferenceFromCenter.x) 
		{  //Parece obvio pero...
			cameraAngle -= Vector3.Lerp (mouseInputDifferenceFromCenter, previousMousePosition, Time.deltaTime).normalized.x * 10;
		}
		else { cameraAngle = 0f;		}
		//Reiniciamos valores para siguiente frame
		previousMousePosition = mouseInputDifferenceFromCenter;

	}

	void CheckPlayerVisible()
	{
		RaycastHit hit;
		Vector3 playerRayCastCoordinate = cameraObject.GetComponent<Camera> ().WorldToScreenPoint (player.transform.position);
		Ray newRay = cameraObject.GetComponent<Camera> ().ScreenPointToRay (playerRayCastCoordinate);
		//Debug.DrawRay (newRay.origin, newRay.direction * 50, Color.yellow);
		if (Physics.Raycast (newRay, out hit)) 
		{
			if (hit.transform.tag != "Player") 
			{
				wallMaterialAlpha = 0.75f;
			} 
			else
			{
				wallMaterialAlpha = 1f;
			}
		}


	}



}
