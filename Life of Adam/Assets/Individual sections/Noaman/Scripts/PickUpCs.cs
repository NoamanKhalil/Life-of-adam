using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickUpCs : MonoBehaviour
{
    [Header("Add player object here")]
    [SerializeField]
    private FpcontrollerCs fp;
    [Header("Add the place points here")]
    [SerializeField]
    private GameObject redPlacePos;
    [SerializeField]
    private GameObject bluePlacePos;
    [Header("Add the child object 'PickPoint'")]
    [SerializeField]
    private GameObject pickupPoint;
    [SerializeField]
    private GameObject cam;
    [Header("Object you picked")]
    [SerializeField]
	private GameObject pickedObj;
    [Header("Value to pickup object")]
    [SerializeField]
	private float Dist;
    [SerializeField]
    private LevelManagerCs day;
	private Rigidbody rb;

    [SerializeField]
    private AudioClip myClip;
    [SerializeField]
    private AudioSource myAudio;

	bool isholding;
	bool canDrop;
	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		fp = GetComponent<FpcontrollerCs>();

		isholding = false;

		if (GameObject.Find("LevelManager") != null)
		{
			day = GameObject.Find("LevelManager").GetComponent<LevelManagerCs>();
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
        RaycastHit hit;

		// when a object can be picked up 
        if (Physics.Raycast(cam.transform.position, fwd, out hit, Mathf.Infinity)&&!isholding)
		{

			if ((Input.GetMouseButtonDown(0)) && isholding == false && fp != null)
			{
				Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * 10, Color.black);
				//Debug.Log("Did Hit");
				if (hit.collider.gameObject.tag == "Red" || hit.collider.gameObject.tag == "Blue"||hit.collider.gameObject.tag == "Pick")
				{
					hit.collider.gameObject.GetComponent<Rigidbody>().useGravity = false;
					pickedObj = hit.collider.gameObject;
					//hit.collider.gameObject.GetComponent<Rigidbody>().constraints
					hit.collider.gameObject.transform.position = pickupPoint.transform.position;
					hit.collider.gameObject.transform.parent = pickupPoint.transform;
					isholding = true;
					canDrop = false;
					hit.collider.gameObject.AddComponent<FixedJoint>();
					hit.collider.gameObject.GetComponent<FixedJoint>().connectedBody =rb;
					//fp.setSpeed(15f);
					GetComponent<UiHandlerCs>().setRay(false);
					//pickupPoint.GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
					Debug.Log("Picked object");
				}
			}

		}
		// when a object can be dropped 
		else if ((Input.GetMouseButtonDown(0)) && isholding == true )
		{
            Debug.Log("trying to drop object");
			//Debug.Log(Vector3.Distance(this.transform.position, bluePlacePos.transform.position));
			//hit.collider.gameObject.GetComponent<Test>().setSlotActive();
			if (pickedObj.tag == "Blue"&& Vector3.Distance (this.transform.position, bluePlacePos.transform.position)<Dist&& bluePlacePos!=null)
			{
				Debug.Log("blue code called ");
                Rigidbody tempRb= GetComponentInChildren<Rigidbody>();
				//fp.setSpeed(4.0f);
				Destroy(pickupPoint.GetComponentInChildren<FixedJoint>());
				tempRb.useGravity = true;
				pickupPoint.transform.DetachChildren();
				tempRb.constraints  &= ~(RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionY| RigidbodyConstraints.FreezePositionZ) ;
				pickedObj.SetActive(false);
				pickedObj = null;
				bluePlacePos.GetComponent<PuzzleCs>().setSlotActive();
				bluePlacePos.GetComponent<PuzzleCs>().setSlotActive();
				isholding = false;
				day.setBlueTrue();
				GetComponent<UiHandlerCs>().setRay(true);
			}
			else if (pickedObj.tag == "Red" && Vector3.Distance (this.transform.position, redPlacePos.transform.position)<Dist&&redPlacePos!= null)
			{
				Debug.Log("red code called ");
				Rigidbody tempRb = GetComponentInChildren<Rigidbody>();
				//fp.setSpeed(8.0f);
                Destroy(pickupPoint.GetComponentInChildren<FixedJoint>());
				tempRb.useGravity = true;
				pickupPoint.transform.DetachChildren();
				tempRb.constraints  &= ~(RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionY| RigidbodyConstraints.FreezePositionZ) ;
				pickedObj.SetActive(false);
				pickedObj = null;
				redPlacePos.GetComponent<PuzzleCs>().setSlotActive();
				redPlacePos.GetComponent<PuzzleCs>().setSlotActive();
				isholding = false;
				day.setRedTrue();
				GetComponent<UiHandlerCs>().setRay(true);
			}
            else if (pickedObj != null &&isholding)
			{
				Debug.Log("Null object called ");
				//fp.setSpeed(8.0f);
				pickupPoint.GetComponentInChildren<Rigidbody>().constraints &= ~(RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionY| RigidbodyConstraints.FreezePositionZ) ;
                Destroy(pickupPoint.GetComponentInChildren<FixedJoint>());
				Debug.Log("Object dropped");
				pickupPoint.GetComponentInChildren<Collider>().enabled = true;
				pickupPoint.GetComponentInChildren<Rigidbody>().useGravity = true;
				pickupPoint.transform.DetachChildren();
				pickedObj = null;
				isholding = false;
				GetComponent<UiHandlerCs>().setRay(true);
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