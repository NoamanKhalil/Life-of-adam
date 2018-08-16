using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(AudioSource))]
public class AudioEventCs : MonoBehaviour 
{
    public AudioClip myClip;
    private AudioSource myAudio;

    void Awake()
    {
        myAudio =GetComponent<AudioSource>();
        myAudio.clip = myClip;
    }

    private void OnTriggerEnter(Collider other)
    {
        myAudio.Play();
    }
}
