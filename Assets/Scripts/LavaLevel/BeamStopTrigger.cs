using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamStopTrigger : MonoBehaviour
{
    public GameObject beam;
    public GameObject beamStopTrigger;
    private bool beamCanStopNow = false;
    
    // Start is called before the first frame update
    void Start()
    {
        beamStopTrigger.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (beam.activeSelf)
        {
            beamStopTrigger.SetActive (true);
        }

        if (beamCanStopNow)
        {
            beam.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "BeamStop")
        {
            beamCanStopNow = true;
        }
    }
}
