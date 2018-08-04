using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectHandlerCs : MonoBehaviour
{

	Vector3 postiion;
	Quaternion rotation;

	// Use this for initialization
	void Start ()
	{
		postiion = this.transform.position;
		rotation = this.transform.rotation;
		
	}


    public void Reset() 
	{
		this.transform.position = postiion;
		this.transform.rotation = rotation;
	}
}
