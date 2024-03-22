using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;  // Unity Netcode namespace for multiplayer support.
using UnityEngine;    // Standard Unity namespace.

public class NetworkButtons : MonoBehaviour  // Inherits from MonoBehaviour to be used as a component in Unity.
{
    // Start is called before the first frame update. OnGUI is a Unity method for rendering GUI elements.
    void OnGUI()
    {
        // Creates a GUI area on the screen for our network buttons.
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));

        // Checks if the current instance is not already a client or a server.
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            // Creates a GUI button and starts the game as a host if clicked.
            if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();

            // Creates a GUI button and starts the game as a server only if clicked.
            if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();

            // Creates a GUI button and starts the game as a client if clicked.
            if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        }

        // Ends the GUI area defined at the beginning.
        GUILayout.EndArea();
    }
}
