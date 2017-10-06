using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	private const float MOVE_SPEED_INCOMBAT = 5f;
	private const float TURN_SPEED_INCOMBAT = 180f;
	private const float PATH_REACH_NODE_THS = 0.5f;
	private const float PATH_REACH_PLAYER_THS = 1f;
	private const float PATH_STUCK_TIMELIMIT = 5f;

	private float timeElapsedReachingNextNode = 0;
	private Vector3 previousValidUnstuckNode;

	private List<Vector3> targetPathPositions;
	private Transform target_player_position;
	private Transform target_path_position;
	private Quaternion target_rotation;

	public void SetNewPath(List<Vector3> newPath)
	{
		StopAllCoroutines ();
		targetPathPositions = newPath;
		StartCoroutine ("FollowPath");
	}

	IEnumerator FollowPath()
	{
		Quaternion auxRotation;
		previousValidUnstuckNode = transform.position;
		while (targetPathPositions.Count > 0) {
			while (Vector3.Distance (transform.position, targetPathPositions [0]) > PATH_REACH_NODE_THS) {
				auxRotation = transform.rotation; 
				transform.LookAt (targetPathPositions [0]); 
				target_rotation = transform.rotation; 
				transform.rotation = auxRotation;
				transform.rotation = Quaternion.RotateTowards (transform.rotation, target_rotation, TURN_SPEED_INCOMBAT * Time.deltaTime);

				transform.Translate (Vector3.forward * MOVE_SPEED_INCOMBAT * Time.deltaTime);
				yield return null;
			}
			print ("[" + gameObject.name + "]: Node reached." );
			targetPathPositions.RemoveAt (0);
			previousValidUnstuckNode = transform.position;
		}
		print ("[" + gameObject.name + "]: Path finished." );
	}

}
