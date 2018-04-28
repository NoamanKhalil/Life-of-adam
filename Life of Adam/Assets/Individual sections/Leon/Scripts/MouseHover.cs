using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseHover : MonoBehaviour {

    public GameObject image;
    
    

     public void OnMouseOver()
    {
        image.SetActive(true);
        
        

    }

     public void OnMouseExit()
    {
        image.SetActive(false);
    }
}
