using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	private const float MOVE_SPEED = 5f;
	private const float TURN_RATE = 360f;
	private const float PATH_REACH_NODE_THS = 3f;
	private const float PATH_REACH_PLAYER_THS = 1f;
	private const float PATH_STUCK_TIMELIMIT = 5f;

	private float moveMultiplier = 1;

	private Vector3 previousValidUnstuckNode;

	private List<Transform> targetPathPositions;
	private Transform target_player_position;
	private Transform target_path_position;
	private Quaternion target_rotation;

	private GameObject Player; // the player gameobject
	public float SafetyAngle = 50f, SafetyDistance = 15f; // no hay na mas que decir

	private RaycastHit objectHitted;
	private Vector2 VectorBetweenPlayerAndEnemy, VectorFordwardEnemy;


	public Light enemyLight;

	public enum EnemyState
	{
		NoCombat,
		Alert,
		InCombat
	}

	void Start()
	{
		Player = StageData.currentInstance.GetPlayer ();
		StageData.currentInstance.enemiesInStage.Add (this);
		StartCoroutine ("FollowPlayer");
	}
	IEnumerator FollowPlayer()
	{
		while (true) {
			SetNewPath(StageData.currentInstance.GetPathToTarget(transform, StageData.currentInstance.GetPlayer().transform));
			yield return new WaitForSeconds (0.5f);
		}
	}
	void Update()
	{
		if (IsPlayerInVisionRange ()) {
			enemyLight.color = Color.red;
		} else
			enemyLight.color = Color.white;
	}

	public void SetState(EnemyState newstate)
	{
		switch (newstate)
		{
		case EnemyState.NoCombat:
			{
				moveMultiplier = 1;
				break;
			}
		case EnemyState.Alert:
			{
				moveMultiplier = 1.5f;
				break;
			}
		case EnemyState.InCombat:
			{
				moveMultiplier = 2;
				break;
			}
		}
	}
	public void SetNewPath(List<Transform> newPath)
	{
		if (newPath == null)
			return;
		StopCoroutine ("FollowPath");
		targetPathPositions = newPath;
		StartCoroutine ("FollowPath");
	}

	public bool IsPlayerInVisionRange()
	{
		VectorBetweenPlayerAndEnemy = (new Vector2 (Player.transform.position.x, Player.transform.position.z) - new Vector2 (this.transform.position.x, this.transform.position.z)).normalized;
		VectorFordwardEnemy = new Vector2 (transform.forward.x, transform.forward.z);

		if (Vector3.Distance (transform.position, Player.transform.position) < SafetyDistance &&
			Vector2.Angle (VectorBetweenPlayerAndEnemy, VectorFordwardEnemy) < SafetyAngle) {
			Physics.Raycast (transform.position, (Player.transform.position - transform.position).normalized, out objectHitted/*, 15f, layerDefault*/);
			return objectHitted.collider.gameObject.tag == "Player";
		} else {
			return false;
		}
	}
	IEnumerator FollowPath()
	{
		Quaternion auxRotation;
		previousValidUnstuckNode = transform.position;
		while (targetPathPositions.Count > 0) {
			while (Vector3.Distance (transform.position, targetPathPositions [0].position) > PATH_REACH_NODE_THS) {
				auxRotation = transform.rotation; 
				transform.LookAt (targetPathPositions [0]); 
				target_rotation = transform.rotation; 
				transform.rotation = auxRotation;
				transform.rotation = Quaternion.RotateTowards (transform.rotation, target_rotation, TURN_RATE * moveMultiplier * Time.deltaTime);

				transform.Translate (Vector3.forward * MOVE_SPEED * moveMultiplier * Time.deltaTime);
				yield return null;
			}
			targetPathPositions.RemoveAt (0);
			previousValidUnstuckNode = transform.position;
		}
	}

}
