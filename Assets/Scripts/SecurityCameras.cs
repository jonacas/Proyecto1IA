using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameras : MonoBehaviour {

	const int STATE_STOP = 0, STATE_ROTATING = 1;

	public float timeCameraIsStopped = 1.5f, rotationSpeed = 15f, maxAngleRotation = 50f;

	public int idStage;
	public int idStagePart;

	float SafetyAngle = 50;
	float SafetyDistance = 20;

	float state, aux/* count time */;
	Vector3 originalForward;

	private GameObject playerReference;

	void Awake()
	{
		state = STATE_ROTATING;
		originalForward = transform.forward;
	}
	void Start()
	{
		playerReference = StageData.currentInstance.GetPlayer ();
		StartCoroutine ("CheckIfPlayerIsInRange");
	}
	void Update () {
		rotate ();
	}
	IEnumerator CheckIfPlayerIsInRange()
	{
		while (true) {
			if (IsPlayerInVisionRange ()) {
				StageData.currentInstance.SendAlert (playerReference.transform.position, idStage, idStagePart);
			}
			yield return new WaitForSeconds (0.5f);
		}
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
	public bool IsPlayerInVisionRange()
	{
		Vector2 VectorBetweenPlayerAndEnemy = (new Vector2 (playerReference.transform.position.x, playerReference.transform.position.z) - new Vector2 (this.transform.position.x, this.transform.position.z)).normalized;
		Vector2 VectorFordwardEnemy = new Vector2 (transform.forward.x, transform.forward.z);
		RaycastHit objectHitted;

		if (Vector3.Distance (transform.position, playerReference.transform.position) < SafetyDistance &&
			Vector2.Angle (VectorBetweenPlayerAndEnemy, VectorFordwardEnemy) < SafetyAngle)
		{
			//print ("Detectamos choque con algo");
			Physics.Raycast (transform.position, (playerReference.transform.position - transform.position).normalized, out objectHitted/*, 15f, layerDefault*/);
			return objectHitted.collider.gameObject.tag == "Player";
		} 
		else 
		{
			return false;
		}
	}
}
