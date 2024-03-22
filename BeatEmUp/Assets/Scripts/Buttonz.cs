using System.Collections;                        // Used for collections like lists, dictionaries, etc.
using System.Collections.Generic;                // Used for generic collections.
using Unity.Netcode;                             // Namespace for Unity's networking component.
using UnityEngine;                               // Main Unity namespace.

public class Buttonz : MonoBehaviour             // Declares the Buttonz class that inherits from MonoBehaviour.
{
    // OnGUI is called for rendering and handling GUI events.
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));   // Defines a new GUI area on the screen.

        // Check if the current instance is neither a client nor a server.
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            // Creates a button and starts host mode if it's clicked.
            if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
            // Creates a button and starts server mode if it's clicked.
            if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
            // Creates a button and starts client mode if it's clicked.
            if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        }

        GUILayout.EndArea();                         // Ends the previously defined GUI area.
    }
}
