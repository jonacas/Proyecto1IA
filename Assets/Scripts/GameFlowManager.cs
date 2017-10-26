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
		if (Input.GetKeyDown (KeyCode.T)) 
		{
			endGameCanvas.SetActive(true);
			Time.timeScale = 0f;
		}
		if (Input.GetKeyDown (KeyCode.P)) 
		{
			endGameCanvas.SetActive(false);
			Time.timeScale = 1f;
		}
		
	}

	 public void RestartLevel()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		endGameCanvas.SetActive (false);
		Time.timeScale = 1f;
	}

	 public void QuitToMainMenu()
	{
		//Aqui cambiaremos el codigo de arriba, pero con la escena del menu.
		
	}



}
