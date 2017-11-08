using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botton : MonoBehaviour {

    public Animator anim;

    //si cortar comunicaciones == true, se cortaran las comunicaciones Y NADA MAS
    public bool cortarComunicaciones;
    //eje en el que se movera el objeto
    public char eje;
    //unidades que se moveera el objeto
    public float movimiento;
    //objeto que se movera
    public GameObject objetoParaMover;


    private Vector3 posicionInicialObjeto;
    private bool pulsado, accionRealizada;

    void Awake()
    {
        if (objetoParaMover != null)
            posicionInicialObjeto = objetoParaMover.transform.position;
    }

    void FixedUpdate()
    {
        if (!accionRealizada && pulsado)
        {
            switch (eje)
            {
                case 'x':
                case 'X':



                    break;

            }

        }
    }

    
    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("e")) { anim.enabled = true; }
        pulsado = true;
        
    }
}
