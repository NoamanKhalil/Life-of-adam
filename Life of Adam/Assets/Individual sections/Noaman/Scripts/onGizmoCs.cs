using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onGizmoCs : MonoBehaviour
{
	public Color col;
	public float radius;


	void OnDrawGizmos()
	{
		// Draw a yellow sphere at the transform's position
		Gizmos.color = col;
		Gizmos.DrawSphere(transform.position, radius);
	}
}