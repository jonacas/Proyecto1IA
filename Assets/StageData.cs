using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour {

	public GameObject player;
	public static StageData currentInstance;

	public List<EnemyMovement> enemiesInStage;
	public List<EnemyMovement> enemiesInStage2;
	public List<EnemyMovement> enemiesInStage3;

	private int obstacleLayerMask = 1 << 8;

	public CreacionGrafo CG;

	void Awake()
	{
		currentInstance = this;
	}
	public GameObject GetPlayer()
	{
		return player;
	}
	public List<Transform> GetPathToTarget(Vector3 requester, Vector3 target)
	{
		List<Transform> camino;

		Node inicio, final;

		int initX = (int) Mathf.Round(requester.x / CG.incrementoX);
		int initZ = (int) Mathf.Round(requester.z / CG.incrementoZ);
		int finalX = (int) Mathf.Round(target.x / CG.incrementoX);
		int finalZ = (int) Mathf.Round(target.z / CG.incrementoZ);

		if (initX < 0 || initX >= CG.filas || initZ < 0 || initZ >= CG.columnas || finalX < 0 || finalX >= CG.filas || finalZ < 0 || finalZ >= CG.columnas)
			return null;

		inicio = CG.nodeMap [initZ, initX].GetComponent<Node>();
		if (CG.nodeMap [finalZ, finalX] != null)
			final = CG.nodeMap [finalZ, finalX].GetComponent<Node> ();
		else
			final = null;

		if (final == null) {
			Node closestNode = null;
			for (int i = -1; i < 2; i++) {
				for (int j = -1; j < 2; j++) {
					if (CG.nodeMap [finalZ + i, finalX + j] != null)
						final = CG.nodeMap [finalZ + i, finalX + j].GetComponent<Node> ();
					else
						final = null;
					
					if (final != null && !Physics.Raycast (CG.nodeMap [finalZ + i, finalX + j].transform.position, target, 
						    Vector3.Distance (CG.nodeMap [finalZ + i, finalX + j].transform.position, target), obstacleLayerMask)) {
						if (closestNode == null) {
							closestNode = final;
						} else if (Vector3.Distance (target, final.transform.position) < Vector3.Distance (target, closestNode.transform.position)) {
							closestNode = final;
						}

					} else {
						print (final == null);
					}
				}
			}
			final = closestNode;
		}

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

	public void SendAlert(EnemyMovement self, Vector3 detectedPos)
	{
		// y todas estas 100 lineas de codigo no se pueden resumir en 5? tienes un switch dentro de un switch, por que no hacer algo rolo
		// if self.idpart == enemiesInstage[i].idpart && self. idstage == enemiesInstage[i].idstage
		for (int i = 0; i < enemiesInStage.Count; i++) {
			if (enemiesInStage [i].enemyIDStage == self.enemyIDStage && enemiesInStage [i].enemyIDStagePart == self.enemyIDStagePart) {
				enemiesInStage [i].SendAlertToPosition (detectedPos);
			}
		}
	}
}
