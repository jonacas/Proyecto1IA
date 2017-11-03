using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameras : MonoBehaviour {

	const int STATE_STOP = 0, STATE_ROTATING = 1;

	public float timeCameraIsStopped = 1.5f, rotationSpeed = 15f, maxAngleRotation = 50f;
	float state, aux/* count time */;
	Vector3 originalForward;

	void Awake()
	{
		state = STATE_ROTATING;
		originalForward = transform.forward;
	}
	void Update () {
		rotate ();
	}

	void rotate(){
		if ((Vector3.Angle (transform.forward, originalForward) > maxAngleRotation) && (aux == 0 || (state == STATE_STOP))) {
			state = STATE_STOP;
			aux += Time.deltaTime;
			if (aux > timeCameraIsStopped) {
				state = STATE_ROTATING;
				rotationSpeed = -rotationSpeed;
			}
		}
		if (state == STATE_ROTATING) {
			if (aux != 0 && Vector3.Angle (transform.forward, originalForward) < maxAngleRotation)
				aux = 0;
			transform.Rotate (Vector3.up, Time.deltaTime * rotationSpeed); 
		}
	}
}
