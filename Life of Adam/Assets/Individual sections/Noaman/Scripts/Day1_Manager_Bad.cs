using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Day1_Manager_Bad : MonoBehaviour
{

	public float timer;
	public bool blue;
	public bool red;

	void Awake()
	{
		blue = false;
		red = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if (blue == true &&red == true )
		{
			timer -= Time.deltaTime;
			if (timer <= 0)
			{
				SceneManager.LoadScene("VideoScene_Day2");
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
