using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpcontrollerCs : MonoBehaviour
{
	public GameObject camera;
	public float inputDelay;
	public float rotationSpeed;
	public float Xaxis, YAxis ;
    public float crouchVal;
    public float straffeVel;
	public float runVel = 36f;
	public float jumpVel = 0.2f;
	public float colliderSizeX, colliderSizeY, colliderSizeZ;
	public float crouchSmooth;

	private float forwardVel;
	private Vector3 velocity;
	private Vector3 IVeloctiy;
	private Vector3 initialCrouch;
	private Vector3 crouchVect;
	[SerializeField]
	private float walkVel = 8f;
	private BoxCollider myCollider;

	Rigidbody rb;
	float forwardInput, straffeInput;
	float tempStamina;
	bool isPlaying;
	bool canJump;
    
    bool canCrouch;

   Vector3 coruchVelocity = Vector3.zero;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		forwardInput = straffeInput = 0;
		isPlaying = false;
        canCrouch = true;
		forwardVel = walkVel;
		myCollider = GetComponent<BoxCollider>();
		//initialCrouch = new Vector3(camera.transform.localPosition.x , camera.transform.localPosition.y-crouchVal, camera.transform.localPosition.z);
		//crouchVect = new Vector3(camera.transform.localPosition.x , camera.transform.localPosition.y, camera.transform.localPosition.z);
		initialCrouch = new Vector3(camera.transform.position.x , camera.transform.position.y-crouchVal, camera.transform.position.z);
        crouchVect = new Vector3(camera.transform.position.x , camera.transform.position.y, camera.transform.position.z);
	}

	void Update()
    { 
		Run();

		forwardInput = Input.GetAxis("Vertical") * forwardVel;
		straffeInput = Input.GetAxis("Horizontal") * forwardVel;
//		Debug.Log(straffeInput);
		//transform.Translate(straffeInput, 0, forwardInput);
		velocity = new Vector3(straffeInput, 0, forwardInput);
		//velocity = transform.localToWorldMatrix * velocity ;
		velocity = transform.TransformDirection(velocity);
		//velocity.y = rb.velocity.y; 

	    rb.velocity =  velocity;


		if (canJump & Input.GetKeyUp(KeyCode.Space))
		{
			rb.AddForce(new Vector3(0, jumpVel, 0), ForceMode.Impulse);
			canJump = false;
		}
		Crouch();

	}
	void Pull()
	{ 
	
	}


	void Crouch()
	{
		if (canCrouch&& Input.GetKeyDown(KeyCode.LeftControl) )
        {
            //Vector3 velocity = Vector3.zero;
            Debug.Log("Left control hit going down");
			//camera.transform.position =new Vector3(camera.transform.position.x , camera.transform.position.y-crouchVal, camera.transform.position.z);
			//camera.transform.position =Vector3.SmoothDamp (transform.position, initialCrouch,ref coruchVelocity, crouchSmooth);
			//myCollider.size = new Vector3(colliderSizeX, colliderSizeY-1, colliderSizeZ);
			myCollider.size = new Vector3(colliderSizeX, 1, colliderSizeZ);
            canCrouch = false;
        }
		else if (!canCrouch && Input.GetKeyDown(KeyCode.LeftControl))
        {
            //Vector3 velocity = Vector3.zero;
            Debug.Log("Left control hit going up");
			//.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + crouchVal, camera.transform.position.z);
			//camera.transform.position =Vector3.SmoothDamp (transform.position,crouchVect,ref coruchVelocity, crouchSmooth);
			//myCollider.size = new Vector3(colliderSizeX, colliderSizeY+1, colliderSizeZ);
			myCollider.size = new Vector3(colliderSizeX, 2, colliderSizeZ);
            canCrouch = true;
        }

		if (canCrouch)
		{
			if (Vector3.Distance (camera.transform.position,crouchVect) > 0.1)
			{ 
				//camera.transform.position =Vector3.SmoothDamp (camera.transform.position,crouchVect,ref coruchVelocity, crouchSmooth);
			}

		}
		else 
		{
			if (Vector3.Distance(camera.transform.position, initialCrouch) > 0.1)
			{
				//.transform.position = Vector3.SmoothDamp(camera.transform.position, initialCrouch, ref coruchVelocity, crouchSmooth);
			}
		}
	}


	public void setSpeed(float mySpeed)
	{
		forwardVel = mySpeed;
	}

	void Run()
	{
        if (Input.GetKey(KeyCode.LeftShift) && tempStamina <= 0)
		{
			forwardVel = runVel ;
		}
		if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			forwardVel = walkVel;
		}
	}


	void OnCollisionStay(Collision other)
	{
		canJump = true;
    }

}
