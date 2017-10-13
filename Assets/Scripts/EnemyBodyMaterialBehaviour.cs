using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyMaterialBehaviour : MonoBehaviour {


	//Estado 2 para buscando, y estado 3 para atacando. Cualquier otro es estado normal.
	public GameObject enemyBody;
	public int enemyState;


	// Use this for initialization
	void Start () 
	{
		
	}
		
	void ChangeColorEnemyBody()
	{
		if (enemyState == 1) 
		{
			enemyBody.GetComponent<MeshRenderer> ().materials[0].color = Color.white;
		}
		if (enemyState == 2) 
		{
			enemyBody.GetComponent<MeshRenderer> ().materials[0].color = Color.yellow;
		}
		if (enemyState == 3) 
		{
			enemyBody.GetComponent<MeshRenderer> ().materials[0].color = Color.red;
		}

	}


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) 
		{
			ChangeColorEnemyBody ();
		}
	}
}
