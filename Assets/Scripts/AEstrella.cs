using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

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

        PriorityQueue abiertos = new PriorityQueue(capacity);
        List<Node> cerrados = new List<Node>();
        bool final = false;
        Node actualNode, oldNode, nodoOrigen, nodoDestino;

        nodoOrigen = origin.GetComponent<Node>();
        nodoDestino = destiny.GetComponent<Node>();

        nodoOrigen.Cost = 0;
        nodoOrigen.Route = null;
        abiertos.Encolar(nodoOrigen, nodoOrigen.Cost);
        actualNode = null;

        int contador = 0;
        while (!final)
        {
            //para evitar bucles (nunca se sabe...)
            if (contador > 100000)
            {
                return null;
            }

            contador++;
            oldNode = actualNode;
            actualNode = abiertos.Desencolar();
            if (actualNode == null) //si el monticulo se vaica
            {
                actualNode = oldNode;
                break;
            }

            foreach (Pareja value in actualNode.ArrayVecinos)
            {
                //si se llega al nodo con un coste mejor
                if (actualNode.Cost + value.distancia < value.nodo.Cost)
                {
                    //se actualiza el coste
                    value.nodo.Cost = actualNode.Cost + value.distancia;

                    //calculamos nueva prioridad del nodo
					if(manhattan)
						value.nodo.Estimated = value.nodo.Cost + heuristicaManhattan(value.nodo.gameObject.transform.position, destiny.transform.position);
					else//euclideana
                    	value.nodo.Estimated = value.nodo.Cost + Vector3.Distance(value.nodo.gameObject.transform.position, destiny.transform.position);

                    //actualizar ruta hasta el nodo
                    value.nodo.Route = actualNode;


                    if (value.nodo.QueuePosition == Node.EN_LISTA_CERRADOS) //Si esta en la lista de cerrados, lo sacamos de la lista
                        cerrados.Remove(value.nodo);

                    //comprobamos si ya esta en abiertos
                    if (abiertos.Contiene(value.nodo))
                    {
                        //si esta en la lista, reducimos su prioridad
                        abiertos.ActualizarPrioridad(value.nodo, value.nodo.Estimated);
                    }
                    else
                    {
                        //si no esta, se encola
                        abiertos.Encolar(value.nodo, value.nodo.Estimated);
                    }
                }
            }

            //el nodo que hemos recorrido entra en cerrado
            cerrados.Add(actualNode);
            //Debug.Log("Metido " + actualNode.gameObject.name + " en cerrados");
            actualNode.QueuePosition = Node.EN_LISTA_CERRADOS;
            //Debug.Log("Peek = " + abiertos.Peek().gameObject.name); 


			if (abiertos.NumElementos() == 0 || (precission == false && abiertos.Primero == nodoDestino))
			{
				final = true;
			}
			else if (abiertos.NumElementos() == 0 ||  abiertos.Primero.Cost > nodoDestino.Cost)
			{
				final = true;
			}
        }


        //Devolver camino
		List<Transform> path = new List<Transform>();
        actualNode = nodoDestino;                   //Reciclamos actualNode para usarlo como auxiliar
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
