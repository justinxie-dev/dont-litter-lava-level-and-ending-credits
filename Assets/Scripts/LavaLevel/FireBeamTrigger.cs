using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class FireBeamTrigger : MonoBehaviour
{
    
    public GameObject powerOrbsTracker;
    public GameObject beam;
    public GameObject lavaGolem;
    public GameObject sniperScope;
    public GameObject sniperScopeCamera;
    public GameObject healthBar;
    public GameObject notReadyToFireText;
    public GameObject readyToFireText;
    public GameObject aimTextAndIconsGuidance;
    public GameObject powerOrbUI1;
    public GameObject powerOrbUI2;
    public GameObject powerOrbUI3;
    public GameObject powerOrbUI4;
    public GameObject powerOrbsPercentUI;

    private Animator lavaGolemAnim;
    private AudioSource beamSound;
    private PowerOrbsCount powerOrbsCount;
    private SniperScopeMoveWithMouse sniperScopeMoveWithMouse;
    private bool beamCanBeFiredNow = false;
    private bool hasPlayed = false;

    private bool aimIsGoodX = false;
    private bool aimIsGoodY = false;
    private bool readyToFire = false;
    private bool firedTheShot = false;
    private bool pressedAim = false;
    private bool pressedFire = false;

    private bool powerOrbUI1WasHereBeforeThisTrigger = false;
    private bool powerOrbUI2WasHereBeforeThisTrigger = false;
    private bool powerOrbUI3WasHereBeforeThisTrigger = false;
    private bool powerOrbUI4WasHereBeforeThisTrigger = false;

    void Start()
    {
        powerOrbsCount = powerOrbsTracker.GetComponent<PowerOrbsCount>();
        lavaGolemAnim = lavaGolem.GetComponent<Animator>();
        beamSound = GetComponent<AudioSource>();
        sniperScopeMoveWithMouse = sniperScopeCamera.GetComponent<SniperScopeMoveWithMouse>();
        
        beam.SetActive(false);
        sniperScope.SetActive(false);
        notReadyToFireText.SetActive(false);
        readyToFireText.SetActive(false);
        aimTextAndIconsGuidance.SetActive(false);

    }

    void Update()
    {
        if (beamCanBeFiredNow)
        {
            aimTextAndIconsGuidance.SetActive(true);
            
            if (pressedAim && powerOrbsCount.powerOrbsCount == 4)
            {
                // Activate the sniper scope camera
                sniperScopeMoveWithMouse.MouseRightClick();

                // Update the UI as appropriate
                sniperScope.SetActive(true);
                healthBar.SetActive(false);
                powerOrbUI1.SetActive(false);
                powerOrbUI2.SetActive(false);
                powerOrbUI3.SetActive(false);
                powerOrbUI4.SetActive(false);
                powerOrbsPercentUI.SetActive(false);

                // Sets the firable area
                aimIsGoodX = sniperScopeCamera.transform.rotation.x < 0.9f && sniperScopeCamera.transform.rotation.x > -0.05f;
                aimIsGoodY = sniperScopeCamera.transform.rotation.y < 0.03f && sniperScopeCamera.transform.rotation.y > 0.001f;
                readyToFire = aimIsGoodX && aimIsGoodY;

                notReadyToFireText.SetActive(false);
                readyToFireText.SetActive(false);

                if (readyToFire && !firedTheShot)
                {
                    notReadyToFireText.SetActive(false);
                    readyToFireText.SetActive(true);

                    if (pressedFire)
                    {
                        beam.SetActive(true);
                        lavaGolemAnim.SetBool("Death", true);

                        if (!hasPlayed)
                        {
                            beamSound.Play();
                            hasPlayed = true;
                            firedTheShot = true;
                        }
                    }
                } 
                else if (!firedTheShot)
                {
                    notReadyToFireText.SetActive(true);
                    readyToFireText.SetActive(false);
                }
                else
                {
                    notReadyToFireText.SetActive(false);
                    readyToFireText.SetActive(false);
                }
            } 
            else
            {
                // Back to original camera
                sniperScope.SetActive(false);
                healthBar.SetActive(true);
                aimTextAndIconsGuidance.SetActive(true);
                powerOrbsPercentUI.SetActive(true);

                // The boolean values stored from entering this trigger are used here to determine how to set back the power orbs UI count.
                // Not doing this and just re-setting everything to active will make the UI show 4 power orbs no matter how many were truly collected.
                if (powerOrbUI1WasHereBeforeThisTrigger)
                {
                    powerOrbUI1.SetActive(true);
                }
                if (powerOrbUI2WasHereBeforeThisTrigger)
                {
                    powerOrbUI2.SetActive(true);
                }
                if (powerOrbUI3WasHereBeforeThisTrigger)
                {
                    powerOrbUI3.SetActive(true);
                }
                if (powerOrbUI4WasHereBeforeThisTrigger)
                {
                    powerOrbUI4.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            beamCanBeFiredNow = true;

            // This is used to set correct power orbs UI count after switching back from Sniper Scope camera
            if (powerOrbUI1.activeInHierarchy)
            {
                powerOrbUI1WasHereBeforeThisTrigger = true;
            }
            if (powerOrbUI2.activeInHierarchy)
            {
                powerOrbUI2WasHereBeforeThisTrigger = true;
            }
            if (powerOrbUI3.activeInHierarchy)
            {
                powerOrbUI3WasHereBeforeThisTrigger = true;
            }
            if (powerOrbUI4.activeInHierarchy)
            {
                powerOrbUI4WasHereBeforeThisTrigger = true;
            }
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pressedAim = true;
        }
        
        else if (context.canceled)
        {
            pressedAim = false;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            pressedFire = true;
        }

        else if (context.canceled)
        {
            pressedFire = false;
        }
    }
}
