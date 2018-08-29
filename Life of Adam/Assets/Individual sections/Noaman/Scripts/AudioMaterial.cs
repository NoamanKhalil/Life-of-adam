using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Sounds
{
    North, East, South, West
};
public class AudioMaterial : MonoBehaviour 
{
    [Header("Audio for this object ")]
    [SerializeField]
    private Sounds currentSound;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
