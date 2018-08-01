using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpCs : MonoBehaviour
{
	public FpcontrollerCs fp;
	public GameObject redPlacePos;
	public GameObject bluePlacePos;
	public GameObject pickupPoint;
    public GameObject cam;


	private GameObject pickedObj;
    [SerializeField]
	private float Dist;
	private DayManagerBad day;

	bool isholding;
	bool canDrop;
	// Use this for initialization
	void Start()
	{
		fp = GetComponent<FpcontrollerCs>();

		isholding = false;

		if (GameObject.Find("LevelManager") != null)
		{
			day = GameObject.Find("LevelManager").GetComponent<DayManagerBad>();
		}
		else
		{
			day = null;
		}
	}

	void Update()
	{
		PickUp();
	}

	void PickUp()
	{
		//add pickup code 
		Vector3 fwd = cam.transform.TransformDirection(Vector3.forward);

		// when a object can be picked up 
		if (Input.GetKeyDown(KeyCode.E) && isholding == false && fp != null)
		{
			RaycastHit hit;
			if (Physics.Raycast(cam.transform.position, fwd, out hit, Mathf.Infinity))
			{
				Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * 10, Color.black);
				Debug.Log("Did Hit");
				if (hit.collider.gameObject.tag == "Red" || hit.collider.gameObject.tag == "Blue")
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
		else if (Input.GetKeyDown(KeyCode.E) && isholding == true && day != null)
		{
			//hit.collider.gameObject.GetComponent<Test>().setSlotActive();
			if (Vector3.Distance(cam.transform.position, bluePlacePos.transform.position) <= Dist && pickedObj.tag == "Blue" && canDrop == true)
			{
				Debug.Log("Is close to blue pos ");
				fp.setSpeed(8.0f);
				pickupPoint.GetComponentInChildren<Collider>().enabled = true;
				pickupPoint.GetComponentInChildren<Rigidbody>().useGravity = true;
				pickupPoint.transform.DetachChildren();
				//pickupPoint.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
				pickedObj.SetActive(false);
				pickedObj = null;
				bluePlacePos.GetComponent<PuzzleCs>().setSlotActive();
				isholding = false;
				day.setBlueTrue();
			}
			else if (Vector3.Distance(cam.transform.position, redPlacePos.transform.position) <= Dist && pickedObj.tag == "Red" && canDrop == true)
			{

				Debug.Log("Is close to red pos ");
				fp.setSpeed(8.0f);
				pickupPoint.GetComponentInChildren<Collider>().enabled = true;
				pickupPoint.GetComponentInChildren<Rigidbody>().useGravity = true;
				pickupPoint.transform.DetachChildren();
				//pickupPoint.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
				pickedObj.SetActive(false);
				pickedObj = null;
				redPlacePos.GetComponent<PuzzleCs>().setSlotActive();
				isholding = false;
				day.setRedTrue();
			}
			else if (pickedObj != null)
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

		if (pickupPoint != null && isholding == true)
		{
			pickedObj.transform.position = pickupPoint.transform.position;
		}
	}

	public void setCanDrop(bool drop)
	{

		canDrop = drop;

	}

	public bool isholdingCheck()
	{
		return isholding; 
	}
}