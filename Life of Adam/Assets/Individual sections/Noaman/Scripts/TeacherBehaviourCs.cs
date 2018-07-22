using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum AiState
{
    Idle =0 , Chase =1 ,Patrol=2
};

[RequireComponent(typeof(MySteeringBehaviour))]
public class TeacherBehaviourCs : MonoBehaviour
{
	public NavMeshAgent agent;
    public MySteeringBehaviour steer;
	public LayerMask layer;
    private AiState currentState;
    public Transform[] pathToFollow;
    public int posPoint;
    public float speed;
	public GameObject playerObj;
	public float Speed;
	public float turnSpeed;
	public float minDist;
	public float attackDistance;

	void Awake()
	{
		 steer= this.gameObject.AddComponent<MySteeringBehaviour>();
	}
    // Use this for initialization
    void Start ()
    {
        pathToFollow = GameObject.Find("wayPointParent").GetComponentsInChildren<Transform>();
		 agent.autoBraking = false;
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
			//on hit chase player 
		}


		if (Vector3.Distance(this.transform.position, playerObj.transform.position) <= minDist)
		{
			currentState= AiState.Chase;
	//		Debug.Log("is chasing");
		}
		else
		{
			currentState = AiState.Patrol;
//			Debug.Log("is patroling");
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
				patrol();
                break;
            
        }

    }

	void chase()
	{
		//transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, Time.deltaTime* speed);
		agent.SetDestination(playerObj.transform.position);
		//transform.LookAt(playerObj.transform.position);
	}


	void patrol()
	{
		if (posPoint < pathToFollow.Length)
        {
            //transform.position = Vector3.MoveTowards(transform.position, pathToFollow[posPoint].position, Time.deltaTime* speed);
			agent.SetDestination(pathToFollow[posPoint].position);
			//transform.LookAt(pathToFollow[posPoint].position);
        }

		if (Vector3.Distance(transform.position, pathToFollow[posPoint].position) < 0.1f)
        {
            if (posPoint == pathToFollow.Length - 1)
            {
                posPoint = 0;
                return;
            }
            posPoint++;
        }
	}

	void idle()
	{
		Debug.Log("Is Idle");
	}

	/*void OnDrawGizmos()
	{
		// Draw a yellow sphere at the transform's position
		Gizmos.color = Color.green;
		Gizmos.DrawSphere(transform.position, minDist); 
	}*/
	}
