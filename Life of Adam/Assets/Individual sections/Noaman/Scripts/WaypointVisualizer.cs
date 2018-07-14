using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointVisualizer : MonoBehaviour 
{
    [SerializeField]
	float sphereRadius;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmos()
	{
		// Draw a yellow sphere at the transform's position
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, sphereRadius);
	}


	}
