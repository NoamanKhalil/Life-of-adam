using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpcontrollerCs : MonoBehaviour
{
	public GameObject camera;
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
	float stamina;
	bool isPlaying;
	bool canJump;
	bool canDie;

	bool canCrouch;

	//if true do action 
	bool canPush;
	bool canPull;
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
		initialCrouch = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y - crouchVal, camera.transform.localPosition.z);
		crouchVect = new Vector3(camera.transform.localPosition.x, camera.transform.localPosition.y, camera.transform.localPosition.z);
		/*initialCrouch = new Vector3(camera.transform.position.x , camera.transform.position.y-crouchVal, camera.transform.position.z);
        crouchVect = new Vector3(camera.transform.position.x , camera.transform.position.y, camera.transform.position.z);*/
		canMove = true;
		canPush = false;
		canPull = false;
	}

	void Update()
	{
		Run();

		if (Input.GetKeyDown(KeyCode.I))
		{
			OnDie();
		}


		if (canMove)
		{
			if (!canPull && !canPush )
			{
				forwardInput = Input.GetAxis("Vertical") * forwardVel;
				straffeInput = Input.GetAxis("Horizontal") * forwardVel;
			}
			else if (canPull && Input.GetAxis("Vertical") < 0)
			{
				//vector3.back
				forwardInput = Input.GetAxis("Vertical") * forwardVel;
			}
			else if (canPush && Input.GetAxis("Vertical") > 0)
			{
				//vector3.forward
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


		if (canJump & Input.GetKeyUp(KeyCode.Space))
		{
			rb.AddForce(new Vector3(0, jumpVel, 0), ForceMode.Impulse);
			canJump = false;
		}
		Crouch();

	}


	void Crouch()
	{
		if (canCrouch && Input.GetKeyDown(KeyCode.LeftControl))
		{
			Vector3 colliderPos = myCollider.transform.position;
			//Vector3 velocity = Vector3.zero;
			Debug.Log("Left control hit going down");
			//camera.transform.position =new Vector3(camera.transform.position.x , camera.transform.position.y-crouchVal, camera.transform.position.z);
			//camera.transform.position =Vector3.SmoothDamp (transform.position, initialCrouch,ref coruchVelocity, crouchSmooth);
			//myCollider.size = new Vector3(colliderSizeX, colliderSizeY-1, colliderSizeZ);
			colliderPos.y = colliderPos.y - 0.2f;
			myCollider.transform.position = colliderPos;
			myCollider.size = new Vector3(colliderSizeX, 1, colliderSizeZ);
			canCrouch = false;
		}
		else if (!canCrouch && Input.GetKeyDown(KeyCode.LeftControl))
		{
			Vector3 colliderPos = myCollider.transform.position;
			//Vector3 velocity = Vector3.zero;
			//Debug.Log("Left control hit going up");
			//.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y + crouchVal, camera.transform.position.z);
			//camera.transform.position =Vector3.SmoothDamp (transform.position,crouchVect,ref coruchVelocity, crouchSmooth);
			//myCollider.size = new Vector3(colliderSizeX, colliderSizeY+1, colliderSizeZ);
			colliderPos.y = colliderPos.y + 0.2f;
			myCollider.transform.position = colliderPos;
			myCollider.size = new Vector3(colliderSizeX, 2, colliderSizeZ);
			canCrouch = true;
		}


		//debug to find the sweet spot in the crouch 
		//Debug.Log(Vector3.Distance(camera.transform.localPosition, crouchVect));
		//Debug.Log(Vector3.Distance(camera.transform.localPosition, initialCrouch));


		if (canCrouch)
		{
			if (Vector3.Distance(camera.transform.localPosition, crouchVect) > 1)
			{
				//Debug.Log(Vector3.Distance(camera.transform.localPosition, crouchVect));
				Vector3 newPosition = camera.transform.localPosition;
				//camera.transform.localPosition =Vector3.SmoothDamp (camera.transform.position,crouchVect,ref coruchVelocity, crouchSmooth);
				newPosition.y = Mathf.SmoothDamp(camera.transform.localPosition.z, crouchVect.y, ref coruchVelocity.y, crouchSmooth);
				camera.transform.localPosition = newPosition;

			}

		}
		else
		{
			if (Vector3.Distance(camera.transform.localPosition, initialCrouch) > 1.85)
			{
				Vector3 newPosition = camera.transform.localPosition;
				//.transform.position = Vector3.SmoothDamp(camera.transform.position, initialCrouch, ref coruchVelocity, crouchSmooth);
				newPosition.y = Mathf.SmoothDamp(camera.transform.localPosition.z, initialCrouch.y, ref coruchVelocity.y, crouchSmooth);
				camera.transform.localPosition = newPosition;
			}
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
	public void setPull(bool pull)
	{
		canPull = pull;
	}

	void OnCollisionStay(Collision other)
	{
		canJump = true;
	}


	public void OnDie()
	{
		StopAllCoroutines();
		StartCoroutine(Die(this.transform, startPos, deathSmoothness));
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
				rb.constraints= RigidbodyConstraints.FreezePositionY;
				Debug.Log("lerping at 50");
				rb.constraints= RigidbodyConstraints.FreezeRotation;
				Debug.Log("lerping done");

				yield return null;
			}

			yield return null;
		}

	}
}