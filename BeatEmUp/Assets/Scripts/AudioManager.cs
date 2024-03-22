using UnityEngine.Audio;              // Required for handling audio within Unity.
using System;                         // General C# system namespace.
using UnityEngine;                    // Required for Unity engine functionalities.

public class AudioManager : MonoBehaviour  // Declares AudioManager class inheriting from MonoBehaviour.
{
    public static AudioManager instance { get; private set; }  // Static instance for global access.
    private AudioSource source;                               // Reference to the AudioSource component.

    private void Awake()                                      // Awake method called when the script instance is loaded.
    {
        instance = this;                                      // Initialize the static instance with this script instance.
        source = GetComponent<AudioSource>();                 // Assigns the AudioSource component to the source variable.
    }

    public void PlaySound(AudioClip _sound)                   // Public method to play sounds.
    {
        source.PlayOneShot(_sound);                           // Plays the sound clip once.
    }
}
