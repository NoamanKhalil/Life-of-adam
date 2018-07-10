using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destory : MonoBehaviour {
    
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player") 
        {
            Destroy(this.gameObject);
        }
    }
}
