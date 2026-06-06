using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScriptForObjectives : MonoBehaviour
{
    public GameObject button;
    public GameObject objective;
    public GameObject solidColorBackground;
    public GameObject warrior;
    public PlayerInput playerInput; 
    private Animator warriorAnim;
    private AudioSource objectiveAudioSource;

    void Start()
    {
        button.SetActive(false);
        objective.SetActive(false);
        solidColorBackground.SetActive(false);
        
        warriorAnim = warrior.GetComponent<Animator>();
        objectiveAudioSource = GetComponent<AudioSource>();

        if (warriorAnim == null)
        {
            Debug.LogError("Animator warriorAnim is not found");
        }

        if (objectiveAudioSource == null)
        {
            Debug.LogError("AudioSource objectiveAudioSource is not found");
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3(0.0f, 30.0f, 0.0f) * Time.deltaTime);

        // Disable jump animation upon leaving an objective menu, and then re-enabling afterwards
        // Cannot put under OnLeaveObjective as jump animation is still triggered for some reason, perhaps due to undefined ordering
        // Also disable menu activating since objectives screen already pauses the game. Double pausing leads to some undesired behaviors.
        if (button.activeInHierarchy)
        {
            playerInput.actions.FindAction("Jump").Disable();
            playerInput.actions.FindAction("Menu").Disable();
        }
        else
        {
            playerInput.actions.FindAction("Jump").Enable();
            playerInput.actions.FindAction("Menu").Enable();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            // Pause the game
            Time.timeScale = 0.0f;

            // Make cursor reappear but confined to game screen
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            
            // Adjust UI as necessary
            button.SetActive(true);
            objective.SetActive(true);
            solidColorBackground.SetActive(true);
            
            // Play sound
            objectiveAudioSource.Play();
        }
    }

    public void ResumeGameFromObjectiveScreenButton()
    {
        // Undo everything done by OnTriggerEnter within this script
        Time.timeScale = 1.0f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        button.SetActive(false);
        objective.SetActive(false);
        solidColorBackground.SetActive(false);
        
        // This is for stopping the slash/attack animation upon Button click with mouse
        warriorAnim.ResetTrigger("attack");
    }

    public void OnLeaveObjective(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            // Same functionality as ResumeGameFromObjectiveScreenButton but with controller input for triggering it

            // Undo everything done by OnTriggerEnter within this script
            Time.timeScale = 1.0f;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            button.SetActive(false);
            objective.SetActive(false);
            solidColorBackground.SetActive(false);
        }
    }
}
