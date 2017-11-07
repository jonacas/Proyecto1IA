using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGenerator : MonoBehaviour {

    public Text texto;
    private string[] desaparecido = new string[6];
    private string[] alerta = new string[6];
    private string[] presiguiendo = new string[6];
    private float timer = 0;

    // Use this for initialization
    void Start () {

        alerta[0] = "He avistado en el sector ";
        alerta[1] = "Creo que he visto algo en el sector ";
        alerta[2] = "Hay algo aquí en el sector ";
        alerta[3] = "Me ha parecido ver algo, voy a investigar el sector ";
        alerta[4] = "Cuerpo desconocido detectado en sector ";
        alerta[5] = "¿Que demonios era eso? Voy a investigar en sector ";


        desaparecido[0] = "Hemos perdido al sospechoso";
        desaparecido[1] = "Se ha perdido la pis";
        desaparecido[2] = "El ladron se ha esfuamado";
        desaparecido[3] = "Lo hemos perdido, volved a vuestros puestos";
        desaparecido[4] = "El intruso ha desaparecido";
        desaparecido[5] = "Vuelvan a sus patruyas";



    }
	
	// Update is called once per frame
	void Update () {

        if (timer > 0) {

            timer = timer - Time.deltaTime;

            if (timer <= 0) {

                
            }

        }

    }

    public void detectadoText(int zone) {

        int i = Random.Range(0, 5);
        texto.text = alerta[i] + zone;

    }

    public void Perdido() {

        int i = Random.Range(0, 5);
        texto.text = desaparecido[i];

    }
}
