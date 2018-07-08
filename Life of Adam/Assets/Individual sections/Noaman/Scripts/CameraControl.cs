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

    // Use this for initialization
    void Start () 
	{
		Character = this.transform.parent.gameObject;
        isholding = false;
		day = GameObject.Find("LevelManager").GetComponent<Day1_Manager_Bad>();
	}
	void Update () 
	{
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
                if ( hit.collider.gameObject.tag == "Red"||hit.collider.gameObject.tag == "Blue")
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
                }
            }
           
        }
		// when a object can be dropped 
		else if (Input.GetKeyDown(KeyCode.E) && isholding == true &&day!=null)
        {

			if (canDrop== true)
			{
				//hit.collider.gameObject.GetComponent<Test>().setSlotActive();
				if (Vector3.Distance(this.transform.position, bluePlacePos.transform.position) < Dist && pickedObj.tag == "Blue")
				{
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
				else if (Vector3.Distance(this.transform.position, redPlacePos.transform.position) < Dist && pickedObj.tag == "Red")
				{
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
			}
			else if (pickedObj!=null)
			{
				fp.setSpeed(8.0f);
				Debug.Log("Object dropped");
				pickupPoint.GetComponentInChildren<Collider>().enabled = true;
				pickupPoint.GetComponentInChildren<Rigidbody>().useGravity = true;
				pickupPoint.transform.DetachChildren();
				pickedObj = null;
				isholding = false;
			}
	

            //isholding = false;
        }


		/*if (pickupPoint != null)
		{
			canDrop = true;
			isholding = true;
		}
		else
		{ 
			canDrop = true;
			isholding = true;
		}*/


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
