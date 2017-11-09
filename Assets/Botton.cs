using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botton : MonoBehaviour {

    public Animator anim;
    //public Animator otroObjeto;

    //si cortar comunicaciones == true, se cortaran las comunicaciones Y NADA MAS
    public bool cortarComunicaciones;

    private bool pulsado;
    
    void OnTriggerStay(Collider other)
    {
        GameObject.Find("Pulsar").GetComponent<UnityEngine.UI.Text>().enabled = true;
        if(!pulsado)
            {
            if (Input.GetKeyDown("e")) { 
                anim.enabled = true;
                pulsado = true;
				StageData.currentInstance.PressedButton (this.gameObject.name);


			}
        }
        
    }
    void OnTriggerExit(Collider other) {
        GameObject.Find("Pulsar").GetComponent<UnityEngine.UI.Text>().enabled = false;

    }
}
