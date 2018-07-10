using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformCs : MonoBehaviour
{

    public Transform[] pathToFollow;
    public int posPoint;
    public float speed;
    public float pauseTimer;
    public float maxTime;
    // Use this for initialization
    void Start()
    {
        pathToFollow = GameObject.Find("wayPointParent").GetComponentsInChildren<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        patrol();
    }


    void patrol()
    {

        if (posPoint < pathToFollow.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, pathToFollow[posPoint].position, Time.deltaTime * speed);
        }

        if (transform.position == pathToFollow[posPoint].position)
        {
            pauseTimer -= Time.deltaTime;
            if (pauseTimer <= 0)
            {

                if (posPoint == pathToFollow.Length -1)
                {
                    posPoint = 0;
                    pauseTimer = maxTime;

                    return; 
                }

                posPoint++;

            }

        }
    }

}
