using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {

	public List<GameObject> buttons;

	public CanvasGroup loadingCG;
	public CanvasGroup loadingTextCG;

	public CanvasGroup pauseMenuCG;

	public CanvasGroup settingsCG;
	public Slider settingsSlider;

	private float targetScaleX = 1.75f;
	private List<float> targetScales;
	private float animSpeed = 4f;
	private float menuFadeSpeed = 6.5f;

	private bool subMenuOpen = false;
	private bool blockInputs = false;
	private bool gamePaused = false;

	private bool fadeAnimationInProgress = false;

	private Vector3 loadingTextInitialPos;

	// Use this for initialization
	void Start () {
		targetScales = new List<float> { 1, 1, 1 };
		loadingTextInitialPos = loadingTextCG.transform.localPosition;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (gamePaused) {
				UnpauseGame ();
			} else {
				PauseGame ();
			}
		}
		if (!subMenuOpen && gamePaused) {
			for (int i = 0; i < buttons.Count; i++) {
				buttons [i].transform.localScale = new Vector3 (Mathf.MoveTowards(buttons[i].transform.localScale.x, 
					targetScales[i], Time.unscaledDeltaTime * animSpeed),1,1); 
			}
		}
	}
	public void UnpauseGame()
	{
		if (fadeAnimationInProgress)
			return;
		StartCoroutine ("FadeOut");
		gamePaused = false;
		Time.timeScale = 1;
	}
	public void PauseGame()
	{
		if (fadeAnimationInProgress)
			return;
		StartCoroutine ("FadeIn");
		gamePaused = true;
		Time.timeScale = 0;
		for (int i = 0; i < buttons.Count; i++) {
			buttons [i].transform.localScale = Vector3.one;
			targetScales [i] = 1;
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
		case 0: // Continue
			{
				UnpauseGame ();
				break;
			}
		case 1: // Settings
			{
				StartCoroutine ("OpenSettingsMenu");
				break;
			}
		case 2: // Quit
			{
				StartCoroutine ("BackToMainMenu");
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
	IEnumerator FadeIn()
	{
		pauseMenuCG.gameObject.SetActive (true);
		fadeAnimationInProgress = true;
		float t = 0;
		while (t < 1) {
			t = Mathf.MoveTowards (t, 1, Time.unscaledDeltaTime * menuFadeSpeed);
			pauseMenuCG.alpha = t;
			yield return null;
		}
		fadeAnimationInProgress = false;
	}
	IEnumerator FadeOut()
	{
		fadeAnimationInProgress = true;
		float t = 1;
		while (t > 0) {
			t = Mathf.MoveTowards (t, 0, Time.unscaledDeltaTime * menuFadeSpeed);
			pauseMenuCG.alpha = t;
			yield return null;
		}
		fadeAnimationInProgress = false;
		pauseMenuCG.gameObject.SetActive (false);
	}
	IEnumerator OpenSettingsMenu()
	{
		settingsCG.gameObject.SetActive (true);
		blockInputs = true;
		subMenuOpen = true;
		settingsSlider.value = GlobalGameData.currentInstance.settingMouseSens;
		float t = 0;
		while (t < 1) {
			t = Mathf.MoveTowards (t, 1, Time.unscaledDeltaTime * animSpeed * 2f);
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
			t = Mathf.MoveTowards (t, 0, Time.unscaledDeltaTime * animSpeed * 2f);
			settingsCG.alpha = t;
			yield return null;
		}
		blockInputs = false;
		subMenuOpen = false;
		settingsCG.gameObject.SetActive (false);
	}
	IEnumerator BackToMainMenu()
	{
		loadingCG.gameObject.SetActive (true);
		blockInputs = true;

		float t = 0;
		while (t < 1) {
			t = Mathf.MoveTowards (t, 1, Time.unscaledDeltaTime * animSpeed);
			loadingCG.alpha = t;
			yield return null;
		}
		// Cambiad la escena por la que toque, puse 1 por poner algo.
		AsyncOperation AO = SceneManager.LoadSceneAsync(0);
		AO.allowSceneActivation = false;

		t = 0;
		while (AO.progress < 0.9f || t < 1) {
			t = Mathf.MoveTowards (t, 1, Time.unscaledDeltaTime * animSpeed);
			loadingTextCG.transform.localPosition = loadingTextInitialPos + Vector3.left * 200 * (1 - t);		
			loadingTextCG.alpha = t;
			yield return null;
		}
		yield return new WaitForSecondsRealtime (0.25f);
		t = 1;
		while (t > 0) {
			t = Mathf.MoveTowards (t, 0, Time.unscaledDeltaTime * animSpeed);
			loadingTextCG.transform.localPosition = loadingTextInitialPos + Vector3.right * 200 * (1 - t);
			loadingTextCG.alpha = t;
			yield return null;

		}
		yield return new WaitForSecondsRealtime (0.25f);
		AO.allowSceneActivation = true;
		Time.timeScale = 1;
	}
}
