using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AITerminator : MonoBehaviour
{
    public GameObject warrior;
    public TerminatorTrigger terminatorTrigger;
    public GameObject axeKick;
    private Animator terminatorAnim;
    private Animator warriorAnim;
    private NavMeshAgent terminatorAgent;
    private AudioSource terminatorAudioSource;
    private AudioSource axeKickAudioSource;
    private WarriorStatusController warriorStatusController;
    private bool axeKickCompleted = false;

    void Start()
    {
        terminatorAgent = GetComponent<NavMeshAgent>();
        terminatorAudioSource = GetComponent<AudioSource>();
        axeKickAudioSource = axeKick.GetComponent<AudioSource>();
        terminatorAnim = GetComponent<Animator>();
        warriorAnim = warrior.GetComponent<Animator>();
        warriorStatusController = warrior.GetComponent<WarriorStatusController>();
        
        if (terminatorAgent == null)
        {
            Debug.LogError("Robot is missing");
        }

        if (terminatorAudioSource == null)
        {
            Debug.LogError("AudioSource terminatorAudioSource is missing");
        }

        if (terminatorAnim == null)
        {
            Debug.LogError("Animator terminatorAnim is missing");
        }

        if (warriorAnim == null)
        {
            Debug.LogError("Animator warriorAnim is missing");
        }
    }

    void Update()
    {
        // Only SetDestination when NavMeshAgent is enabled
        if (terminatorAgent.isActiveAndEnabled)
        {
            terminatorAgent.SetDestination(warrior.transform.position);
        }

        // Checking each frame that if the axe kick animation is completed, then change this boolean to true
        if (terminatorAnim.GetCurrentAnimatorStateInfo(0).IsName("AxeKick"))
        {
            axeKickCompleted = true;
        }

        // Once axe kick is completed, the warrior plays die animation and his health is reduced to 0
        if (axeKickCompleted)
        {
            warriorStatusController.health = 0.0f;
            warriorAnim.SetTrigger("died");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            // Activate the axe kick from walk animation
            terminatorAnim.SetBool("WarriorCloseEnoughtoHit", true);
        }
    }
    
    public void canPlayRobotSoundsNow()
    {
        if (terminatorTrigger.canPlayRobotSounds)
        {
            terminatorAudioSource.spatialBlend = 0.92f;
            terminatorAudioSource.Play();
        }
    }

    public void canPlayAxeKickSoundNow()
    {
        axeKickAudioSource.Play();
    }
}
