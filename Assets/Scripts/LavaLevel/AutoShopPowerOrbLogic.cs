using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoShopPowerOrbLogic : MonoBehaviour
{
    public GameObject tireTrigger0;
    public GameObject tireTrigger1;
    public GameObject tireTrigger2;

    private TireTrigger tireTriggerScript0;
    private TireTrigger tireTriggerScript1;
    private TireTrigger tireTriggerScript2;

    public GameObject powerOrb;

    void Start()
    {
        powerOrb.SetActive(false);
        
        tireTriggerScript0 = tireTrigger0.GetComponent<TireTrigger>();
        tireTriggerScript1 = tireTrigger1.GetComponent<TireTrigger>();
        tireTriggerScript2 = tireTrigger2.GetComponent<TireTrigger>();

        if(tireTriggerScript0 == null)
        {
            Debug.LogError("Tire Trigger script cannot be found for the first Tire Trigger Game Object");
        }

        if (tireTriggerScript1 == null)
        {
            Debug.LogError("Tire Trigger script cannot be found for the second Tire Trigger Game Object");
        }

        if(tireTriggerScript1 == null)
        {
            Debug.LogError("Tire Trigger script cannot be found for the third Tire Trigger Game Object");
        }
    }

    void Update()
    {
        // If all three tires have been OnTriggerEntered, then activate the Power Orb at the AutoShop location
        if (tireTriggerScript0.storeRecordIntoAutoshopLogic && 
            tireTriggerScript1.storeRecordIntoAutoshopLogic && 
            tireTriggerScript2.storeRecordIntoAutoshopLogic)
        {
            powerOrb.SetActive(true);
        }
    }
}
