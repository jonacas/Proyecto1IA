using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour {

	public GameObject player;
	public static StageData currentInstance;
	public List<EnemyMovement> enemiesInStage;
	public CreacionGrafo CG;

    private int obstacleLayerMask = 1 << 8;
    private bool cmunicationsEnabeled;
    public bool ComunicationsEnabeled
    {
        get;
        set;
    }

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

		Node closestNode = null;

		if (CG.nodeMap [initZ, initX] != null)
			inicio = CG.nodeMap [initZ, initX].GetComponent<Node> ();
		else
			inicio = null;

		if (inicio == null) {
			closestNode = null;
			for (int i = -1; i < 2; i++) {
				for (int j = -1; j < 2; j++) {
					if (CG.nodeMap [initZ + i, initX + j] != null)
						inicio = CG.nodeMap [initZ + i, initX + j].GetComponent<Node> ();
					else
						inicio = null;

					if (inicio != null && !Physics.Raycast (CG.nodeMap [initZ + i, initX + j].transform.position, target, 
						Vector3.Distance (CG.nodeMap [initZ + i, initX + j].transform.position, target), obstacleLayerMask)) {
						if (closestNode == null) {
							closestNode = inicio;
						} else if (Vector3.Distance (target, inicio.transform.position) < Vector3.Distance (target, closestNode.transform.position)) {
							closestNode = inicio;
						}

					}
				}
			}
			inicio = closestNode;
		}

		if (CG.nodeMap [finalZ, finalX] != null)
			final = CG.nodeMap [finalZ, finalX].GetComponent<Node> ();
		else
			final = null;

		if (final == null) {
			closestNode = null;
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

					}
				}
			}
			final = closestNode;
		}

		camino = AEstrella.FindPath(final, inicio, CG.filas * CG.columnas, false, true);

		foreach (GameObject n in CG.nodeMap)
		{
			if(n != null)
				n.GetComponent<Node>().Cost = float.PositiveInfinity;
		}

		return camino;
	}

	public void SendAlert(Vector3 detectedPos, int area, int stage)
	{
        if (ComunicationsEnabeled)
        {
            print("Sending alert...");
            for (int i = 0; i < enemiesInStage.Count; i++)
            {
                if (enemiesInStage[i].enemyIDStage == stage && enemiesInStage[i].enemyIDStagePart == area)
                {
                    enemiesInStage[i].SendAlertToPosition(detectedPos);
                }
            }
        }
	}
}
