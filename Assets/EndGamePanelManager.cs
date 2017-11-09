using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGamePanelManager : MonoBehaviour {

	public CanvasGroup globalCG;
	public CanvasGroup fade1CG;
	public CanvasGroup fade2CG;

	public Text headerInfo;
	public Text subInfo;

	public string endgameHeader;
	public string endgameSubdesc;

	public static EndGamePanelManager currentInstance;

	private bool animationStarted = false;

	void Awake()
	{
		currentInstance = this;
	}
	public void EndGame()
	{
		if (animationStarted)
			return;
		animationStarted = true;
		headerInfo.text = endgameHeader;
		subInfo.text = endgameSubdesc;
		StartCoroutine ("FadeAndBackToMainMenu");
	}

	IEnumerator FadeAndBackToMainMenu()
	{
		float animSpeed = 0.75f;
		float t = 0;
		while (t < 1) {
			t += Time.deltaTime * animSpeed;
			globalCG.alpha = t;
			fade1CG.alpha = t;
			yield return null;
		}
		t = 0;
		animSpeed = 2f;
		while (t < 1) {
			t += Time.deltaTime * animSpeed;
			fade1CG.transform.localScale = new Vector3 (1, 0.4f + t, 1);
			yield return null;
		}
		yield return new WaitForSeconds (1.5f);
		t = 0;
		animSpeed = 1.25f;
		while (t < 1) {
			t += Time.deltaTime * animSpeed;
			fade2CG.alpha = t;
			yield return null;
		}
		yield return new WaitForSeconds (0.25f);
		SceneManager.LoadScene (0);
	}
}
