using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

	public List<GameObject> buttons;

	public CanvasGroup loadingCG;
	public CanvasGroup loadingTextCG;

	public CanvasGroup settingsCG;
	public Slider settingsSlider;

	private float targetScaleX = 1.75f;
	private List<float> targetScales;
	private float animSpeed = 4f;

	private bool subMenuOpen = false;
	private bool blockInputs = false;

	private Vector3 loadingTextInitialPos;

	// Use this for initialization
	void Start () {
		targetScales = new List<float> { 1, 1, 1 };
		loadingTextInitialPos = loadingTextCG.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if (!subMenuOpen) {
			for (int i = 0; i < buttons.Count; i++) {
				buttons [i].transform.localScale = new Vector3 (Mathf.MoveTowards(buttons[i].transform.localScale.x, 
					targetScales[i], Time.deltaTime * animSpeed),1,1); 
			}
		}
	}

	public void MouseEnter(int index)
	{
		targetScales [index] = targetScaleX;
	}
	public void MouseExit(int index)
	{
		targetScales [index] = 1;
	}

	public void MouseClick(int index)
	{
		if (blockInputs)
			return;
		
		switch (index) {
		case 0: // Play
			{
				StartCoroutine ("LoadingScreen");
				break;
			}
		case 1: // Settings
			{
				StartCoroutine ("OpenSettingsMenu");
				break;
			}
		case 2: // Quit
			{
				Application.Quit ();
				break;
			}
		}
	}
	public void OnConfirmSettingsClicked()
	{
		StartCoroutine("CloseSettingsMenu");
	}
	public void SaveSliderValue()
	{
		GlobalGameData.currentInstance.SetMouseSens (settingsSlider.value);
	}
	IEnumerator OpenSettingsMenu()
	{
		settingsCG.gameObject.SetActive (true);
		blockInputs = true;
		subMenuOpen = true;
		settingsSlider.value = GlobalGameData.currentInstance.settingMouseSens;
		float t = 0;
		while (t < 1) {
			t = Mathf.MoveTowards (t, 1, Time.deltaTime * animSpeed * 2f);
			settingsCG.alpha = t;
			yield return null;
		}
		blockInputs = false;
	}
	IEnumerator CloseSettingsMenu()
	{
		blockInputs = true;
		float t = 1;
		while (t > 0) {
			t = Mathf.MoveTowards (t, 0, Time.deltaTime * animSpeed * 2f);
			settingsCG.alpha = t;
			yield return null;
		}
		blockInputs = false;
		subMenuOpen = false;
		settingsCG.gameObject.SetActive (false);
	}
	IEnumerator LoadingScreen()
	{
		loadingCG.gameObject.SetActive (true);
		blockInputs = true;

		float t = 0;
		while (t < 1) {
			t = Mathf.MoveTowards (t, 1, Time.deltaTime * animSpeed);
			loadingCG.alpha = t;
			yield return null;
		}
		// Cambiad la escena por la que toque, puse 1 por poner algo.
		AsyncOperation AO = SceneManager.LoadSceneAsync(1);
		AO.allowSceneActivation = false;

		t = 0;
		while (AO.progress < 0.9f || t < 1) {
			t = Mathf.MoveTowards (t, 1, Time.deltaTime * animSpeed);
			loadingTextCG.transform.localPosition = loadingTextInitialPos + Vector3.left * 200 * (1 - t);		
			loadingTextCG.alpha = t;
			yield return null;
		}
		yield return new WaitForSeconds (0.25f);
		t = 1;
		while (t > 0) {
			t = Mathf.MoveTowards (t, 0, Time.deltaTime * animSpeed);
			loadingTextCG.transform.localPosition = loadingTextInitialPos + Vector3.right * 200 * (1 - t);
			loadingTextCG.alpha = t;
			yield return null;
			
		}
		yield return new WaitForSeconds (0.25f);
		AO.allowSceneActivation = true;
	}
}
