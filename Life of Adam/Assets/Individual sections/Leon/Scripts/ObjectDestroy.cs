using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ObjectDestroy : MonoBehaviour {
    
    public AudioClip[] clips;
    public int i;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "object")
        {
            Destroy(other.gameObject);
            
            
        }
    }
}
