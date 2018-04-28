using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Test : MonoBehaviour
{
    public float sphereRadius;
    public LayerMask layer;
    [SerializeField]
    private float Dist;
    [SerializeField]
    private GameObject otherObj;
    [SerializeField]
    private Text myDistText;
    [SerializeField]
    private string thisName;
    [SerializeField]
    private bool objective;

    // Use this for initialization
    void Start ()
    {
        objective = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //myDistText.text = thisName + " " + Vector3.Distance(this.transform.position, otherObj.transform.position) +"IsObjective complete :"+ objective;
        /*if (Vector3.Distance(this.transform.position, otherObj.transform.position) < Dist) 
        {
            objective = true;
        }
        else
        {
            objective = false;
        }*/

        // Play a noise if an object is within the sphere's radius.
        if (Physics.CheckSphere(transform.position, sphereRadius,layer))
        {
            Debug.Log("Object in area");
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, sphereRadius);
    }
}
