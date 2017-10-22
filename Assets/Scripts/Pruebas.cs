using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pruebas : MonoBehaviour {

    public Material materialCamino, materialInicioFinal, materialBase;
    public int filas, columnas;

	List<Transform> camino;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (camino != null)
            {
                foreach (Transform n in camino)
                    n.gameObject.GetComponent<Renderer>().sharedMaterial = materialBase;
            }

            Node inicio, final;
            GameObject aux = null;
            inicio = final = null;
            while (aux == null)
            {
                aux = this.gameObject.GetComponent<CreacionGrafo>().nodeMap[Random.Range(0, columnas), Random.Range(0, filas)];
            }
            inicio = aux.GetComponent<Node>();
            aux = null;
            while (aux == null)
            {
                aux = this.gameObject.GetComponent<CreacionGrafo>().nodeMap[Random.Range(0, columnas), Random.Range(0, filas)];
            }
            final = aux.GetComponent<Node>();
            camino = AEstrella.FindPath(inicio, final, filas * columnas * 5, false, true);
            StageData.currentInstance.enemiesInStage[0].SetNewPath(camino);

            for (int i = 0; i < camino.Count; i++)
            {
                if (i == 0 || i == camino.Count - 1)
                    camino[i].gameObject.GetComponent<Renderer>().sharedMaterial = materialInicioFinal;
                else
                    camino[i].gameObject.GetComponent<Renderer>().sharedMaterial = materialCamino;
            }

            foreach (GameObject n in this.gameObject.GetComponent<CreacionGrafo>().nodeMap)
            {
                if (n != null)
                    n.GetComponent<Node>().Cost = float.PositiveInfinity;
            }
        }

    }
}
