using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class CoinSpawn : NetworkBehaviour  // Inherits from NetworkBehaviour to use networking features.
{
    private int counter = 0;                            // Counter variable, unused in the snippet.
    public AudioClip CoinSpawns;                        // AudioClip for coin spawning sound.
    public AudioClip CoinGrab;                          // AudioClip for coin grabbing sound.
    private float coinNum = 1f;                         // Number of coins to spawn, initialized to 1.
    [SerializeField] private GameObject coin;           // The coin prefab to spawn.
    private NetworkObject coinNetwork;                  // NetworkObject component for networking features, unused in the snippet.

    public float timeToSpawn;                           // Time interval between coin spawns.
    public float currentTimeToSpawn;                    // Current countdown time to the next spawn.

    void Start()                                        // Start is called before the first frame update.
    {
        currentTimeToSpawn = timeToSpawn;              // Initialize the countdown timer.
    }

    void Update()                                       // Update is called once per frame.
    {
        if (!IsOwner) return;                           // Return if this object isn't owned by the local player.
        if (currentTimeToSpawn > 0)                     // Countdown timer logic.
        {
            currentTimeToSpawn -= Time.deltaTime;       // Decrement the timer by the elapsed time since the last frame.
        }
        else
        {
            CoinFunction();                             // Spawn coins when the timer reaches 0.
            currentTimeToSpawn = timeToSpawn;           // Reset the timer.
        }
    }

    void CoinFunction()                                 // Function responsible for spawning coins.
    {
        for (int i = 0; i < coinNum; i++)               // Loop based on the number of coins to spawn.
        {
            float x = Random.Range(-7.73f, 7.73f);      // Random X position within specified range.
            float y = Random.Range(-4f, 4f);            // Random Y position within specified range.
            CoinSpawnServerRpc(x, y);                   // Call the server RPC to spawn a coin at the random position.
        }
    }

    [ServerRpc(RequireOwnership = false)]              // Server RPC, does not require the caller to own the object.
    private void CoinSpawnServerRpc(float x, float y)  // RPC method to spawn coins on the server.
    {
        GameObject go = Instantiate(coin, new Vector3(x, y, 0), Quaternion.identity); // Instantiate the coin prefab.
        go.GetComponent<NetworkObject>().Spawn();                                      // Spawn the instantiated coin across the network.
        AudioManager.instance.PlaySound(CoinSpawns);                                   // Play the coin spawn sound.
    }

    private void OnTriggerEnter2D(Collider2D Collision) // Trigger method for collision detection, specifically for 2D physics.
    {
        if (!NetworkManager.Singleton.IsServer) return; // Return if not running on the server.

        if (Collision.gameObject.CompareTag("Player"))  // Check if the colliding object has the "Player" tag.
        {
            NetworkObject.Despawn();                    // Despawn this network object.
        }
    }
}
