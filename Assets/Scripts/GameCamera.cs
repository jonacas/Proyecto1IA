using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

	public GameObject camera;
	public GameObject player;
	public float cameraAngle = 0f;
	private Vector3 previousMousePosition;
	[Range(-100, 100)]
	public float cameraRotationSpeed;

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void FixedUpdate () {
		camera.transform.LookAt (player.transform);
		CalculateMousePosition ();
		camera.transform.RotateAround(player.transform.position, new Vector3 (0.0f, 1.0f, 0.0f), Time.deltaTime * cameraAngle);
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



}
