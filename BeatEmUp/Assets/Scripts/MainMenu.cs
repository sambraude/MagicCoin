using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioClip Play;
    public AudioClip Exit;

    private void StartTheScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void EndTheGame()
    {
        Application.Quit();
    }
    public void PlayGame()
    {
        AudioManager.instance.PlaySound(Play);
        Invoke("StartTheScene", 1);
    }

    public void QuitGame()
    {
        AudioManager.instance.PlaySound(Exit);
        Debug.Log("QUIT!");
        Invoke("EndTheGame", 1);
    }
        
}
