using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour {

    Vector3 startPosition;
    public float intensity;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {

        float x = startPosition.x;
        float y = intensity * Mathf.Sin(Time.time) + startPosition.y;
        float z = startPosition.z;

        transform.position = new Vector3(x, y, z);
    }


}
