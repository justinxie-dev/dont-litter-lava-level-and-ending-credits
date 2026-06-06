using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerOrbTrigger : MonoBehaviour
{
    public GameObject powerOrb;
    public GameObject powerOrbsTracker;
    public GameObject powerOrbsSet;
    public GameObject powerOrbUI1;
    public GameObject powerOrbUI2;
    public GameObject powerOrbUI3;
    public GameObject powerOrbUI4;
    public GameObject primaryObjectiveUI;
    public TextMeshProUGUI powerOrbsPercentUI;
    private AudioSource powerOrbPickUpAudioSource;
    private PowerOrbsCount powerOrbsCountScript;
    private bool hasPlayed = false;
    private bool playPowerOrbPickupSound = false;


    void Awake()
    {
        // Cannot put as list. For some reason, doing so makes Unit Edior freeze when dragging them to Inspector.
        // Have to put in Awake() instead of Start() because AutoShop logic will enable its power orb mid-game and reset everything to false, which
        // is undesired behavior.
        powerOrbUI1.SetActive(false);
        powerOrbUI2.SetActive(false);
        powerOrbUI3.SetActive(false);
        powerOrbUI4.SetActive(false);
    }
    void Start()
    {
        powerOrbPickUpAudioSource = powerOrbsSet.GetComponent<AudioSource>();
        powerOrbsCountScript = powerOrbsTracker.GetComponent<PowerOrbsCount>();

        if (powerOrbPickUpAudioSource == null)
        {
            Debug.LogError("AudioSource powerOrbPickUpAudioSource is not found");
        }

        if (powerOrbsCountScript == null)
        {
            Debug.LogError("PowerOrbsCount powerOrbsCountScript is not found");
        }
    }

    void Update()
    {
        if (playPowerOrbPickupSound)
        {
            if (!hasPlayed)
            {
                powerOrbPickUpAudioSource.Play();
                powerOrb.transform.gameObject.SetActive(false);
                hasPlayed = true;
                powerOrbsCountScript.powerOrbsCount++;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            playPowerOrbPickupSound = true;
            primaryObjectiveUI.SetActive(false);
            
            // Make the power orb icons appear sequentially on UI below health bar no matter from which sequence of locations the warrior collects/retrieves them from
            if (!powerOrbUI1.activeInHierarchy)
            {
                powerOrbUI1.SetActive(true);
                powerOrbsPercentUI.text = "25%";
            }
            else if (!powerOrbUI2.activeInHierarchy)
            {
                powerOrbUI2.SetActive(true);
                powerOrbsPercentUI.text = "50%";
            }
            else if (!powerOrbUI3.activeInHierarchy)
            {
                powerOrbUI3.SetActive(true);
                powerOrbsPercentUI.text = "75%";
            }
            else if (!powerOrbUI4.activeInHierarchy)
            {
                powerOrbUI4.SetActive(true);
                powerOrbsPercentUI.text = "100%";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            // Need to deactivate this so that the UI will only count this trigger once. Not doing this will result in
            // undesired behavior of UI adding another power orb to under the health bar everytime the warrior triggers
            // a power orb spot.
            transform.gameObject.SetActive(false);
        }
    }
}
