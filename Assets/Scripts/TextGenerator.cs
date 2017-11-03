using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextGenerator : MonoBehaviour {

    public Text texto;
    public string[] patrulla = new string[6];
    public string[] Informar = new string[6];
    public string[] alerta = new string[6];
    public string[] presiguiendo = new string[6];
    public bool isAlerta = false;
    public bool isInformar = false;
    public bool isPersiguiendo = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isAlerta)
        {

            int i = Random.Range(0, 5);
            texto.text = alerta[i];

        }
        else if (isInformar) {

            int i = Random.Range(0, 5);
            texto.text = Informar[i];

        }
        else if (isPersiguiendo)
        {

            int i = Random.Range(0, 5);
            texto.text = presiguiendo[i];

        }

    }
}
