using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.Netcode;
using UnityEngine;

public class CoinSpawn : NetworkBehaviour
{
    // coin prefab

    private int counter = 0;
    public AudioClip CoinSpawns;
    public AudioClip CoinGrab;
    private float coinNum = 1f;
    [SerializeField] private GameObject coin;
     private NetworkObject coinNetwork;
    // Shoot position

    public float timeToSpawn;
    public float currentTimeToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        currentTimeToSpawn = timeToSpawn;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!IsOwner) return;
        if (currentTimeToSpawn > 0)
        {
            currentTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            CoinFunction();
            currentTimeToSpawn = timeToSpawn;
        }
    }
    void CoinFunction()
    {
        
        for (int i = 0; i < coinNum; i++)
        {
            float x = Random.Range(-7.73f, 7.73f);
            float y = Random.Range(-4f, 4f);
            CoinSpawnServerRpc(x, y);
        } 
    }
    // Unity Message 0 references
   

    [ServerRpc(RequireOwnership = false)]
    private void CoinSpawnServerRpc(float x, float y)
    {

        GameObject go = Instantiate(coin, new Vector3(x, y, 0), Quaternion.identity);
        go.GetComponent<NetworkObject>().Spawn();
        AudioManager.instance.PlaySound(CoinSpawns);
    }


    /* [ServerRpc(RequireOwnership = false)]
     void CoinDeleteServerRpc(Collider2D Collision)
     {
         if (Collision.gameObject.CompareTag("Player"))
         {
             Destroy(gameObject);
         }
     } */
    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (!NetworkManager.Singleton.IsServer) return;

        if (Collision.gameObject.CompareTag("Player"))
        {
            //gameObject.GetComponent<NetworkObject>().Despawn();

            NetworkObject.Despawn();
            
        }


    }

}
