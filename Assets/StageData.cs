using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour {

	public GameObject player;
	public static StageData currentInstance;

	public List<EnemyMovement> enemiesInStage;
	public List<EnemyMovement> enemiesInStage2;
	public List<EnemyMovement> enemiesInStage3;


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

		int initX = (int) Mathf.Round(requester.transform.position.x / CG.incrementoX);
		int initZ = (int) Mathf.Round(requester.transform.position.z / CG.incrementoZ);
		int finalX = (int) Mathf.Round(target.transform.position.x / CG.incrementoX);
		int finalZ = (int) Mathf.Round(target.transform.position.z / CG.incrementoZ);

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

	public void SendAlert(EnemyMovement self, Transform detectedPos)
	{
		if (self.enemyIDStage == 1 && self.enemyIDStagePart == 1) 
		{
			enemiesInStage[0].SetState (EnemyMovement.EnemyState.Alert);
			List<Transform> newRoute = GetPathToTarget (enemiesInStage[0].transform, detectedPos);
			enemiesInStage[0].SetNewPath (newRoute);
			print ("entered in alert state");
		}
		switch (self.enemyIDStage)
		{
			case 1: // En este caso, va directamente a por el jugador: activara el dialogo de alerta,
			{		// Pero como no hay nadie mas, pues le perseguira.
				enemiesInStage[0].SetState (EnemyMovement.EnemyState.Alert);
				List<Transform> newRoute = GetPathToTarget (enemiesInStage[0].transform, detectedPos);
				enemiesInStage[0].SetNewPath (newRoute);
				print ("entered in alert state");
				break;
			}
			case 2: //Tiene tres zonas
			{
				switch (self.enemyIDStagePart) 
				{
					case 1:
					{	
						for (int i = 0; i < enemiesInStage2.Count; i++) 
						{
							if (enemiesInStage2 [i].enemyIDStagePart == 1) 
							{
								enemiesInStage2[i].SetState (EnemyMovement.EnemyState.Alert);
								List<Transform> newRoute = GetPathToTarget (enemiesInStage2[i].transform, detectedPos);
								enemiesInStage2[i].SetNewPath (newRoute);
							}
						}
						break;
					}
					case 2: 
					{
						for (int i = 0; i < enemiesInStage2.Count; i++) 
						{
							enemiesInStage2[i].SetState (EnemyMovement.EnemyState.Alert);
							List<Transform> newRoute = GetPathToTarget (enemiesInStage2[i].transform, detectedPos);
							enemiesInStage2[i].SetNewPath (newRoute);
						}
						break;
					}
					case 3:
					{
						for (int i = 0; i < enemiesInStage2.Count; i++) 
						{
							enemiesInStage2[i].SetState (EnemyMovement.EnemyState.Alert);
							List<Transform> newRoute = GetPathToTarget (enemiesInStage2[i].transform, detectedPos);
							enemiesInStage2[i].SetNewPath (newRoute);
						}
						break;
					}

				}
				break;
			}
			case 3: // Tiene cuatro zonas
			{
				switch (self.enemyIDStagePart) 
				{
				case 1:
					{	
						for (int i = 0; i < enemiesInStage3.Count; i++) 
						{
							enemiesInStage3[i].SetState (EnemyMovement.EnemyState.Alert);
							List<Transform> newRoute = GetPathToTarget (enemiesInStage3[i].transform, detectedPos);
							enemiesInStage3[i].SetNewPath (newRoute);
						}
						break;
					}
				case 2: 
					{
						for (int i = 0; i < enemiesInStage3.Count; i++) 
						{
							enemiesInStage3[i].SetState (EnemyMovement.EnemyState.Alert);
							List<Transform> newRoute = GetPathToTarget (enemiesInStage3[i].transform, detectedPos);
							enemiesInStage3[i].SetNewPath (newRoute);
						}
						break;
					}
				case 3:
					{
						for (int i = 0; i < enemiesInStage3.Count; i++) 
						{
							enemiesInStage3[i].SetState (EnemyMovement.EnemyState.Alert);
							List<Transform> newRoute = GetPathToTarget (enemiesInStage3[i].transform, detectedPos);
							enemiesInStage3[i].SetNewPath (newRoute);
						}
						break;
					}
				case 4:
					{
						for (int i = 0; i < enemiesInStage3.Count; i++) 
						{
							enemiesInStage3[i].SetState (EnemyMovement.EnemyState.Alert);
							List<Transform> newRoute = GetPathToTarget (enemiesInStage3[i].transform, detectedPos);
							enemiesInStage3[i].SetNewPath (newRoute);
						}
						break;
					}
				}
				break;

			}
		}


	}





		
}
