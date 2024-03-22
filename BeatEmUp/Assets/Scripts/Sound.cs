using UnityEngine.Audio;  // Namespace for handling audio in Unity.
using UnityEngine;        // Main Unity namespace.

[System.Serializable]     // Makes the Sound class able to be serialized and visible in the Unity editor.
public class Sound
{
    public string name;    // Name of the sound for identification.
    public AudioClip clip; // The audio clip asset that this sound will play.

    [Range(0f, 1f)]        // Restricts the volume to a range between 0 and 1.
    public float volume;   // Volume of the sound, from 0 (mute) to 1 (full volume).
    [Range(.1f, 3f)]       // Restricts the pitch to a range between 0.1 and 3.
    public float pitch;    // Pitch of the sound; 1 is normal, <1 is lower, >1 is higher.

    public bool loop;      // Whether the sound should loop when played.

    [HideInInspector]      // Hides the AudioSource component in the Unity editor, as it will be assigned via script.
    public AudioSource source; // The AudioSource component that will play this sound. Assigned at runtime.
}
