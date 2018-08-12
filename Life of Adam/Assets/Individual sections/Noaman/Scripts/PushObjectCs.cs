using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushObjectCs : MonoBehaviour
{
	// if true it is constriant if false its not constraint  
    [SerializeField]
	private bool posX;
	[SerializeField]
    private bool posY;
	[SerializeField]
    private bool posZ;
	private Rigidbody rb;

	// Use this for initialization
	void Start () 
	{

		rb = GetComponent<Rigidbody>();
		if ((rb.constraints & RigidbodyConstraints.FreezePositionX) == RigidbodyConstraints.FreezePositionX)
		{
			posX = true;
		}
		if ((rb.constraints & RigidbodyConstraints.FreezePositionY) == RigidbodyConstraints.FreezePositionY)
		{
			posY = true;
		}
		if ((rb.constraints & RigidbodyConstraints.FreezePositionZ) == RigidbodyConstraints.FreezePositionZ)
		{
			posZ = true;
		}
		rb.constraints = RigidbodyConstraints.FreezeAll;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void isPushing()
	{
		//all constraints removed 
		rb.constraints = RigidbodyConstraints.None;
		// while all are frozen (Not possible )
		if (posX == true && posY == true && posZ == true)
		{
			rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
		}
		//while x pos is frozen it will not be frozen again
		if (posX == false && posY == true && posZ == true)
		{
			rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ |RigidbodyConstraints.FreezeRotation ;
		}
		//while y pos is frozenit will not be frozen again
		if (posX == true && posY == false && posZ == true)
		{
            rb.constraints=RigidbodyConstraints.FreezePositionX |RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezeRotation;
		}
		//while z pos is frozen it will not be frozen again
		if (posX == true && posY == true && posZ == false)
		{
            rb.constraints=RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezeRotation;
		}
		//set specific constriants off 
		//~ inverts values IE : value A = 0000 0000 it will become 1111 1111 
        //rb.constraints & RigidbodyConstraints.FreezePositionZ) == RigidbodyConstraints.FreezePositionZ
	}
	public void notPushing()
	{
		rb.constraints = RigidbodyConstraints.FreezeAll;// set constriasnts back 
	}

}