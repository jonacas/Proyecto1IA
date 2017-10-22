using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

[System.Serializable]
public struct Pareja
{
    public Node nodo;
    public float distancia;

    public Pareja(Node nodo, float distancia)
    {
        this.nodo = nodo;
        this.distancia = distancia;
    }
}

public class Node : MonoBehaviour
{

    public const int NO_ESTA_EN_LISTA_ABIERTOS = -1, EN_LISTA_CERRADOS = -2;

    public GameObject[] VecinosGameObjects;
    public List<Pareja> arrayVecinos;
    private int queuePosition;
    private float estimated;
    private Node route;
    private float cost;
	private bool water;
    private GameObject _gameObject = null;

    //variables para cola prioridad
    private float _prioridad;
    private int _indiceCola;

    public Node Route
    {
        get
        {
            return route;
        }

        set
        {
            route = value;
        }
    }

    public int QueuePosition
    {
        get
        {
            return queuePosition;
        }

        set
        {
            queuePosition = value;
        }
    }

    public float Cost
    {
        get
        {
            return cost;
        }

        set
        {
            cost = value;
        }
    }

    public float Estimated
    {
        get
        {
            return estimated;
        }

        set
        {
            estimated = value;
        }
    }

    public List<Pareja> ArrayVecinos
    {
        get
        {
            return arrayVecinos;
        }
    }

	public bool Water
	{
		get
		{
			return water;
		}

		set
		{
			water = value;
		}
	}

    public float prioridad
    {
        get;
        set;
    }

    public int indiceCola
    {
        get;
        set;
    }


    void Awake()
    {
        queuePosition = NO_ESTA_EN_LISTA_ABIERTOS;
        Cost = float.PositiveInfinity;
        
        arrayVecinos = new List<Pareja>();
			
    }

    public void SetVecinos(GameObject[] vecinos)
    {
        Node nodoActual;
        float distanciaActual;

        foreach (GameObject value in vecinos)
        {
            if (value == null)
                continue;
            nodoActual = value.GetComponent<Node>();
            distanciaActual = Vector3.Distance(transform.position, value.transform.position);

            if (Water || nodoActual.Water)
                distanciaActual *= 3;

            if (nodoActual != null)
            {
               ArrayVecinos.Add(new Pareja(nodoActual, distanciaActual));
            }
            else
            {
                Debug.Log("Error en setVecinos");
            }
        }
    }



}
