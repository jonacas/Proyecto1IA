using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlowManager : MonoBehaviour {


	public GameObject endGameCanvas;

	public static GameFlowManager currentInstance;

	void Awake()
	{
		currentInstance = this;
	}

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

	public void ShowGameOver()
	{
		endGameCanvas.SetActive(true);
		Time.timeScale = 0f;
		Cursor.lockState = CursorLockMode.None;
	}
		

	 public void RestartLevel()
	{
		endGameCanvas.SetActive (false);
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.Locked;
		SceneManager.LoadScene (1);
	}

	 public void QuitToMainMenu()
	{
		//Aqui cambiaremos el codigo de arriba, pero con la escena del menu.
		endGameCanvas.SetActive (false);
		Time.timeScale = 1f;
		Cursor.lockState = CursorLockMode.None;
		SceneManager.LoadScene ("MainMenu");

	}



}
