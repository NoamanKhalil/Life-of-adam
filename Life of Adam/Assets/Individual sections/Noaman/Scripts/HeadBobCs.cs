using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Got from http://wiki.unity3d.com/index.php?title=Headbobber
//converted to c# from JS by noaman khalil 

public class HeadBobCs : MonoBehaviour 
{

	public float timer = 0.0f;
	public float bobbingSpeed = 0.18f;
	public float bobbingAmount = 0.2f;
	public float midpoint = 2.0f;

	float horizontal;
	float vertical;
	float waveslice;
	float translateChange;
	float totalAxes;

	
	// Update is called once per frame
	void Update ()
	{
		waveslice = 0.0f; 
	    horizontal = Input.GetAxis("Horizontal"); 
		vertical = Input.GetAxis("Vertical"); 
	    if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
		{
			timer = 0.0f; 
	    } 
	    else
		{ 
	       waveslice = Mathf.Sin(timer); 
	       timer = timer + bobbingSpeed; 
	       if (timer > Mathf.PI * 2)
			{ 
	          timer = timer - (Mathf.PI * 2); 
	        } 
	    } 
	    if (waveslice != 0) 
		{ 
			translateChange = waveslice* bobbingAmount;
			totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical); 
	       totalAxes = Mathf.Clamp (totalAxes, 0.0f, 1.0f); 
	       translateChange = totalAxes* translateChange;
			this.transform.localPosition = new Vector3(transform.localPosition.x, midpoint + translateChange,transform.localPosition.z); 
	    } 
	    else 
		{ 
			this.transform.localPosition = new Vector3(transform.localPosition.x, midpoint, transform.localPosition.z); 
	    } 	
	}
}
