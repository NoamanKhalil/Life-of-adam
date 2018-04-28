using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum State
{
    Patrol =0 , Chase =1 
};

public class TeacherBehaviourCs : MonoBehaviour
{

    private State mystate;
    public Transform[] pathToFollow;
    public int posPoint;
    public float speed;
    // Use this for initialization
    void Start ()
    {
        pathToFollow = GameObject.Find("wayPointParent").GetComponentsInChildren<Transform>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (posPoint < pathToFollow.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, pathToFollow[posPoint].position, Time.deltaTime * speed);
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
}
