using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Petardo : MonoBehaviour {

    private const float DISTANCIA_A_JUGADOR = 2F;
    private const float TIEMPO_EXPLOSION = 3f;
    private const float RANGO_RUIDO = 100f;
    private const float FUERZA_LANZAMIENTO = 1000f;
    private const float TIEMPO_LUZ = 0.5f;
    private const int INTENSIDAD_INICIAL_LUZ = 1000;

    ParticleSystem sistemaParticulas;
    float tiempoLanzado;
    public GameObject modeloJugador;
    public GameObject camera;
    bool cuentaAtras, luzEncendida;
    Rigidbody rb;
    Light luz;

	// Use this for initialization
	void Awake () {
        sistemaParticulas = GetComponentInChildren<ParticleSystem>();
        modeloJugador = GameObject.Find("PlayerModel");
        camera = GameObject.Find("Main Camera");
        rb = this.gameObject.GetComponent<Rigidbody>();
        luz = this.GetComponent<Light>();
        luz.enabled = false;
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
                luz.enabled = true;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1f, transform.position.z);
                rb.isKinematic = true;
                luzEncendida = true;
                luz.intensity = INTENSIDAD_INICIAL_LUZ;
            }
        }

        if (luzEncendida)
        {
            if (Time.time - (tiempoLanzado) >= (TIEMPO_EXPLOSION + TIEMPO_LUZ))
            {
                luz.enabled = false;
                luzEncendida = false;
            }
            else
            {
                luz.intensity = INTENSIDAD_INICIAL_LUZ - (1000 * (Time.time - (tiempoLanzado) - TIEMPO_EXPLOSION) / TIEMPO_LUZ);
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
        rb.isKinematic = false;
        rb.AddForce(dir * FUERZA_LANZAMIENTO);
        cuentaAtras = true;
    }
	
}
