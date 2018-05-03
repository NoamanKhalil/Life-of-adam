using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour 
{
    public float Sensitivity = 2f;
    public float Smoothness = 2f;
    public GameObject pickupPoint;

    private GameObject pickedObj;
    Vector2 MouseControl;
	Vector2 Smoothing;
	GameObject Character;

    public float maxClamp;
    public float minClamp;

    bool isholding;
    bool canDrop;
    // Use this for initialization
    void Start () 
	{
		Character = this.transform.parent.gameObject;
        isholding = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector2 nd = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		nd =Vector2.Scale (nd, new Vector2 (Sensitivity * Smoothness, Sensitivity * Smoothness));
		Smoothing.x = Mathf.Lerp (Smoothing.x, nd.x, 1f / Smoothness);
		Smoothing.y = Mathf.Lerp (Smoothing.y, nd.y, 1f / Smoothness);
		MouseControl += Smoothing;
		MouseControl.y = Mathf.Clamp (MouseControl.y, minClamp, maxClamp);
	//	MouseControl.x = Mathf.Clamp (MouseControl.y, -20f, 60f);

		transform.localRotation = Quaternion.AngleAxis (-MouseControl.y, Vector3.right);
		//transform.rotation = Quaternion.AngleAxis (-MouseControl.y, Vector3.right);
		Character.transform.localRotation = Quaternion.AngleAxis (MouseControl.x, Character.transform.up);
        //Character.transform.rotation = Quaternion.AngleAxis (MouseControl.x, Character.transform.up);

        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        if (Input.GetKeyDown(KeyCode.E) && isholding == false)
        {
            RaycastHit hit;
           // Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            /*GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = pickedObj.transform.position;
            this.transform.parent = pickedObj;*/
            if (Physics.Raycast(transform.position, fwd, out hit, Mathf.Infinity))
            {
                //Debug.DrawLine(ray.origin, hit.point, Color.yellow);
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) *10, Color.black);
                Debug.Log("Did Hit");
                if ( hit.collider.gameObject.tag == "Pick")
                {
                    hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    pickedObj = hit.collider.gameObject;
                    //hit.collider.gameObject.GetComponent<Rigidbody>().constraints
                    hit.collider.gameObject.transform.position = pickupPoint.transform.position;
                    hit.collider.gameObject.transform.parent = pickupPoint.transform;
                    hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    isholding = true;
                    canDrop = false;
                    hit.collider.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None; 
                    // show pick GUI
                }
            }
           
        }
        else if (Input.GetKeyDown(KeyCode.E) && isholding == true && canDrop == true)
        {
            
            pickupPoint.GetComponentInChildren<Collider>().enabled = true;
            pickupPoint.GetComponentInChildren<Rigidbody>().useGravity = true;
            pickupPoint.transform.DetachChildren();
            //pickupPoint.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            pickedObj.SetActive(false);
            pickedObj = null;

            

            isholding = false;
        }


        if (pickupPoint !=null&&isholding ==true)
        {
            pickedObj.transform.position = pickupPoint.transform.position;
        }
    }
    public void setCanDrop (bool drop)
    {

        canDrop = drop;
    }
    

    public bool isholdingCheck ()
    {
        return isholding;
    }
}
