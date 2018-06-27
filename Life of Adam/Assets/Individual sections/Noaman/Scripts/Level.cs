using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour 
{
	public string levelName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerStay(Collider other)
	{
		if (other.attachedRigidbody)
		{
			SceneManager.LoadScene(levelName);
            
		}

	}
}
