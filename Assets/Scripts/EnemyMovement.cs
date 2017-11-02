using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	private const float MOVE_SPEED = 5f;
	private const float TURN_RATE = 360f;
	public float PATH_REACH_NODE_THS = 3f;
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

	public int enemyIDStage;
	public int enemyIDStagePart;

	private EnemyState currentState;
	private Transform beforeAlert;

	public Transform startPatrolPoint;
	public Transform endPatrolPoint;

	public Transform[] patrolRoute; //Falta por implementar esto
	private int currentPatrolPosition;

	public bool playerCaptured = false;
	public float thresholdEnemyCapture = 5f;

	public enum EnemyState
	{
		Patrolling,
		Alert,
		Informing,
		InCombat
	}

	void Start()
	{
		currentState = EnemyState.Patrolling;
		currentPatrolPosition = 0;
		Player = StageData.currentInstance.GetPlayer ();
		//StageData.currentInstance.enemiesInStage.Add (this);

		List<Transform> newRoute = 
			StageData.currentInstance.GetPathToTarget (transform, startPatrolPoint);
		SetNewPath (newRoute);

		//StartCoroutine ("FollowPlayer");
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
		if (Vector3.Distance (transform.position, Player.transform.position) < thresholdEnemyCapture)
		{
			PlayerCatched ();
			playerCaptured = true;
			StopCoroutine ("FollowPath");
			//print ("Hemos parado corutina del enemigo");
		}
		else if (!playerCaptured && IsPlayerInVisionRange ()) 
		{
			enemyLight.color = Color.red;
			StageData.currentInstance.SendAlert (this, Player.transform);
			//print ("Iniciamos protocolo de aviso a enemigos");
		} 
		else 
		{
			enemyLight.color = Color.white;
		}

	}

	public void SetState(EnemyState newstate)
	{
		switch (newstate)
		{
		case EnemyState.Patrolling:
			{
				currentState = EnemyState.Patrolling;
				moveMultiplier = 1f;
				break;
			}
		case EnemyState.Informing:
			{
				currentState = EnemyState.Informing;
				moveMultiplier = 0.8f;
				break;
			}
		case EnemyState.Alert:
			{
				//print ("Actualizamos estado a estado de Alerta");
				if (currentState != EnemyState.Alert) 
				{
					beforeAlert = transform;
					//print ("Añadimos posicion a la que volver despues");
				}
				currentState = EnemyState.Alert;
				moveMultiplier = 1.5f;
				break;
			}
		case EnemyState.InCombat:
			{
				currentState = EnemyState.InCombat;
				moveMultiplier = 2f;
				break;
			}
		}
	}
	public void SetNewPath(List<Transform> newPath)
	{
		//print ("Iniciamos el desarrollo de caminos para el jugador");
		if (newPath == null)
			return;
		StopCoroutine ("FollowPath");
		targetPathPositions = newPath;
		StartCoroutine ("FollowPath");
		//print ("Parado y restarteado nuevo camino de enemigo");
	}

	public bool IsPlayerInVisionRange()
	{
		VectorBetweenPlayerAndEnemy = (new Vector2 (Player.transform.position.x, Player.transform.position.z) - new Vector2 (this.transform.position.x, this.transform.position.z)).normalized;
		VectorFordwardEnemy = new Vector2 (transform.forward.x, transform.forward.z);

		if (Vector3.Distance (transform.position, Player.transform.position) < SafetyDistance &&
			Vector2.Angle (VectorBetweenPlayerAndEnemy, VectorFordwardEnemy) < SafetyAngle)
		{
			//print ("Detectamos choque con algo");
			Physics.Raycast (transform.position, (Player.transform.position - transform.position).normalized, out objectHitted/*, 15f, layerDefault*/);
			return objectHitted.collider.gameObject.tag == "Player";
		} 
		else 
		{
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
				//print ("Nos movemos hacia objetivo");
				yield return null;
			}
			targetPathPositions.RemoveAt (0);
			previousValidUnstuckNode = transform.position;
		}
		//Hemos llegado al final del camino.... [COMPROBAR QUE NO LLEGA HASTA AQUI SIN TERMINAR EL CAMINO]
		if (currentState == EnemyState.Alert) {
			List<Transform> newRoute = 
				StageData.currentInstance.GetPathToTarget (transform, beforeAlert);
			SetNewPath (newRoute);
			SetState (EnemyState.Patrolling);
			//print ("volvemos a la patrulla");
		} 
		else if (currentState == EnemyState.Patrolling && targetPathPositions.Count == 0) //Por si acaso, la verdad... 
		{
			//Si el enemigo esta mas cerca de un punto que de otro, irá hacia el otro.
			if (Vector3.Distance (transform.position, startPatrolPoint.position) < 
				Vector3.Distance (transform.position, endPatrolPoint.position)) {
				List<Transform> newRoute = 
					StageData.currentInstance.GetPathToTarget (transform, endPatrolPoint);
				SetNewPath (newRoute);
				//print ("volvemos a anterior puesto de patrulla");
			} 
			else 
			{
				List<Transform> newRoute = 
					StageData.currentInstance.GetPathToTarget (transform, startPatrolPoint);
				SetNewPath (newRoute);
				SetState (EnemyState.Patrolling);
				//print ("volvemos a anterior puesto de patrulla");
			}

		}



	}

	public bool IsEnemyPatrolling() {return currentState == EnemyState.Patrolling;}

	public void PlayerCatched()
	{
		
	}


}
