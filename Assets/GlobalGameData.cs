using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameData : MonoBehaviour {

	public static GlobalGameData currentInstance;

	public float settingMouseSens;

	void Awake()
	{
		DontDestroyOnLoad (this.gameObject);
		settingMouseSens = 1f;
		currentInstance = this;
	}
	public void SetMouseSens(float arg)
	{
		settingMouseSens = arg;
	}


}
