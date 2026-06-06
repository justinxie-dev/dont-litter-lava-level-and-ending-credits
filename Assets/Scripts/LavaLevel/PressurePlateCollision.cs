using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Animator))]
public class PressurePlateCollision : MonoBehaviour
{
    private Animator pressurePlateAnim;
    private AudioSource pressurePlateAudioSource;
    public bool pressurePlateIsPressed = false;
    private Material pressurePlateMaterial;

    void Awake()
    {
        pressurePlateAnim = GetComponent<Animator>();

        if (pressurePlateAnim == null)
        {
            Debug.LogError("Animator pressurePlateAnim is not found");
        }
    }

    void Start()
    {
        pressurePlateAudioSource = GetComponent<AudioSource>();
        pressurePlateMaterial = GetComponent<Renderer>().material;

        if (pressurePlateAudioSource == null)
        {
            Debug.LogError("Audio Source pressurePlateAudioSource is not found");
        }

        if (pressurePlateMaterial == null)
        {
            Debug.LogError("Material pressurePlateMaterial is not found");
        }

        pressurePlateMaterial.color = new Color(255.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        if (pressurePlateIsPressed)
        {
            pressurePlateMaterial.color = new Color(0.0f, 100.0f, 0.0f);
        }
    }

    void FixedUpdate()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            pressurePlateIsPressed = true;
            pressurePlateAnim.SetBool("Pressed", pressurePlateIsPressed);
        }
    }

    void PlaySound()
    {
        pressurePlateAudioSource.Play();
    }

   
}
