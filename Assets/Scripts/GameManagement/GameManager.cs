using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // persistent game info prefab, used for Instantiate when needed
    public GameObject pgiPrefab;
    public PersistentGameInfo gameInfo;

    public GameObject screenTransitionFadeObject;
    public float transitionTime = 1f;
    
    public static GameManager instance;

    public LevelManager levelManager;

    public InGameMenu inGameMenu;

    public bool dialogueActive = false;
    public bool dialogContinueCurrentlyPressed = false;

    private void Awake()
    {
        instance = this;
        gameInfo = FindObjectOfType<PersistentGameInfo>();
        if (gameInfo == null)
        {
            gameInfo = Instantiate(pgiPrefab).GetComponent<PersistentGameInfo>();
        }
      
    }

    void Start()
    {
        if (gameInfo == null)
        {
            Debug.LogError("Persistent Game Object not found");
        }

        if (SceneManager.GetActiveScene().name != "LoadingScreen")
        {
            gameInfo.currentLevelName = SceneManager.GetActiveScene().name;
            gameInfo.nextLevelName = levelManager.nextScene;
        }

        LoadingScreenManager.nextSceneName = gameInfo.nextLevelName;

        if (inGameMenu)
        {
            if (inGameMenu.MenuActive)
            {
                InGameMenu.ActivateInGameMenu(false);
            }
        }

        if (levelManager)
        {
            levelManager.StartLevel();
            StartCoroutine(SceneTransitionEnter());
        }
    }

    void FixedUpdate()
    {
        if (levelManager.IsLevelCleared())
        {
            if (inGameMenu) 
            {
                if (inGameMenu.MenuActive)
                {
                    InGameMenu.ActivateInGameMenu(false);
                }
            }

            if (SceneManager.GetActiveScene().name != "LoadingScreen")
            {
                // The level is cleared, so we need to go to the loading screen
                StartCoroutine(SceneTransitionExit());
            }
        }
    }

    private IEnumerator SceneTransitionExit()
    {
        // this coroutine runs when the scene transitions from the current scene to the loading screen
        // specifically, this fades the current scene to black before switching to the loading screen
        screenTransitionFadeObject.SetActive(true);
        Color color = screenTransitionFadeObject.GetComponent<Image>().color;
        float fadeElapsedTime = 0f;

        while (fadeElapsedTime < transitionTime)
        {
            fadeElapsedTime += Time.deltaTime;
            float frac = fadeElapsedTime / transitionTime;
            screenTransitionFadeObject.GetComponent<Image>().color = Color.Lerp(color, new Color(color.r, color.g, color.b, 1f), frac);
            yield return null;
        }
        SceneManager.LoadScene("LoadingScreen");
    }

    private IEnumerator SceneTransitionEnter()
    {
        // this coroutine runs when the scene transitions from the loading screen to the next screen
        // specifically, the next scene fades from black after switching from the loading screen

        screenTransitionFadeObject.SetActive(true);
        Color color = screenTransitionFadeObject.GetComponent<Image>().color;
        Color initialColor = new Color(color.r, color.g, color.b, 1f);
        Color finalColor = new Color(color.r, color.g, color.b, 0f);

        // initially have the screen be entirely black
        screenTransitionFadeObject.GetComponent<Image>().color = initialColor;

        float fadeElapsedTime = 0f;

        while (fadeElapsedTime < transitionTime)
        {
            fadeElapsedTime += Time.deltaTime;
            float frac = fadeElapsedTime / transitionTime;
            screenTransitionFadeObject.GetComponent<Image>().color = Color.Lerp(initialColor, finalColor, frac);
            yield return null;
        }
        screenTransitionFadeObject.SetActive(false);
    }

    public static LevelManager GetLevelManager ()
    {
        return instance.levelManager;
    }

    public static void SetDialogueActive(bool value)
    {
        instance.dialogueActive = value;
    }
    public static bool IsDialogueActive()
    {
        return instance.dialogueActive;
    }

    public static void OnDialogueContinue(InputAction.CallbackContext context)
    {
        // Continue pressed, this just grabs the exact moment
        if (context.performed)
        {
            instance.dialogContinueCurrentlyPressed = true;
        }
        // Continue released
        else if (context.canceled)
        {
            instance.dialogContinueCurrentlyPressed = false;
        }
    }
    public static bool IsDialogueContinuePressed()
    {
        bool currentValue = instance.dialogContinueCurrentlyPressed;
        instance.dialogContinueCurrentlyPressed = false; // Clean up the value so that the key is no longer pressed
        return currentValue;
    }


    public static void RestartLevel()
    {
        if (instance.inGameMenu.MenuActive)
        {
            InGameMenu.ActivateInGameMenu(false);
        }
        // Get the active scene's name and reload the scene
        string activeSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(activeSceneName);
    }

    public static void ReloadLastCheckpoint()
    {
        bool checkPointReloadSuccess = instance.levelManager.ReloadLastCheckpoint();
        // Checkpoint reload was succesfull
        if (checkPointReloadSuccess && instance.inGameMenu.MenuActive)
        {
            InGameMenu.ActivateInGameMenu(false);
        }
        // If we can't reload checkpoint just reload level
        else if (!checkPointReloadSuccess)
        {
            RestartLevel();
        }
    }

    public static void GoToTitleScreen()
    {
        if (instance.inGameMenu.MenuActive)
        {
            InGameMenu.ActivateInGameMenu(false);
        }
        // For now, use Build index 0 since it should be the first
        // scene loaded (or be in the first scene loaded)
        // In the future, may decide to use a string to load the title screen
        SceneManager.LoadScene(0);
    }

    // Code borrowed from the GameQuitter.cs script provided in Milestone 3
    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public static void GoToVillageLevel()
    {
        if (instance.inGameMenu.MenuActive)
        {
            InGameMenu.ActivateInGameMenu(false);
        }

        SceneManager.LoadScene(2);
    }

    public static void GoToForestLevel()
    {
        if (instance.inGameMenu.MenuActive)
        {
            InGameMenu.ActivateInGameMenu(false);
        }

        SceneManager.LoadScene(3);
    }

    public static void GoToTempleLevel()
    {
        if (instance.inGameMenu.MenuActive)
        {
            InGameMenu.ActivateInGameMenu(false);
        }

        SceneManager.LoadScene(4);
    }

    public static void GoToGolemLevel()
    {
        if (instance.inGameMenu.MenuActive)
        {
            InGameMenu.ActivateInGameMenu(false);
        }

        SceneManager.LoadScene(5);
    }

    public static void GoToLavaLevel()
    {
        if (instance.inGameMenu.MenuActive)
        {
            InGameMenu.ActivateInGameMenu(false);
        }

        SceneManager.LoadScene(6);
    }

    public static void GoToEndingCredits()
    {
        if (instance.inGameMenu.MenuActive)
        {
            InGameMenu.ActivateInGameMenu(false);
        }

        SceneManager.LoadScene(7);
    }
}
