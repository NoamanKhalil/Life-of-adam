using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Rigidbody))]
public class PushCs : MonoBehaviour
{
    [Header("This component helps in pushing objects , they need the pushibject component atatched to them and need to be tagged as 'Move'")]
	public FpcontrollerCs fp;
	[Header("secondary ray cast sratt")]
	public GameObject secondary;
    [Header("Primary ray cast sratt")]
	public GameObject cam;
    [Header("To see if object is being pulled ")]
	public GameObject thingToPull;
    [Header("Distance before the object can be pushed ")]
	public float dist;
    [Header("Secondary cast distance before the object can be pushed keep distnace amount 1.5-2.5 ")]
	public float secondaryDist;
	public GameObject textObj;
	// Use this for initialization
	void Start ()
	{
		fp = GetComponent<FpcontrollerCs>();
		textObj.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
        Vector3 fwd = cam.transform.TransformDirection(Vector3.forward);
		Vector3 sFwd = secondary.transform.TransformDirection(Vector3.forward);

				Debug.DrawRay(cam.transform.position ,cam.transform.TransformDirection(Vector3.forward)* dist, Color.black);
				Debug.DrawRay(secondary.transform.position ,secondary.transform.TransformDirection(Vector3.forward)* secondaryDist, Color.black);
		if (thingToPull == null)
		{
			RaycastHit hit;

			if (Physics.Raycast(cam.transform.position, fwd, out hit, dist) && Physics.Raycast(secondary.transform.position, sFwd, out hit, secondaryDist) && hit.collider.gameObject.tag == "Move" )
			{
	
					textObj.SetActive(true);
					if (Input.GetMouseButtonDown(0))
					{
						Debug.Log("Did Hit");
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
			else
			{
				textObj.SetActive(false);
			}
	
		}
		else if (thingToPull != null &&(Input.GetMouseButtonDown(0)))
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
		}
	}
}
