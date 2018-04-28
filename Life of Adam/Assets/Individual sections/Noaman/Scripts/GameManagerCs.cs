using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerCs : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject Buttons;
    public float timer = 10.0f;

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
        timer -= Time.deltaTime;
        //Debug.Log(timer);
        if (timer <=0 && !SceneA &&!SceneB )
        {
            videoPlayer.Pause();
            Buttons.SetActive(true);
        }

        if (SceneA&&timer<=0 )
        {
            Debug.Log("Scene A loaded ");
            SceneManager.LoadScene("SceneA");
        }
        if (SceneB && timer <= 0)
        {
            // put Game scene here
            Debug.Log("Scene B loaded ");
            SceneManager.LoadScene("TestScene");

        }

    }


    public void SceneAActive()
    {
        Buttons.SetActive(false);
        SceneA = true;
        timer = 10.0f;
        videoPlayer.Play();
    }
    public void SceneBActive()
    {
        Buttons.SetActive(false);
        SceneB = true;
        timer = 10.0f;
        videoPlayer.Play();
    }
}
