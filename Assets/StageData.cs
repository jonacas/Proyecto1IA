using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour {

	public GameObject player;
	public static StageData currentInstance;
	public List<EnemyMovement> enemiesInStage;

	public CreacionGrafo CG;

	void Awake()
	{
		currentInstance = this;
	}
	public GameObject GetPlayer()
	{
		return player;
	}
	public List<Transform> GetPathToTarget(Transform requester, Transform target)
	{
		List<Transform> camino;

		Node inicio, final;

		int initX = (int)(requester.transform.position.x / CG.incrementoX);
		int initZ = (int)(requester.transform.position.z / CG.incrementoZ);
		int finalX = (int)(target.transform.position.x / CG.incrementoX);
		int finalZ = (int)(target.transform.position.z / CG.incrementoZ);

		if (initX < 0 || initX >= CG.filas || initZ < 0 || initZ >= CG.columnas || finalX < 0 || finalX >= CG.filas || finalZ < 0 || finalZ >= CG.columnas)
			return null;

		inicio = CG.nodeMap [initZ, initX].GetComponent<Node>();
		final = CG.nodeMap [finalZ, finalX].GetComponent<Node>();
		print (inicio.gameObject.name);
		print (final.gameObject.name);

		camino = AEstrella.FindPath(final, inicio, CG.filas * CG.columnas * 5, false, true);
		StageData.currentInstance.enemiesInStage [0].SetNewPath (camino);

		foreach (GameObject n in CG.nodeMap)
		{
			if(n != null)
				n.GetComponent<Node>().Cost = float.PositiveInfinity;
		}

		return camino;
	}
		
}
