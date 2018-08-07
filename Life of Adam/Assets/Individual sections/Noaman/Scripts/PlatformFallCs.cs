using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlatformFallCs : MonoBehaviour
{
	[SerializeField]
	private GameObject player;
	private Rigidbody rb;
	[SerializeField]
	private float distToDrop;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//Debug.Log(Vector3.Distance(this.transform.position, player.transform.position));
		if (Vector3.Distance(this.transform.position, player.transform.position) <= distToDrop)
		{
	     rb.useGravity = true;
		 rb.constraints = RigidbodyConstraints.None;
		}
	}
}
