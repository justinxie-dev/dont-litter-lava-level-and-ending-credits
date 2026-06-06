using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampCollision : MonoBehaviour
{
    public GameObject giantBall;
    private Rigidbody rb;
    private AudioSource giantBallAudioSource;
    private bool ballReadyToRoll = false;
    private bool hasPlayed = false;
    private float percentDecrease = 1.0f;
    
    void Start()
    {
        rb = giantBall.GetComponent<Rigidbody>();
        giantBallAudioSource = giantBall.GetComponent<AudioSource>();
        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        if (ballReadyToRoll)
        {
            rb.AddForce(rb.velocity.normalized * 30.0f * percentDecrease);
            
            // Gradually decrease the force added to the giantBall
            if (percentDecrease > 0.0f)
            {
                percentDecrease -= 0.01f;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player" && rb.isKinematic == true)
        {
            rb.isKinematic= false;
            ballReadyToRoll = true;

            giantBallAudioSource.spatialBlend = 0.88f;
            giantBallAudioSource.Play();
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.transform == giantBall.transform)
        {
            giantBallAudioSource.Stop();
        }
    }
}
