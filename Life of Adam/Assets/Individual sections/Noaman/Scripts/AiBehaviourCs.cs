using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum AiState
{
    Idle =0 , Chase =1 ,Patrol=2
};

[RequireComponent(typeof(MySteeringBehaviour))]
public class AiBehaviourCs : MonoBehaviour
{
    [Header("Paste in the A object name here for enemy to follow path")]
    public string nameA;
    [Header("Paste in the B object name here for enemy to follow path")]
    public string nameB;
	public NavMeshAgent agent;
    public MySteeringBehaviour steer;
	public LayerMask layer;
    private AiState currentState;
    public Transform[] pathToFollowA;
    public Transform[] pathToFollowB;
    public int posPoint;
    [Header("MoveSpeed")]
    public float speed;
	public GameObject playerObj;
    [Header("TurnSpeed")]
	public float turnSpeed;
    [Header("MinDist before attacking player")]
	public float minDist;
    [Header("dist before ttack player")]
	public float attackDistance;
    public LevelManagerCs day;

	private bool isPathA;
    private bool isPathB;
    private AiAnimator myAnimator;
	void Awake()
	{
		 steer= this.gameObject.AddComponent<MySteeringBehaviour>();
        agent.autoBraking = false;
	}
    // Use this for initialization
    void Start ()
    {
        pathToFollowA = GameObject.Find(nameA).GetComponentsInChildren<Transform>();
        pathToFollowB = GameObject.Find(nameB).GetComponentsInChildren<Transform>();
		agent.autoBraking = false;
		isPathA = true;
		isPathB = false;
        myAnimator = GetComponent<AiAnimator>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		// stuff for collision avoidence 
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		//normalized vector between left & forward 
		Vector3 leftD = transform.TransformDirection(Vector3.left + Vector3.forward).normalized;
		//normalized vector between right & forward 
		Vector3 rightD = transform.TransformDirection(Vector3.right + Vector3.forward).normalized;

		RaycastHit hit;

		Debug.DrawLine(transform.position, transform.position + fwd.normalized * attackDistance, Color.red);
		Debug.DrawLine(transform.position, transform.position + leftD.normalized * attackDistance, Color.red);
		Debug.DrawLine(transform.position, transform.position + rightD.normalized * attackDistance, Color.red);



		if (Physics.Raycast(transform.position, fwd, out hit,attackDistance, layer, QueryTriggerInteraction.Ignore) ||
		    Physics.Raycast(transform.position, leftD, out hit, attackDistance, layer, QueryTriggerInteraction.Ignore) ||
		    Physics.Raycast(transform.position, rightD, out hit, attackDistance, layer, QueryTriggerInteraction.Ignore))
		{
			currentState= AiState.Chase; 
		}

		if (Vector3.Distance(this.transform.position, playerObj.transform.position) <= 1f)
		{

			Debug.Log("You just died");
			playerObj.GetComponent<FpcontrollerCs>().OnDie();
		    day.resetLevel();	
			currentState = AiState.Patrol;
		}

		if (Vector3.Distance(this.transform.position, playerObj.transform.position) <= minDist)
		{
			currentState= AiState.Chase;
		}
		else
		{
			currentState = AiState.Patrol;
		}


		switch (currentState)
        {
            
            case AiState.Idle:
				idle();
                break;
			case AiState.Chase:
				// Seek();
				chase();
                break;
			case AiState.Patrol:
				//Flee();
                if (!agent.pathPending && agent.remainingDistance<0.5f)
                {
                    if (isPathA)
                    {
                        patrol(pathToFollowA);
                    }
                    else if (isPathB)
                    {
                        patrol(pathToFollowB);
                    }
                }
			

                break;
            
        }

    }

	void chase()
	{
		agent.SetDestination(playerObj.transform.position);
        myAnimator.SetChase();
	}


	void patrol(Transform [] arr)
	{

        if (arr.Length == 0)
            return;
        myAnimator.SetPatrol();
        // Set the agent to go to the currently selected destination.
        agent.destination = arr[posPoint].position;
        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        posPoint = (posPoint + 1) % arr.Length;

	}

	void setA()
	{
		isPathA = true;
	}
	void setB()
	{
		isPathB = true;
	}

	void idle()
	{
		Debug.Log("Is Idle");
	}

}