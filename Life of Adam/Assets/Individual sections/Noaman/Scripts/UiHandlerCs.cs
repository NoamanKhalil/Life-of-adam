using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHandlerCs : MonoBehaviour
{

    [SerializeField]
	private GameObject cam;
    [SerializeField]
	private float hitDist;
	[SerializeField]
	private GameObject pickUpText;
	[SerializeField]
	private GameObject pushText;

	// Use this for initialization
	void Start ()
	{
			pickUpText.SetActive(false);
			pushText.SetActive(false);
		
	}
	
	// Update is called once per frame
	void Update () 
	{

	    Vector3 fwd = cam.transform.TransformDirection(Vector3.forward);
	    RaycastHit hit;
		Debug.DrawRay(cam.transform.position, Vector3.forward, Color.black, Mathf.Infinity);

		if (Physics.Raycast(cam.transform.position, fwd, out hit, Mathf.Infinity) && pickUpText != null &&pushText!=null )
		{
			    if ((hit.collider.gameObject.tag == "Red" || hit.collider.gameObject.tag == "Blue" || hit.collider.gameObject.tag == "Pick") && pickUpText.activeSelf!=true)
				{
					pickUpText.SetActive(true);
					//text stuff here
				}
			    else if (hit.collider.gameObject.tag == "Move"&&pushText.activeSelf!=true)
				{
					pushText.SetActive(true);
					//text stuff for 
				}
			    else if (hit.collider.gameObject.tag == "Untagged")
				{
					pickUpText.SetActive(false);
					pushText.SetActive(false);
				}

		}

		
	}
}
