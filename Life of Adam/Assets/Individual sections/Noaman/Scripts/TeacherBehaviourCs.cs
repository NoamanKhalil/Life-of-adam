using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum AiState
{
    Idle =0 , Chase =1 ,Patrol=2
};

[RequireComponent(typeof(MySteeringBehaviour))]
public class TeacherBehaviourCs : MonoBehaviour
{

    public MySteeringBehaviour steer;

    private AiState currentState;
    public Transform[] pathToFollow;
    public int posPoint;
    public float speed;
	public GameObject playerObj;
	public float Speed;
	public float turnSpeed;
	public int minDist;

	void Awake()
	{
		 steer= this.gameObject.AddComponent<MySteeringBehaviour>();
	}
    // Use this for initialization
    void Start ()
    {
        pathToFollow = GameObject.Find("wayPointParent").GetComponentsInChildren<Transform>();
    }
	
	// Update is called once per frame
	void Update ()
    {

		if (Vector3.Distance(this.transform.position, playerObj.transform.position) < minDist)
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
                steer.Seek(playerObj, turnSpeed, minDist, Speed);
                break;
			case AiState.Patrol:
				//Flee();
				patrol();
                break;
            
        }

    }


	void patrol()
	{
		if (posPoint < pathToFollow.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, pathToFollow[posPoint].position, Time.deltaTime* speed);
        }

        if (transform.position == pathToFollow[posPoint].position)
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
}
