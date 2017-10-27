using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelIntroAnimation : MonoBehaviour {

	public string levelNameDisplayedString;
	public string levelDescDisplayedString;

	public Text levelName;
	public Text levelDesc;
	public Image introLine;
	public CanvasGroup introCG;

	private Vector3 levelNameInitialPos;
	private Vector3 levelDescInitialPos;
	private Vector3 separationLineInitialPos;

	// Use this for initialization
	void Awake () {
		levelNameInitialPos = levelName.transform.localPosition;
		levelDescInitialPos = levelDesc.transform.localPosition;
		separationLineInitialPos = introLine.transform.localPosition;
		levelName.text = levelNameDisplayedString;
		levelDesc.text = levelDescDisplayedString;
	}
	IEnumerator LevelIntro()
	{
		introCG.alpha = 0;
		float animSpeed = 1.85f;
		float t = 0;
		yield return new WaitForSeconds (0.5f);
		while (t < 1) {
			t = Mathf.MoveTowards (t, 1, Time.deltaTime * animSpeed);
			introCG.alpha = t;
			levelName.transform.localPosition = levelNameInitialPos + Vector3.right * (1 - t) * 40;
			levelDesc.transform.localPosition = levelDescInitialPos + Vector3.left * (1 - t) * 40;
			yield return null;
		}
		yield return new WaitForSeconds (2f);
		while (t > 0) {
			t = Mathf.MoveTowards (t, 0, Time.deltaTime * animSpeed);
			introCG.alpha = t;
			levelName.transform.localPosition = levelNameInitialPos + Vector3.left * (1 - t) * 40;
			levelDesc.transform.localPosition = levelDescInitialPos + Vector3.right * (1 - t) * 40;
			yield return null;
		}
	}

	public void PlayIntroAnimation()
	{
		StartCoroutine ("LevelIntro");
	}
}
