using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class TerminatorTrigger : MonoBehaviour
{
    public GameObject terminator;
    public GameObject warrior;
    public GameObject terminatorIsNearText;
    public GameObject terminatorIsNearMouseRightClickText;
    public GameObject terminatorIsNearMouseRightClickImage;
    public GameObject forcePush;
    private Animator terminatorAnim;
    private Animator warriorAnim;
    private NavMeshAgent terminatorAgent;
    private WarriorStatusController warriorStatusController;
    private Rigidbody terminatorRB;
    private AudioSource terminatorIsNearAudioSource;
    private AudioSource forcePushAudioSource;
    private bool buttonPressed = false;
    private bool forceAnimationCompleted = false;
    private Vector3 forceDirection;
    private bool addForce = false;
    private float percentDecrease = 1.0f;
    public bool canPlayRobotSounds = false;
    private bool pressedForce = false;

    void Start()
    {
        warriorAnim = warrior.GetComponent<Animator>();
        warriorStatusController = warrior.GetComponent<WarriorStatusController>();
        terminatorAnim = terminator.GetComponent<Animator>();
        terminatorRB = terminator.GetComponent<Rigidbody>();
        terminatorIsNearAudioSource = GetComponent<AudioSource>();
        forcePushAudioSource = forcePush.GetComponent<AudioSource>();

        // Terminator is handled by animation at start instead of with physics
        terminatorRB.isKinematic = true;
        terminatorAgent = terminator.GetComponentInParent<NavMeshAgent>();

        terminatorIsNearText.SetActive(false);
        terminatorIsNearMouseRightClickImage.SetActive(false);
        terminatorIsNearMouseRightClickText.SetActive(false);
    }

    void Update()
    {
        // Let this trigger sphere move with the Terminator
        transform.position = terminator.transform.position;

        if (warriorStatusController.canPunch)
        {
            forceAnimationCompleted = warriorAnim.GetCurrentAnimatorStateInfo(0).length > warriorAnim.GetCurrentAnimatorStateInfo(0).normalizedTime;

            if (pressedForce && forceAnimationCompleted)
            {
                // Terminator is now handled by physics
                terminatorAnim.enabled = false;
                terminatorRB.isKinematic = false;
                terminatorAgent.enabled = false;
                addForce = true;
            }
            else
            {
                // This is needed so that it does not continually run in FixedUpdate() when warrior simply approaches the Terminator within trigger radius
                addForce = false;
            }
        }

        // If the Terminator is not standing upright and is basically not moving
        if (terminator.transform.rotation.z != 0.0f && terminatorRB.velocity.magnitude < 0.15f)
        {
            // Give control back to Animator (and disabled physics) and NavMeshAgent
            terminatorAnim.enabled = true;
            terminatorRB.isKinematic = true;
            terminatorAgent.enabled = true;

            // Reset decrease value back to 1, so addForce can work as normal in the next run of it
            percentDecrease = 1.0f;
        }
    }

    void FixedUpdate()
    {
        if (addForce)
        {
            // This will allow force to always push the Terminator away from the player no matter where in the 3D scene
            forceDirection = terminatorRB.transform.position - warrior.transform.position;
            terminatorRB.AddForce(forceDirection.normalized * 65.0f * percentDecrease, ForceMode.Force);

            if (percentDecrease > 0.0f)
            {
                percentDecrease -= 0.015f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player" && terminatorRB.isKinematic == true)
        {
            warriorStatusController.canPunch = true;

            terminatorIsNearText.SetActive(true);
            terminatorIsNearMouseRightClickImage.SetActive(true);
            terminatorIsNearMouseRightClickText.SetActive(true);

            terminatorIsNearAudioSource.spatialBlend = 0.88f;
            terminatorIsNearAudioSource.Play();

            canPlayRobotSounds = true;

            Debug.Log("Player within force push radius");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            if (pressedForce)
            {
                forcePushAudioSource.spatialBlend = 0.5f;
                forcePushAudioSource.time = 0.35f;
                forcePushAudioSource.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            warriorStatusController.canPunch = false;

            terminatorIsNearText.SetActive(false);
            terminatorIsNearMouseRightClickImage.SetActive(false);
            terminatorIsNearMouseRightClickText.SetActive(false);

            canPlayRobotSounds = false;

            Debug.Log("Player is not within force push radius anymore");
        }
    }

    public void OnForce(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pressedForce = true;
        }

        else if (context.canceled)
        {
            pressedForce = false;
        }
    }
}
