﻿using System.Collections;
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
	public bool green;
	public bool isHappy;
	public bool isTutorial;
	public bool isDayOne;
	public bool isDayTwo;
	public bool isDayThree;
	public Text timerTxt;
	public string sceneName;
	public PuzzleCs[] puzzle;
    public FpcontrollerCs player;

	// Update is called once per frame
	void Update () 
	{
		if (lives <= 0)
		{
			//timer = 1.0f;
			blue = true;
			red = true;
		}


        // if bad
		if (isDayOne && !isHappy)
		{
			if (blue)
			{
				timer -= Time.deltaTime;
				GetComponent<CanvasFadeCs>().FadeOut();
				if (timer <= 0)
				{
					SceneManager.LoadScene(sceneName);
				}
			}
		}
        if (isHappy&& isDayOne&&levelTimer<=0&&lives>0)
        {
            GetComponent<CanvasFadeCs>().FadeOut();
            resetLevel();
            //Debug.Log("You were right");
        }
        else if (isHappy && isDayOne&& lives <= 0)
        {
            timer -= Time.deltaTime;
            GetComponent<CanvasFadeCs>().FadeOut();
            if (timer <= 0)
            {
                SceneManager.LoadScene(sceneName);
            }
        }


		if (timerTxt != null && isHappy)
		{
			levelTimer -= Time.deltaTime;

			int minutes = Mathf.FloorToInt(levelTimer / 60F);
			int seconds = Mathf.FloorToInt(levelTimer - minutes * 60);
			string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
			timerTxt.text = timeString;
		}

		/*if  (levelTimer <= 0)
		{
			blue = true;
			red = true;
		}*/


		if (blue == true &&red == true&& !isTutorial )
		{
			timer -= Time.deltaTime;
			GetComponent<CanvasFadeCs>().FadeOut();
			if (timer <= 0)
			{
				SceneManager.LoadScene(sceneName);
			}
		}
        // day 1 happy (if the player sets red true)
		if (red == true && isHappy&&!isTutorial)
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
		levelTimer = 120;
		blue = false;
		red = false;
        for (int i = 0; i < puzzle.Length; i++)
        {
            puzzle[i].Reset();
        }
        player.OnDie();
        //puzzle[0].Reset();
		//puzzle[1].Reset();

    }
}