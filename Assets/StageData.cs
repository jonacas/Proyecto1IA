using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour {

	public GameObject player;
	public static StageData currentInstance;
	public List<EnemyMovement> enemiesInStage;

	void Awake()
	{
		currentInstance = this;
	}
	public GameObject GetPlayer()
	{
		return player;
	}
		
}
