using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Namespace for scene management.

public class MainMenu : MonoBehaviour // Inherits from MonoBehaviour for Unity-specific functionality.
{
    public AudioClip Play; // Audio clip to play when starting the game.
    public AudioClip Exit; // Audio clip to play when exiting the game.

    private void StartTheScene() // Method to load the next scene.
    {
        // Loads the scene that comes next in the build settings.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void EndTheGame() // Method to quit the application.
    {
        Application.Quit(); // Quits the game.
    }

    public void PlayGame() // Public method linked to the Play button in the UI.
    {
        AudioManager.instance.PlaySound(Play); // Plays the "Play" sound.
        Invoke("StartTheScene", 1); // Delays the scene loading by 1 second to allow the sound to play.
    }

    public void QuitGame() // Public method linked to the Quit button in the UI.
    {
        AudioManager.instance.PlaySound(Exit); // Plays the "Exit" sound.
        Debug.Log("QUIT!"); // Logs the quit action to the console.
        Invoke("EndTheGame", 1); // Delays the application quit by 1 second to allow the sound to play.
    }
}
