using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    void Update()
    {
        // Once credits are finished, automatically go back to Main Menu
        if (!GetComponent<VideoPlayer>().isPlaying)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
