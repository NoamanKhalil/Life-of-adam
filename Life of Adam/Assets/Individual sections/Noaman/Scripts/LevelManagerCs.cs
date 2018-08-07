using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManagerCs : MonoBehaviour
{
	public int lives;
	public float levelTimer;
	public float timer;
	public bool blue;
	public bool red;
	public bool isHappy;
	public Text timerTxt;
	public string sceneName;
	public PuzzleCs[] puzzle;

	// Update is called once per frame
	void Update () 
	{
		if (lives <= 0)
		{
			//timer = 1.0f;
			blue = true;
			red = true;
		}

		if (timerTxt != null && isHappy)
		{
			levelTimer -= Time.deltaTime;

			int minutes = Mathf.FloorToInt(levelTimer / 60F);
			int seconds = Mathf.FloorToInt(levelTimer - minutes * 60);
			string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
			timerTxt.text = timeString;
		}

		if  (levelTimer <= 0)
		{
			blue = true;
			red = true;
		}


		if (blue == true &&red == true )
		{
			timer -= Time.deltaTime;
			GetComponent<CanvasFadeCs>().FadeOut();
			if (timer <= 0)
			{
				SceneManager.LoadScene(sceneName);
			}
		}
		if (red == true && isHappy)
		{ 
			timer -= Time.deltaTime;
			GetComponent<CanvasFadeCs>().FadeOut();
			if (timer <= 0)
			{
				SceneManager.LoadScene(sceneName);
			}
		}

		
	}

	public void setTimerTrue()
	{
		isHappy = true;
	}
		

	public void setBlueTrue()
	{
		blue = true;
	}
	public void setRedTrue()
	{	
		red = true;
	}

	public void resetLevel()
	{
		lives--;
		levelTimer = 600;
		blue = false;
		red = false;
		puzzle[0].Reset();
		puzzle[1].Reset();
	}
}
