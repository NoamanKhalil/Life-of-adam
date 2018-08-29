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
            green = true;
		}
        //------------------------------------------//

        // if bad && day one 
        if (isDayOne && !isHappy)
		{
			if (blue)
			{
                loadMyScene();
            }
		}
        // if bad && day two 
        else if (isDayTwo&&!isHappy)
        {
            if (blue&&red)
            {
                loadMyScene();
            }
        }
        // if bad && day three 
        else if (isDayThree && !isHappy)
        {
            if (blue && red&&green)
            {
                loadMyScene();
            }
        }
        //------------------------------------------//
        // if good && day one 
        if (isDayOne && isHappy)
        {
            if (red)
            {
                loadMyScene();
            }
        }
        // if good && day two 
        else if (isDayTwo && isHappy)
        {
            if (blue && red)
            {
                loadMyScene();
            }
        }
        // if good && day three 
        else if (isDayThree && isHappy)
        {
            if (blue && red && green)
            {
                loadMyScene();
            }
        }
        //------------------------------------------//
        //Only happens when happy 
        if (isHappy&& (isDayOne||isDayTwo||isDayThree)&&levelTimer<=0&&lives>0)
        {
            GetComponent<CanvasFadeCs>().FadeOut();
            resetLevel();

        }
        else if (isHappy&&(isDayOne || isDayTwo || isDayThree)&& lives <= 0)
        {
            loadMyScene();
        }

		if (timerTxt != null && isHappy)
		{
			levelTimer -= Time.deltaTime;

			int minutes = Mathf.FloorToInt(levelTimer / 60F);
			int seconds = Mathf.FloorToInt(levelTimer - minutes * 60);
			string timeString = string.Format("{0:0}:{1:00}", minutes, seconds);
			timerTxt.text = timeString;
		}

        //------------------------------------------//
        //Only happens when in tutorial
        if (isTutorial)
        {
            GameObject go = GameObject.Find("Wall");
            if (blue && red&&go!=null)
            {
                // when to disbale door 
                go.SetActive(false);
            }
        }

    }


    void loadMyScene ()
    {
        timer -= Time.deltaTime;
        GetComponent<CanvasFadeCs>().FadeOut();
        if (timer <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

	public void setTimerTrue()
	{
		isHappy = true;
	}
	
    public void setTimerFalse()
    {
        isHappy = false;
        levelTimer = 120.0f;
    }

    public void continueHappy()
    {
        levelTimer = 120.0f;
    }

	public void setBlueTrue()
	{
		blue = true;
	}
	public void setRedTrue()
	{	
		red = true;
	}
    public void setGreenTrue()
    {
        green = true;
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
        player.OnDie();;

    }
}
