using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEstrella{

    public const bool MAS_PRECISO = true, MAS_RAPIDO = false;

	public static List<Transform> FindPath(Node origin, Node destiny, int capacity, bool precission, bool manhattan)
    {
       // Debug.Log("INICIO de nodo " + origin.gameObject.name + " a ndodo " + destiny.gameObject.name);
		if (origin == destiny) {
			List<Transform> aux = new List<Transform> ();
			aux.Add(origin.gameObject.transform);
			return aux;
		}


        PriorityHeap abiertos = new PriorityHeap(capacity);
        List<Node> cerrados = new List<Node>();
        bool final = false;
        Node actualNode, oldNode;

        origin.Cost = 0;
        origin.Route = null;
        abiertos.Add(origin, origin.Cost);
        actualNode = null;
        while (!final)
        {
            oldNode = actualNode;
            actualNode = abiertos.ExtractMin();
            if (actualNode == null) //si el monticulo se vaica
            {
                actualNode = oldNode;
                break;
            }
            foreach (Pareja value in actualNode.ArrayVecinos)
            {
                if (actualNode.Cost + value.distancia < value.nodo.Cost)
                {
                    value.nodo.Cost = actualNode.Cost + value.distancia;

					if(manhattan)
						value.nodo.Estimated = value.nodo.Cost + heuristicaManhattan(value.nodo.transform.position, destiny.transform.position);
					else
                    	value.nodo.Estimated = value.nodo.Cost + Vector3.Distance(value.nodo.transform.position, destiny.transform.position);
                    value.nodo.Route = actualNode;
                    if (value.nodo.QueuePosition == Node.EN_LISTA_CERRADOS) //Si esta en la lista de cerrados, lo sacamos de la lista
                        cerrados.Remove(value.nodo);
                   /* if (!abiertos.DecreasePriority(value.nodo, value.nodo.Estimated))
                    {*/
                        abiertos.Add(value.nodo, value.nodo.Estimated);
                        //Debug.Log("Metido " + value.nodo.gameObject.name + " en abiertos con prioridad: " + value.nodo.Estimated);
                   /* }
                    else
                        Debug.Log("Decrecentado " + value.nodo.gameObject.name + " en abiertos con prioridad: " + value.nodo.Estimated);*/
                }
            }
            cerrados.Add(actualNode);
            //Debug.Log("Metido " + actualNode.gameObject.name + " en cerrados");
            actualNode.QueuePosition = Node.EN_LISTA_CERRADOS;
            //Debug.Log("Peek = " + abiertos.Peek().gameObject.name); 
            if (precission == false && abiertos.Peek() == destiny)
            {
                final = true;
            }
            else if (abiertos.Peek().Cost > destiny.Cost || abiertos.Peek() == null)
            {
                final = true;
            }
        }
        //Retreiving path
		List<Transform> path = new List<Transform>();
        actualNode = destiny;                   //Reciclamos actualNode para usarlo como auxiliar
        while (actualNode != null)
        {
			path.Add(actualNode.gameObject.transform);
            actualNode = actualNode.Route;
        }

        abiertos = null;
        cerrados.Clear();
        return path; // borrar mas tarde
    }

	private static float heuristicaManhattan(Vector3 origen, Vector3 destino)
	{
		//la suma de las diferencias componente a componente en un espacio en 2 dimensiones
		float diferencia = 0;
		diferencia += Mathf.Abs (destino.x - origen.x);
		diferencia += Mathf.Abs (destino.z - origen.z);
		return diferencia;
	}
}
