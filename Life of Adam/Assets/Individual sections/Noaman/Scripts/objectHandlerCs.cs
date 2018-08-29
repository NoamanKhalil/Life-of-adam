using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectHandlerCs : MonoBehaviour
{
    [Header("dist before the object falls and resets")]
    public float dropDist;
	Vector3 postiion;
	Quaternion rotation;

	// Use this for initialization
	void Start ()
	{
		postiion = this.transform.position;
		rotation = this.transform.rotation;
        dropDist = 5.0f;
		
	}
    void Update()
    {
        float dist = postiion.y - this.transform.position.y;
        if ( dist >dropDist )
        {
            Reset();
        }

    }

    public void Reset() 
	{
		this.transform.position = postiion;
		this.transform.rotation = rotation;
	}
}
