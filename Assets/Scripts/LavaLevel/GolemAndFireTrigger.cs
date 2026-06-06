using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GolemAndFireTrigger : MonoBehaviour
{
    private AudioSource rockPlatformAudioSource;
    private bool playSound = false;
    private bool stopRepeatedPlaysNow = false;

    void Start()
    {
        rockPlatformAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playSound)
        {
            if(!stopRepeatedPlaysNow)
            {
                rockPlatformAudioSource.Play();
                stopRepeatedPlaysNow = true;
            }
            
        } 
        else
        {
            rockPlatformAudioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            playSound = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            playSound = false;
            stopRepeatedPlaysNow = false;
        }
    }
}
