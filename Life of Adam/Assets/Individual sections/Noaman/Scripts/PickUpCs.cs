using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpCs : MonoBehaviour
{
    GameObject mainCam;
    Transform pickedObj;
    bool isholding; 

	// Use this for initialization
	void Start ()
    {
        if (mainCam == null)
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera");
            pickedObj = mainCam.GetComponent<CameraControl>().pickupPoint.transform;
        }
	}

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isholding == false)
        {
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = pickedObj.transform.position;
            this.transform.parent = pickedObj;
            isholding = true;
        }
        else if (Input.GetMouseButtonDown(1) && isholding == true)
        {
            this.transform.parent = null;
            GetComponent<Rigidbody>().useGravity = true;
            isholding = false;
        }

        
    }


}
