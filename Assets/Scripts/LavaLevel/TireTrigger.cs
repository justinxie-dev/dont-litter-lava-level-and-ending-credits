using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TireTrigger : MonoBehaviour
{
    
    public GameObject tire;
    private Material tireMaterial;
    private AudioSource tireAudioSource;
    
    public bool storeRecordIntoAutoshopLogic = false;
    private bool turnGreen = false;
    private bool stopRepeatedPlaysNow = false;
    
    void Start()
    {
        tireMaterial = tire.GetComponent<Renderer>().material;
        tireAudioSource= GetComponent<AudioSource>();
    }

    void Update()
    {
        // If condition is true, we set the tire to green to indicate that this tire has been visited and a "ding" sound will play
        if (turnGreen)
        {
            tireMaterial.color = new Color(159.0f, 253.0f, 50.0f);
            storeRecordIntoAutoshopLogic = true;

            // This is needed or else it will continue to repeat the clip on each Update(). Since this clip is quiet for the first split second, the behavior that will be observed is that the audio will seemingly not play.
            // The other alternative is to just put the Play() inside OnTriggerEnter, but I prefer to put all functionality in Update().
            // Audio clip/sound source link: https://pixabay.com/sound-effects/search/pick-up/
            if (!stopRepeatedPlaysNow)
            {
                tireAudioSource.Play();
                stopRepeatedPlaysNow = true;
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            turnGreen = true;
        }
    }
}
