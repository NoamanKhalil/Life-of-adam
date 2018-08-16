using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class ItemDropAudioCs : MonoBehaviour 
{

    public AudioClip[] myClip;
    private AudioSource myAudio;
	// Use this for initialization
	void Awake ()
    {
        myAudio = GetComponent<AudioSource>();
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            
        }
    }
}
