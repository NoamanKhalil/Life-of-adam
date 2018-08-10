using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandlerCs : MonoBehaviour
{
	public GameObject myCanvas;
	public bool isPaused;

	// Use this for initialization
	void Start ()
	{
		myCanvas.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			if (!isPaused)
			{
				Cursor.visible = true;
		        Cursor.lockState = CursorLockMode.None;
				isPaused = true;
				myCanvas.SetActive(true);
				Time.timeScale = 0;
				return;
			}
			else if (isPaused)
			{
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
				isPaused = false;
				myCanvas.SetActive(false);
				Time.timeScale = 1;
				return;
			}
		}
		
	}


	public void unPause()
	{ 
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	    isPaused = false;
		myCanvas.SetActive(false);
		Time.timeScale = 1;
	}
}
