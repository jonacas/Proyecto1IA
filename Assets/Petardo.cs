using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petardo : MonoBehaviour {

    private const float DISTANCIA_A_JUGADOR = 2F;
    private const float TIEMPO_EXPLOSION = 3f;
    private const float RANGO_RUIDO = 100f;
    private const float FUERZA_LANZAMIENTO = 1000f;

    ParticleSystem sistemaParticulas;
    float tiempoLanzado;
    public GameObject modeloJugador;
    public GameObject camera;
    bool cuentaAtras;
    Rigidbody rb;

	// Use this for initialization
	void Awake () {
        sistemaParticulas = GetComponentInChildren<ParticleSystem>();
        modeloJugador = GameObject.Find("PlayerModel");
        rb = this.gameObject.GetComponent<Rigidbody>();
	}


    void Update()
    {
        if (cuentaAtras)
        {
            if (Time.time - (tiempoLanzado) >= TIEMPO_EXPLOSION)
            {
                print("BOOM");
                sistemaParticulas.Play();
                this.GetComponent<MeshRenderer>().enabled = false;
                StageData.currentInstance.SendNoise(this.transform.position, RANGO_RUIDO);
                cuentaAtras = false;
            }
        }

    }

    public void ComenzarCicloLanzamiento()
    {
        Vector3 dir = camera.transform.forward;
        sistemaParticulas.Stop();
        tiempoLanzado = Time.time;
        this.transform.position = modeloJugador.transform.position + dir * DISTANCIA_A_JUGADOR;
        this.GetComponent<MeshRenderer>().enabled = true;
        dir.y = dir.y * +5;
        rb.AddForce(dir * FUERZA_LANZAMIENTO);
        cuentaAtras = true;
    }
	
}
