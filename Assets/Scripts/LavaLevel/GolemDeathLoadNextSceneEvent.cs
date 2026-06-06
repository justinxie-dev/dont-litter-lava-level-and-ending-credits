using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDeathLoadNextSceneEvent : MonoBehaviour
{
    public GameObject levelManager;
    private LavaLevelManager lavaLevelManager;

    void Start()
    {
        lavaLevelManager = levelManager.GetComponent<LavaLevelManager>();
    }

    // Callback to put at the end of the Golem "die" animation to transition into the next scene
    public void GolemDeathLoadNextScene()
    {
        lavaLevelManager.LoadNextLevel();
    }
}
