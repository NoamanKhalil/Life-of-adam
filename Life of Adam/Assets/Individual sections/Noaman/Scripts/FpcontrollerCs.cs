using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpcontrollerCs : MonoBehaviour
{
    [Header("Pass in the camera here")]
	public GameObject camera;
	public float straffeVel;
    [Header("Speed of which player runs")]
	public float runVel = 36f;
	[Header("How high the player jumps")]
	public float jumpVel = 0.2f;
    [Header("Speed of which dies and returns to the start postion")]
	public float deathSmoothness;
    [Header("Speed of which player crouches")]
	public float coruchSmoothness;
	private float forwardVel;
	private Vector3 velocity;
	private Vector3 IVeloctiy;
    [Header("Transform for camera to move to while not crocuhing")]
    [SerializeField]
	private Transform initialCrouch;
    [Header("Transform for camera to move to while crocuhing")]
    [SerializeField]
	private Transform crouchVect;
	[SerializeField]
	private float walkVel = 8f;
	private BoxCollider myCollider;
    [Header("When playe is crocuhed")]
    [SerializeField]
    private Vector3 crouchPosSize;
    [Header("When playe is not crocuhed")]
    [SerializeField]
    private Vector3 startCrouchSize;
	Rigidbody rb;
	float forwardInput, straffeInput;
	float stamina;
	bool isPlaying;
	bool canJump;
	bool canDie;

	bool canCrouch;
	bool isGrounded;

	//if true do action 
	bool canPush;
	bool canMove;

	Vector3 coruchVelocity = Vector3.zero;
	Vector3 startPos;
	Quaternion startRot;


	void Start()
	{
		startPos = transform.position;
		startRot = camera.transform.rotation;
		rb = GetComponent<Rigidbody>();
		forwardInput = straffeInput = 0;
		isPlaying = false;
		canCrouch = true;
		forwardVel = walkVel;
		myCollider = GetComponent<BoxCollider>();
		canMove = true;
		canPush = false;
	}

	void Update()
	{
		if (Time.timeScale <= 1)
		{
			Run();
			if (canMove)
			{
				if (!canPush)
				{
					forwardInput = Input.GetAxis("Vertical") * forwardVel;
					straffeInput = Input.GetAxis("Horizontal") * forwardVel;
				}
				else if (canPush && Input.GetAxis("Vertical") < 0 || Input.GetAxis("Vertical") > 0)
				{
					//vector3.back
					forwardInput = Input.GetAxis("Vertical") * forwardVel;
				}
			}
			velocity = new Vector3(straffeInput, 0, forwardInput);
			velocity = transform.TransformDirection(velocity);
			rb.velocity = velocity;
			if (canJump & Input.GetKeyUp(KeyCode.Space))
			{
				rb.AddForce(new Vector3(0, jumpVel, 0), ForceMode.Impulse);
				canJump = false;
			}

			Crouch();
			//GroundCheck();
		}

	}
	/* ground check is used to make sure the player has a platform under it , if its not we can make the player fall*/
	void GroundCheck()
	{
		RaycastHit hit;
		float distance = 3f;
		Vector3 dirD = Vector3.down;
		Vector3 pos = transform.position;
		pos.x += 1;

		if (Physics.Raycast(pos, dirD, out hit, distance))
		{
			
			rb.constraints= RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezeRotation;
		}
		else
		{
			// make player fall 
			//rb.constraints = RigidbodyConstraints.;
			//rb.mass = 5000;
		}
	}
   /* Called when player crouched , initiated the coroutine to move back and forth to the crouch postion smoothly */
   /* we also adjust the colldier size so adam can go throguh small areas */
	void Crouch()
	{
		// on true
		if (canCrouch && Input.GetKeyDown(KeyCode.LeftControl))
		{
			canMove = false;
			Vector3 colliderPos = myCollider.transform.position;
			//Vector3 velocity = Vector3.zero;
			Debug.Log("Left control hit going down");
            //pos should be 0- 0.5 
			colliderPos.y = colliderPos.y - 0.5f;
			myCollider.transform.position = colliderPos;
            // mess with the collider size 
            myCollider.size = crouchPosSize;
			canCrouch = false;
            StopAllCoroutines();
			StartCoroutine(onCrouch(camera.transform, crouchVect.transform.position, coruchSmoothness));

		}
		//on false 
		else if (!canCrouch && Input.GetKeyDown(KeyCode.LeftControl))
		{
			canMove = false;
			Vector3 colliderPos = myCollider.transform.position;
            //pos should be 0+ 0.5
			colliderPos.y = colliderPos.y + 0.5f;
			myCollider.transform.position = colliderPos;
			myCollider.size = startCrouchSize;
			canCrouch = true;
            StopAllCoroutines();
			StartCoroutine(onCrouch(camera.transform, initialCrouch.transform.position, coruchSmoothness));
		}

		
	}
	public void setSpeed(float mySpeed)
	{
		forwardVel = mySpeed;
	}
    /* code for player to run*/
	void Run()
	{
		if (Input.GetKey(KeyCode.LeftShift) && stamina <= 0)
		{
			forwardVel = runVel;
			stamina -= 2;
		}
		else if (Input.GetKeyUp(KeyCode.LeftShift))
		{
			forwardVel = walkVel;
		}
	}

	public void setPush(bool push)
	{
		canPush = push;
	}
	void OnCollisionStay(Collision other)
	{
		canJump = true;
	}
    /* Called when player dies , initiated the coroutine to move back to the start postion smoothly */
	public void OnDie()
	{
		StopAllCoroutines();
		StartCoroutine(Die(this.transform, startPos, deathSmoothness));
	}


	private IEnumerator onCrouch(Transform transform, Vector3 position, float timeToMove)
	{
		Vector3 currentPos = camera.transform.position;
		float t = 0f;
		while (t < 1)
		{
			t += Time.deltaTime / timeToMove;
			transform.position = Vector3.Lerp(currentPos, position, t);

			if (camera.transform.position == position)
			{

				yield return null;
				Debug.Log("canmove thingie working ");
				canMove = true;
			}

			yield return null;
		}
	}

	private IEnumerator Die(Transform transform, Vector3 position, float timeToMove)
	{
		myCollider.enabled.Equals(false);
		rb.constraints = RigidbodyConstraints.FreezeAll;
		canMove = false;
		Vector3 currentPos = this.transform.position;
		float t = 0f;
		while (t < 1)
		{
			t += Time.deltaTime / timeToMove;
			transform.position = Vector3.Lerp(currentPos, position, t);
			canMove = false;
			//rb.velocity = Vector3.zero;

			if (this.transform.position == startPos)
			{
				canMove = true;
				myCollider.enabled.Equals(true);
				rb.constraints = RigidbodyConstraints.None;
				Debug.Log("lerping");
				rb.constraints= RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezeRotation;


				yield return null;
			}

			yield return null;
		}

	}
}