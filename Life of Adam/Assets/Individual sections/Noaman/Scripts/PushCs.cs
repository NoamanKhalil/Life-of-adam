using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushCs : MonoBehaviour {

	public FpcontrollerCs fp;
	public GameObject cam;
	public GameObject thingToPull;
	// Use this for initialization
	void Start ()
	{
		fp = GetComponent<FpcontrollerCs>();
	}
	
	// Update is called once per frame
	void Update () 
	{
        Vector3 fwd = cam.transform.TransformDirection(Vector3.forward);
		if (thingToPull == null)
		{
			RaycastHit hit;
			if (Physics.Raycast(cam.transform.position, fwd, out hit, Mathf.Infinity))
			{
				//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.black);
				//Debug.Log("Did Hit");
				if (hit.collider.gameObject.tag == "Move" && Vector3.Distance(cam.transform.position, hit.transform.position) <= 2 && Input.GetKey(KeyCode.Mouse0))
				{
				thingToPull = hit.transform.gameObject;
				thingToPull.AddComponent<FixedJoint>();
				thingToPull.GetComponent<FixedJoint>().connectedBody = GetComponentInParent<Rigidbody>();
				thingToPull.GetComponentInParent<Rigidbody>().mass = 100;
				fp.setPush(true);
					cam.GetComponentInParent<CameraControl>().canMoveCheck(false);
				Debug.Log("Started pushing ");

			    }
			}
		}
		else if (thingToPull != null &&(Input.GetKey(KeyCode.Mouse1)||Input.GetKey(KeyCode.Mouse0)))
		{
			thingToPull.GetComponent<Rigidbody>().mass = 10000;
			Destroy(thingToPull.GetComponent<FixedJoint>());
			thingToPull = null; 
			fp.setPush(false);
			cam.GetComponentInParent<CameraControl>().canMoveCheck(true);
			Debug.Log("stopped pushing ");
				
		}
	}
}
