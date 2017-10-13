using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeingPlayer : MonoBehaviour {
	
	public GameObject Player; // the player gameobject
	public float SafetyAngle = 50f, SafetyDistance = 15f; // no hay na mas que decir
	public bool TeVeo;

	private RaycastHit objectHitted;
	private Vector2 VectorBetweenPlayerAndEnemy, VectorFordwardEnemy;

	void Update () {
		VectorBetweenPlayerAndEnemy = (new Vector2 (Player.transform.position.x, Player.transform.position.z) - new Vector2 (this.transform.position.x, this.transform.position.z)).normalized;
		VectorFordwardEnemy = new Vector2 (transform.forward.x, transform.forward.z);

		if (Vector3.Distance (transform.position, Player.transform.position) < SafetyDistance &&
		    Vector2.Angle (VectorBetweenPlayerAndEnemy, VectorFordwardEnemy) < SafetyAngle) {
			Physics.Raycast (transform.position, (Player.transform.position - transform.position).normalized, out objectHitted/*, 15f, layerDefault*/);
			TeVeo = objectHitted.collider.gameObject.tag == "Player";
		} else {
			TeVeo = false;
		}
	}
}
