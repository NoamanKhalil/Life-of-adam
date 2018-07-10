using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ObjectDestroy : MonoBehaviour {

    public AudioSource pianoAudio;
    public AudioClip[] pianoClips;
    public int index;


    // Use this for initialization
    void Start()
    {
        pianoAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (index == pianoClips.Length)
            index = 0;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "audio")
        {
            Destroy(other.gameObject);
            pianoAudio.clip = pianoClips[index];
            pianoAudio.Play();
            index++;
            Debug.Log("object destroyed");

        }

    }
}
