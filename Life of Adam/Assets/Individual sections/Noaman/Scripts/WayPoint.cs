using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    [Header("Assign AI for level here ")]
    [SerializeField]
    private GameObject myAi;
    [Header("Set Point of waypoint here")]
    [SerializeField]
    bool isPointA;
    [SerializeField]
    bool isPointB;
    [Header("Set Point A point 0 here")]
    [SerializeField]
    GameObject pointAPos;
    [Header("Set Point B point 0 here")]
    [SerializeField]
    GameObject pointBPos;
    void OnTriggerEnter(Collider other)
    {
        if (isPointA)
        {
            myAi.transform.position = pointAPos.transform.position;
            myAi.GetComponent<AiBehaviourCs>().setA();
        }
        if (isPointB)
        {
            myAi.transform.position = pointBPos.transform.position;
            myAi.GetComponent<AiBehaviourCs>().setB();
        }
    }
}
