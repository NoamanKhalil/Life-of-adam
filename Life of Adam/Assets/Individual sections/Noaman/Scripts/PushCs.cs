using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PushCs : MonoBehaviour
{
    [Header("This component helps in pushing objects , they need the pushibject component atatched to them and need to be tagged as 'Move'")]
	public FpcontrollerCs fp;
	public GameObject cam;
    [Header("To see if object is being pulled ")]
	public GameObject thingToPull;
    [Header("Distance before the object can be psuhed ")]
	public float dist;
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
			if (Physics.Raycast(cam.transform.position, fwd, out hit, dist)&& Input.GetKeyUp(KeyCode.Mouse0)&&thingToPull==null)
			{
				//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.black);
				//Debug.Log("Did Hit");
				if (hit.collider.gameObject.tag == "Move")
				{
					Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
	                Debug.Log("Did Hit");
	                //print("Found an object - distance: " + hit.distance);
					thingToPull = hit.transform.gameObject;
					thingToPull.AddComponent<FixedJoint>();
					thingToPull.GetComponent<FixedJoint>().connectedBody = GetComponentInParent<Rigidbody>();
					thingToPull.GetComponentInParent<Rigidbody>().mass = 1;
					thingToPull.GetComponent<PushObjectCs>().isPushing();
					fp.setPush(true);
					fp.setSpeed(8);
					cam.GetComponentInParent<CameraControl>().canMoveCheck(false);
					GetComponent<UiHandlerCs>().setRay(false);
					Debug.Log("Started pushing ");
			    }
			}
		}
		else if (thingToPull != null &&(Input.GetKeyUp(KeyCode.Mouse0)))
		{
			fp.setSpeed(8);
			thingToPull.GetComponent<Rigidbody>().mass = 10000;
			Destroy(thingToPull.GetComponent<FixedJoint>());
			thingToPull.GetComponent<PushObjectCs>().notPushing();
			thingToPull = null; 
			fp.setPush(false);
			cam.GetComponentInParent<CameraControl>().canMoveCheck(true);
			GetComponent<UiHandlerCs>().setRay(true);
			Debug.Log("stopped pushing ");
			//this.transform.rotation = Quaternion.identity;
				
		}
	}
}
