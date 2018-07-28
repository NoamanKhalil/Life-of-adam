using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour 
{
	public string levelName;
	public CanvasFadeCs can;
	public float timer;
	private bool canProgress;
	// Use this for initialization
	void Start () 
	{
		canProgress = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (canProgress)
		{
			timer -= Time.deltaTime;
			if (timer <= 0)
			{ 
				SceneManager.LoadScene(levelName);
			}
		}
		
	}
	void OnTriggerStay(Collider other)
	{
		if (other.attachedRigidbody)
		{
			can.FadeOut();
			canProgress = true;
            
		}

	}
}
