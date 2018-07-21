﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevel : MonoBehaviour
{
	public void LoadScene(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}

	public void ReloadCurrentScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void ExitGame()
	{
		Application.Quit();
	}

	public float Timescale
	{
		get { return Time.timeScale; }
		set
		{
			Time.timeScale = value;
			Time.fixedDeltaTime = value * 0.02f;
		}
	}
}
