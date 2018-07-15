using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour 
{
	public FpcontrollerCs fp;
	public GameObject redPlacePos;
	public GameObject bluePlacePos;

    public float Sensitivity = 2f;
    public float Smoothness = 2f;
    public GameObject pickupPoint;

    private GameObject pickedObj;

	[SerializeField]
	private float Dist;

    Vector2 MouseControl;
	Vector2 Smoothing;
	GameObject Character;

    public float maxClamp;
    public float minClamp;

    bool isholding;
    bool canDrop;

	private Day1_Manager_Bad day;
	public Transform thingToPull;

    // Use this for initialization
    void Start () 
	{
		Character = this.transform.parent.gameObject;
        isholding = false;
		day = GameObject.Find("LevelManager").GetComponent<Day1_Manager_Bad>();
	}
	void Update () 
	{
		Push();

		Vector2 nd = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y"));

		nd =Vector2.Scale (nd, new Vector2 (Sensitivity * Smoothness, Sensitivity * Smoothness));
		Smoothing.x = Mathf.Lerp (Smoothing.x, nd.x, 1f / Smoothness);
		Smoothing.y = Mathf.Lerp (Smoothing.y, nd.y, 1f / Smoothness);
		MouseControl += Smoothing;
		MouseControl.y = Mathf.Clamp (MouseControl.y, minClamp, maxClamp);

		transform.localRotation = Quaternion.AngleAxis (-MouseControl.y, Vector3.right);
		Character.transform.localRotation = Quaternion.AngleAxis (MouseControl.x, Character.transform.up);

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

		// when a object can be picked up 
		if (Input.GetKeyDown(KeyCode.E) && isholding == false && fp != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, fwd, out hit, Mathf.Infinity))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) *10, Color.black);
                Debug.Log("Did Hit");
                if ( hit.collider.gameObject.tag == "Red"||hit.collider.gameObject.tag == "Blue" )
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
					fp.setSpeed(3.5f);

					Debug.Log("Picked object");
                }
            }
           
        }
		// when a object can be dropped 
		else if (Input.GetKeyDown(KeyCode.E) && isholding == true &&day!=null)
        {
				//hit.collider.gameObject.GetComponent<Test>().setSlotActive();
			if (Vector3.Distance(this.transform.position, bluePlacePos.transform.position) <= Dist && pickedObj.tag == "Blue" && canDrop == true)
				{
					Debug.Log("Is close to blue pos ");
					fp.setSpeed(8.0f);
					pickupPoint.GetComponentInChildren<Collider>().enabled = true;
					pickupPoint.GetComponentInChildren<Rigidbody>().useGravity = true;
					pickupPoint.transform.DetachChildren();
					//pickupPoint.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					pickedObj.SetActive(false);
					pickedObj = null;

					bluePlacePos.GetComponent<Test>().setSlotActive();
					isholding = false;
					day.setBlueTrue();
				}
			    else if (Vector3.Distance(this.transform.position, redPlacePos.transform.position) <= Dist && pickedObj.tag == "Red"&& canDrop == true)
				{

					Debug.Log("Is close to red pos ");
					fp.setSpeed(8.0f);
					pickupPoint.GetComponentInChildren<Collider>().enabled = true;
					pickupPoint.GetComponentInChildren<Rigidbody>().useGravity = true;
					pickupPoint.transform.DetachChildren();
					//pickupPoint.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					pickedObj.SetActive(false);
					pickedObj = null;
					redPlacePos.GetComponent<Test>().setSlotActive();
					isholding = false;
					day.setRedTrue();
				}
				else if (pickedObj!=null) 
				{
					//Debug.Log("Is dropping object ");

					fp.setSpeed(8.0f);
					Debug.Log("Object dropped");
					pickupPoint.GetComponentInChildren<Collider>().enabled = true;
					pickupPoint.GetComponentInChildren<Rigidbody>().useGravity = true;
					pickupPoint.transform.DetachChildren();
					pickedObj = null;
					isholding = false;
				}


        }

        if (pickupPoint !=null&&isholding ==true)
        {
            pickedObj.transform.position = pickupPoint.transform.position;
        }
    }
	void Push()
	{
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
		if (Input.GetKeyDown(KeyCode.F) && thingToPull == null)
		{
			RaycastHit hit;
			if (Physics.Raycast(transform.position, fwd, out hit, Mathf.Infinity))
			{
				Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.black);
				Debug.Log("Did Hit");
				if (hit.collider.gameObject.tag == "Move")
				{
					thingToPull = hit.transform;
					Debug.Log("Started pushing ");

				}
			}
		}
		else if (Input.GetKeyDown(KeyCode.F) && thingToPull != null)
		{
			thingToPull = null; 
			Debug.Log("stopped pushing ");
				
		}

		if(thingToPull!=null) 
		{
			   // line from crate to player
			   Vector3 D = transform.position - thingToPull.position; 
			   float dist = D.magnitude;
               // short blue arrow from crate to player
			   Vector3 pullDir = D.normalized; 
				//lose tracking if too far
			   if(dist>2) 
				{
				thingToPull=null; 
				}
			   // you have to be less than 1 m for the code to work
			   else if(dist>1) 
			   { 


				float fakeGrav = 10.0f;
			     // for fun, pull a little bit more if further away:
				 // (so, random, optional junk):
				 float pullForDist = (dist - 3) / 2.0f;
			     if(pullForDist>20)
					pullForDist=20;
			     fakeGrav += pullForDist;
			     // Now apply to pull force, using standard meters/sec converted
			     //    into meters/frame:
			     thingToPull.GetComponent<Rigidbody>().velocity += pullDir*(fakeGrav* Time.deltaTime);
			   }
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
