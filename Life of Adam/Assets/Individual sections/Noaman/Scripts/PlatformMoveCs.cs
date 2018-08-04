using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMoveCs : MonoBehaviour 
{
	[SerializeField]
	private Transform [] PatrolPoints;
	[SerializeField]
	//Counts down to 0 from value
	private float StopTime;
    [SerializeField]
	private float speed;
	private float tempStopTime;
	private int posPoint;
	// Use this for initialization
	void Start ()
	{
		tempStopTime = StopTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
		patrol(PatrolPoints);
	}


	void patrol(Transform[] arr)
	{
		if (posPoint <= arr.Length)
		{
			transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[posPoint].position, Time.deltaTime* speed);
		}

		if (this.transform.position== arr[posPoint].position)
		{
			StopTime -= Time.deltaTime;
			//Debug.Log("Debug of distnace  " +Vector3.Distance(transform.position, arr[posPoint].position));
			if (posPoint == arr.Length - 1 && StopTime<=0)
			{
				posPoint = 0;
				StopTime = tempStopTime;
				return;
			}
			if (StopTime <= 0)
			{
				posPoint++;
				StopTime = tempStopTime;
			}
		}	
	}
}
