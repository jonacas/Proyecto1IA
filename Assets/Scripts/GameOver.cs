
//ESTE SCRIPT SE LE AÑADE A UN CANVAS QUE TENGA COMO HIJO SOLO UNA IMAGEN NEGRA QUE CUBRA TODA LA PANTALLA Y UN TEXT QUE MUESTRE EL MENSAJE CENTRADO DE GAMEOVER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	const string STATE_BEGIN = "begin", STATE_MIDDLE = "middle", STATE_END = "end";

	float gradientSpeed = 1.5f, timeGameOverAppear = 2, aux;
	string state;
	public bool dead; 
	Text gameOverText;
	Image gameOverImage;
	Color colorImage, colorText;

	// Use this for initialization
	void Awake () {
		state = STATE_BEGIN;
		dead = false;
		gameOverImage = GetComponentInChildren<Image> ();
		gameOverText = GetComponentInChildren<Text> ();
		colorText = gameOverText.color;
		colorImage = gameOverImage.color;
		colorText.a = 0;
		colorImage.a = 0;
		gameOverText.color = colorText;
		gameOverImage.color = colorImage;
	}
	
	// Update is called once per frame
	void Update () {
		if (dead) {
			switch (state) {
			case STATE_BEGIN:
				if (gameOverText.color.a < 1) {
					colorText.a += Time.deltaTime * gradientSpeed;
					colorImage.a += Time.deltaTime * gradientSpeed;
					gameOverText.color = colorText;
					gameOverImage.color = colorImage;
				}
				else
					state = STATE_MIDDLE;
				break;
			case STATE_MIDDLE:
				aux += Time.deltaTime;
				if (aux >= timeGameOverAppear) {
					aux = 0;
					state = STATE_END;
				}
				break;
			case STATE_END:
				colorText.a -= Time.deltaTime * gradientSpeed;
				//colorImage.a = gameOverImage.color.a - Time.deltaTime * gradientSpeed;
				gameOverText.color = colorText;
				//gameOverImage.color = colorImage;
				if (gameOverText.color.a <= 0)
					SceneManager.LoadScene (SceneManager.GetActiveScene().name);
				break;
			}
		}		
	}

	public void ExecuteGameOver()
	{
		dead = true;
	}
}
