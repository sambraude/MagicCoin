using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;  // Namespace for Unity's networking component.
using UnityEngine;    // Standard Unity namespace.

public class Player_Movement : NetworkBehaviour  // Inherits from NetworkBehaviour for networked behavior.
{
    private int counter = 1;                            // Counter for special coin interaction.
    public AudioClip CoinGrab;                          // Audio clip for regular coin pickup.
    public AudioClip Special;                           // Audio clip for special interaction after multiple pickups.
    public AudioSource Grasswalker;                    // Audio source for walking sound.
    public Rigidbody2D player;                          // Player's Rigidbody2D component for physics interactions.
    public float horizontal;                            // Horizontal input value.
    public float vertical;                              // Vertical input value.
    public float direction;                             // Direction input value, currently unused.
    public float speedLimiter = 0.7f;                   // Limiter for diagonal movement speed.
    private Animator anim;                              // Animator component for controlling animations.
    private SpriteRenderer spriteRenderer;              // SpriteRenderer component for modifying sprite properties.

    [SerializeField] public float runSpeed = 5f;        // Public variable for run speed, can be modified in the Inspector.

    void Start()                                        // Start is called before the first frame update.
    {
        player = GetComponent<Rigidbody2D>();          // Get the Rigidbody2D component attached to the player.
        anim = GetComponent<Animator>();               // Get the Animator component attached to the player.
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to the player.
    }

    void Update()                                       // Update is called once per frame.
    {
        if (!IsOwner) return;                           // Return if the player is not the local player in a networked game.
        horizontal = Input.GetAxisRaw("Horizontal");    // Get horizontal input.
        vertical = Input.GetAxisRaw("Vertical");        // Get vertical input.

        if ((horizontal != 0) && (vertical != 0))       // If moving diagonally,
        {
            horizontal *= speedLimiter;                 // Apply speed limiter to horizontal movement.
            vertical *= speedLimiter;                   // Apply speed limiter to vertical movement.
        }

        player.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed); // Set the player's velocity based on input.

        // Play walking sound and animations based on movement.
        if (player.velocity == Vector2.zero)            // If player is not moving,
        {
            Grasswalker.enabled = false;                // Disable walking sound.
            anim.SetFloat("Speed", 0f);                 // Set animation speed to 0.
        }
        else                                            // If player is moving,
        {
            Grasswalker.enabled = true;                 // Enable walking sound.
            if (player.velocity.x < 0)                  // If moving left,
            {
                spriteRenderer.flipX = true;            // Flip sprite horizontally.
            }
            else                                        // If moving right or not horizontally,
            {
                spriteRenderer.flipX = false;           // Keep sprite orientation normal.
            }
            anim.SetFloat("Speed", 1f);                 // Set animation speed to 1.
        }
    }

    private void OnTriggerEnter2D(Collider2D Collision) // Called when another collider enters this object's trigger collider.
    {
        if (!NetworkManager.Singleton.IsServer) return; // Return if not running on the server.

        if (Collision.gameObject.CompareTag("Coin"))    // If collided with an object tagged "Coin",
        {
            if (counter < 5)                            // If counter is less than 5,
            {
                AudioManager.instance.PlaySound(CoinGrab); // Play regular pickup sound.
                counter++;                              // Increment counter.
            }
            else                                        // If counter is 5,
            {
                AudioManager.instance.PlaySound(Special); // Play special pickup sound.
                counter = 1;                            // Reset counter.
            }
        }
    }
}
