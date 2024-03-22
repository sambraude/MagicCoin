using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;  // Namespace for Unity's visual scripting component, not used in this script.
using UnityEngine;            // Standard Unity namespace.
using UnityEngine.SceneManagement;  // Namespace for managing scenes in Unity.

public class PauseMenu : MonoBehaviour  // Class definition inheriting from MonoBehaviour.
{
    public static bool GameIsPaused = false;  // Static variable to track the game's pause state globally.

    public GameObject pauseMenuUI;  // Public reference to the pause menu UI GameObject.

    // Update is called once per frame.
    void Update()
    {
        // Listens for the Escape key press to toggle the pause state.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();  // Calls Resume() if the game is currently paused.
            }
            else
            {
                Pause();   // Calls Pause() if the game is currently running.
            }
        }
    }

    public void Resume()  // Public method to resume the game.
    {
        pauseMenuUI.SetActive(false);  // Deactivates the pause menu UI.
        Time.timeScale = 1f;  // Restores the normal time scale, resuming game time.
        GameIsPaused = false;  // Sets the pause state to false.
    }

    void Pause()  // Private method to pause the game.
    {
        pauseMenuUI.SetActive(true);  // Activates the pause menu UI.
        Time.timeScale = 0f;  // Sets the time scale to 0, effectively pausing all time-dependent game activities.
        GameIsPaused = true;  // Sets the pause state to true.
    }

    public void LoadMenu()  // Public method to load the main menu scene.
    {
        Time.timeScale = 1f;  // Restores the normal time scale before leaving the scene.
        SceneManager.LoadScene("Menu");  // Loads the scene named "Menu".
    }

    public void QuitGame()  // Public method to quit the game.
    {
        Debug.Log("Quitting game...");  // Logs the quit action to the console for debugging.
        Application.Quit();  // Quits the game application.
    }
}
