using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day1_Manager_Bad : MonoBehaviour
{

	public float timer;
	public bool blue;
	public bool red;

	public string sceneName;

	void Awake()
	{
		
	}

	// Update is called once per frame
	void Update () 
	{
		if (blue == true &&red == true )
		{
			timer -= Time.deltaTime;
			if (timer <= 0)
			{
				SceneManager.LoadScene(sceneName);
			}
		}

		
	}

	public void setBlueTrue()
	{
		blue = true;
	}
	public void setRedTrue()
	{	
		red = true;
	}
}
