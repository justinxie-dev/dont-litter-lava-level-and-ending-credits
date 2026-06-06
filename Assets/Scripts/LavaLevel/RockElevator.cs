using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RockElevator : MonoBehaviour
{
    private bool playerIsOnTheRockElevator = false;
    private Animator rockElevatorAnim;
    private Material rockElevatorMaterial;
    private AudioSource rockElevatorAudioSource;

    private void Awake()
    {
        rockElevatorAnim = GetComponent<Animator>();

        if (rockElevatorAnim == null)
        {
            Debug.LogError("Animator rockElevatorAnim is not found");
        }
    }
    void Start()
    {
        rockElevatorMaterial = GetComponent<Renderer>().material;
        rockElevatorAudioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        rockElevatorAnim.SetBool("PlayerLanded", playerIsOnTheRockElevator);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            playerIsOnTheRockElevator = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            playerIsOnTheRockElevator = false;
        }
    }

    private void PlaySound()
    {
        rockElevatorAudioSource.Play();
    }

    private void StopSound()
    {
        rockElevatorAudioSource.Stop();
    }
}
