using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour {


	public GameObject endGameCanvas;


	// Use this for initialization
	void Start () 
	{
		endGameCanvas.SetActive (false);		
	}
	
	// Update is called once per frame
	void Update () 
	{
		/*if (Input.GetKeyDown (KeyCode.T)) //Abrir menu de pausa
		{
			endGameCanvas.SetActive(true);
			Time.timeScale = 0f;
			Cursor.lockState = CursorLockMode.None;

		}
		if (Input.GetKeyDown (KeyCode.P)) //Cerrar menu de pausa
		{
			endGameCanvas.SetActive(false);
			Time.timeScale = 1f;
			Cursor.lockState = CursorLockMode.Locked;
		}*/
		
	}

	 public void RestartLevel()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		endGameCanvas.SetActive (false);
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.Locked;
	}

	 public void QuitToMainMenu()
	{
		//Aqui cambiaremos el codigo de arriba, pero con la escena del menu.
		SceneManager.LoadScene ("MainMenu");
		endGameCanvas.SetActive (false);
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.None;
	}



}
