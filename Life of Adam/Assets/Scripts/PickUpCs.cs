using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpCs : MonoBehaviour
{
    GameObject mainCam;
    bool isCarrying;
    GameObject carriedObj;
    public float dist;
    public float smooth;

	// Use this for initialization
	void Start ()
    {
		if (mainCam == null)
        {
            mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isCarrying)
        {
            carry(carriedObj);
            checkdrop();
        }
        else
        {
            pickup();
        }
	}

    void carry (GameObject o)
    {

    }
    void pickup ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            float x = Screen.width / 2;
            float y = Screen.height / 2;

            Ray ray = mainCam.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit r;
            if (Physics.Raycast(ray , out r))
            {
                // dummy script to access the object 
                PickMeCs p = r.collider.GetComponent<PickMeCs>();
               if ( r.collider.tag.Equals("Pick"))
                {
                    isCarrying = true;
                    carriedObj = p.gameObject;
                    p.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }

        }
    }
    void checkdrop ()
    {
        if (Input.GetKeyDown (KeyCode.E))
        {
            dropObj();
        }
    }

    void dropObj ()
    {
        isCarrying = false;
        carriedObj.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        carriedObj = null;
      }
}
