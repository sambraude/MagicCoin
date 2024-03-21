using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player_Movement : NetworkBehaviour
{
    private int counter = 1;
    public AudioClip CoinGrab;
    public AudioClip Special;
    public AudioSource Grasswalker;
    public Rigidbody2D player;
    public float horizontal;
    public float vertical;
    public float direction;
    public float speedLimiter = 0.7f;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    [SerializeField]public float runSpeed = 5f;
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if ((horizontal != 0) && (vertical != 0))
        {
            horizontal *= speedLimiter;
            vertical *= speedLimiter;
        }

        player.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);



        // if velocity vector is zero, play idle animation
        if (player.velocity == Vector2.zero)
        {
            Grasswalker.enabled = false;
            anim.SetFloat("Speed", 0f);
        }
        else
        {
            Grasswalker.enabled = true;
            if (player.velocity.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
            anim.SetFloat("Speed", 1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D Collision)
    {
        if (!NetworkManager.Singleton.IsServer) return;

        if (Collision.gameObject.CompareTag("Coin"))
        {
            //gameObject.GetComponent<NetworkObject>().Despawn();
            if (counter < 5)
            {
                AudioManager.instance.PlaySound(CoinGrab);
                counter++;
            }
            else
            {
                AudioManager.instance.PlaySound(Special);
                counter = 1;
            }
            

        }


    }
    


    
}