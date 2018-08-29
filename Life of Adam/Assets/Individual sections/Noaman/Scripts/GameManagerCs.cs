using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerCs : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject buttons;
    public float mainTimer;
    public float goodTimer;
    public float badTimer;
    [Header("Give T Area name post-tutorial t area")]
    public string levelName;
    bool SceneA;
    bool SceneB;

	// Use this for initialization
	void Start ()
    {
        SceneA = false;
        SceneB = false;

	}
	
	// Update is called once per frame
	void Update ()
    {
        mainTimer -= Time.deltaTime;
        //Debug.Log(timer);
        if (mainTimer <=0 && !SceneA &&!SceneB )
        {
            videoPlayer.Pause();
            buttons.SetActive(true);
        }
        if (SceneA)
        {
            goodTimer -= Time.deltaTime;
            mainTimer = 10;
            if (goodTimer <= 0)
            {
            
                SceneManager.LoadScene(levelName);
            }
        }
        if (SceneB)
        {
            badTimer -= Time.deltaTime;
            mainTimer = 10;
            if (badTimer <= 0)
            {
                SceneManager.LoadScene(levelName);
            }
        }

    }


    public void SceneAActive()
    {
        buttons.SetActive(false);
        SceneA = true;
        mainTimer = 10.0f;
        videoPlayer.Play();
    }
    public void SceneBActive()
    {
        buttons.SetActive(false);
        SceneB = true;
        mainTimer = 10.0f;
        videoPlayer.Play();
    }
}
