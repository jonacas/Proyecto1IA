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
        if(!pulsado)
            {
            if (Input.GetKeyDown("e")) { 
                anim.enabled = true;
                pulsado = true;
				StageData.currentInstance.PressedButton (this.gameObject.name);


			}
        }
        
    }
}
