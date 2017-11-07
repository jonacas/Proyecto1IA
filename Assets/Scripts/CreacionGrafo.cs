using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CreacionGrafo : MonoBehaviour {

	public int filas, columnas; //definen la cantidad de _nodos del grafo
	public float radioTest;
	/*
	 * 		COLUMNAS (X)
	 * F
	 * I
	 * L
	 * A
	 * S
	 * (Z)
	 * 
	 * */

	public GameObject GO_NodoBase;
	private Vector3 esquina; //esquina complementaria que define el area del grafo
    public GameObject[,] nodeMap;
	public GameObject GO_Esquina;
	public GameObject agua;
	public float incrementoX, incrementoZ;


	// Use this for initialization
	void Awake () {
		Collider[] collisions;

		Vector3 testPos = new Vector3();
		testPos.y = this.transform.position.y;

		int obstacleLayer = 1<<8;
        //mascara de cristal
        obstacleLayer = obstacleLayer | 1 << 9;
		int waterLayer = 1 << 4;
	
		GameObject[] vectorAux = new GameObject[8];
		GameObject aux;
		Node nodoActual;
        int num = 0; //para dar nombre a lons _nodos

		//agua.SetActive (false);
		nodeMap = new GameObject[filas, columnas]; //almacena los _nodos para asignar vecinos


		esquina = GO_Esquina.transform.position;

		incrementoX = (esquina.x - this.transform.position.x) / columnas - 1;
		incrementoZ = (esquina.z - this.transform.position.z) / filas - 1;

		for (int i = 0; i < filas; i++) {
			for (int j = 0; j < columnas; j++) {
				testPos.x = this.transform.position.x + incrementoX * j;
				testPos.z = this.transform.position.z + incrementoZ * i;

				//comprobamos si el nodo estaria dentro o tocando un obstaculo
				if (Physics.OverlapSphere (testPos, radioTest, obstacleLayer).Length > 0)
				{
					nodeMap [i, j] = null;
					continue;//si colisiona, es descartado
				}

				//si no, se instancia
				aux = GameObject.Instantiate(GO_NodoBase, testPos, this.transform.transform.rotation);


				//comprobacion de si el nodo está en el agua
				//agua.SetActive (true);
				if (Physics.OverlapSphere (testPos, radioTest, waterLayer).Length > 0)
					aux.GetComponent<Node> ().Water = true;

				nodeMap [i, j] = aux;
                aux.gameObject.name = "Nodo_" + System.Convert.ToString(++num);
			}
		}

		//recorremos el mapa de _nodos y asignamos los vecinos
		for (int i = 0; i < filas; i++) {
			for (int j = 0; j < columnas; j++) {
				if(nodeMap[i,j] == null)
					continue;

                Array.Clear(vectorAux, 0, vectorAux.Length);
				nodoActual = nodeMap [i, j].GetComponent<Node>();


                if (i > 0)
                { //fila sup

					if(comprobarAccesible(nodoActual.gameObject, nodeMap[i - 1, j]))
                    	vectorAux[0] = nodeMap[i - 1, j];
                    if (j > 1)
						if(comprobarAccesible(nodoActual.gameObject, nodeMap[i - 1, j - 1]))
                        	vectorAux[1] = nodeMap[i - 1, j - 1];
                    if (j < columnas - 1)
					if(comprobarAccesible(nodoActual.gameObject, nodeMap[i - 1, j + 1]))
                        	vectorAux[2] = nodeMap[i - 1, j + 1];
                }

                //fila inf
                if (i < filas - 1)
                {
					if(comprobarAccesible(nodoActual.gameObject, nodeMap[i + 1, j]))
                    	vectorAux[3] = nodeMap[i + 1, j];
                    if (j > 1)
						if(comprobarAccesible(nodoActual.gameObject, nodeMap[i + 1, j - 1]))
                        	vectorAux[4] = nodeMap[i + 1, j - 1];
                    if (j < columnas - 1)
						if(comprobarAccesible(nodoActual.gameObject, nodeMap[i + 1, j + 1]))
                        	vectorAux[5] = nodeMap[i + 1, j + 1];
                }

                if (j > 0)
					if(comprobarAccesible(nodoActual.gameObject, nodeMap[i, j - 1]))
                    	vectorAux[6] = nodeMap[i, j - 1];
                if (j < columnas - 1)
					if(comprobarAccesible(nodoActual.gameObject, nodeMap[i, j + 1]))
                    	vectorAux[7] = nodeMap[i, j + 1];

				nodoActual.GetComponent<Node>().SetVecinos(vectorAux);
			}
		}

		/*for (int i = 0; i < filas; i++) {
			for (int j = 0; j < columnas; j++) {
				if(nodeMap[i,j] == null)
					continue;

				nodoActual = nodeMap [i, j].GetComponent<Node>();

				for(int k = 0; i < 8; i++)
					vectorAux[k] = null;

				if (i > 0) { //fila sup
					vectorAux [0] = nodeMap [i - 1, j];
					if(j > 1)
						vectorAux [1] = nodeMap [i - 1, j-1];
					if(j < columnas - 1)
						vectorAux [2] = nodeMap [i - 1, j+1];
				}

				//fila inf
				if (i < filas - 1) {
					vectorAux [3] = nodeMap [i + 1, j];
					if (j > 1)
						vectorAux [4] = nodeMap [i + 1, j - 1];
					if (j < columnas - 1)
						vectorAux [5] = nodeMap [i + 1, j + 1];
				}

				if(j > 0)
					vectorAux[6] = nodeMap[i, j - 1];
				if(j < columnas - 1)
					vectorAux[7] = nodeMap[i, j + 1];

                Debug.Log(vectorAux[1].name);
				nodoActual.SetVecinos(vectorAux);
			}
		}*/
	}

	bool comprobarAccesible(GameObject a, GameObject b)
	{
		if (b == null)
			return false;
		return !Physics.Raycast (a.transform.position, b.transform.position -
		a.transform.position, Vector3.Distance (a.transform.position, b.transform.position));

	}
}
