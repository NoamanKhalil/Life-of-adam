using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpCs : MonoBehaviour
{
    GameObject mainCam;
    Transform pickedObj;

	// Use this for initialization
	void Start ()
    {
        if (mainCam == null)
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera");
            pickedObj = mainCam.GetComponent<CameraControl>().pickupPoint.transform;
        }
	}
	

    // works when hoevered over collider
    void OnMouseDown()
    {
        GetComponent<Rigidbody>().useGravity = false;
        this.transform.position = pickedObj.transform.position;
        this.transform.parent = pickedObj;
    }

    void OnMouseUp()
    {
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
    }


}
