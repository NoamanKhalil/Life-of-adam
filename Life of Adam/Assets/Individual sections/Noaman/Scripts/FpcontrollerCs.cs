using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpcontrollerCs : MonoBehaviour
{
	public GameObject camera;
	public LayerMask layer;
	public float inputDelay;
	public float rotationSpeed;
	public float Xaxis, YAxis;
	public float crouchVal;
	public float straffeVel;
	public float runVel = 36f;
	public float jumpVel = 0.2f;
	public float colliderSizeX, colliderSizeY, colliderSizeZ;
	public float crouchSmooth;
	public float deathSmoothness;
	public float coruchSmoothness;
	private float forwardVel;
	private Vector3 velocity;
	private Vector3 IVeloctiy;
    [SerializeField]
	private Transform initialCrouch;
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
		//initialCrouch = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y - crouchVal, camera.transform.localPosition.z);
		//crouchVect = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, camera.transform.localPosition.z);
		/*initialCrouch = new Vector3(camera.transform.position.x , camera.transform.position.y-crouchVal, camera.transform.position.z);
        crouchVect = new Vector3(camera.transform.position.x , camera.transform.position.y, camera.transform.position.z);*/
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

			//		Debug.Log(straffeInput);
			//transform.Translate(straffeInput, 0, forwardInput);
			velocity = new Vector3(straffeInput, 0, forwardInput);
			//velocity = transform.localToWorldMatrix * velocity ;
			velocity = transform.TransformDirection(velocity);
			//velocity.y = rb.velocity.y; 

			rb.velocity = velocity;
			// ahmed changes for testing 
			//rb.AddForce(velocity * 10);
			//rb.velocity = Vector3.ClampMagnitude(rb.velocity, forwardVel);
			if (canJump & Input.GetKeyUp(KeyCode.Space))
			{
				rb.AddForce(new Vector3(0, jumpVel, 0), ForceMode.Impulse);
				canJump = false;
			}

			Crouch();
			GroundCheck();
		}



	}

	void GroundCheck()
	{
		RaycastHit hit;
		float distance = 2f;
		Vector3 dirD = Vector3.down;
		Vector3 pos = transform.position;
		pos.x += 1;

		if (Physics.Raycast(pos, dirD, out hit, distance))
		{
			
			rb.constraints= RigidbodyConstraints.FreezePositionY|RigidbodyConstraints.FreezeRotation;
		}
		else
		{
			rb.constraints = ~RigidbodyConstraints.FreezePositionY;
			rb.mass = 5000;
		}
	}
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

		if (other.gameObject.tag.Equals("Ground"))
		{
			//isGrounded = true;
		}
	}
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