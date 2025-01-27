using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Fields - local persistent variables for specific Class (like health)
    public float walkSpeed = 5;
    public Rigidbody2D rigidBody;
    private BoxCollider2D _collider;

    public int _playerState = 0;
    // -1 is detected, 0 is default, 1 is hiding

    public bool disableInputs;

    // Walking sound
    private AudioSource audioSource;
    private Vector2 lastDirection = Vector2.down; // Default facing direction (down)

    // Reference to the animation system
    public NPCAnimations anim;

    public int animState = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Set variables to components
        rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        disableInputs = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerState == -1)
        {
            // Disable all controls when detected and run dialogue system
            // pass;
        }
        else if (_playerState == 1)
        {
            // Disable all controls except SPACE to unhide yourself
            // pass;
        }
        else
        {
        // Movement inputs
            float horizontalInputs = Input.GetAxisRaw("Horizontal");
            float verticalInputs = Input.GetAxisRaw("Vertical");

            if (!disableInputs)
            {
                rigidBody.velocity = new Vector2(walkSpeed * horizontalInputs, walkSpeed * verticalInputs);
            }

            // Determine walking or idle state
            Vector2 movement = rigidBody.velocity;
            int newPlayerState = animState;

            if (movement.magnitude > 0.1f) // Player is moving
            {
                // Update direction based on input
                if (horizontalInputs > 0) lastDirection = Vector2.right;
                else if (horizontalInputs < 0) lastDirection = Vector2.left;
                else if (verticalInputs > 0) lastDirection = Vector2.up;
                else if (verticalInputs < 0) lastDirection = Vector2.down;

                // Set walking state based on direction
                if (lastDirection == Vector2.down) newPlayerState = -1; // Walk down
                else if (lastDirection == Vector2.left) newPlayerState = 2;  // Walk left
                else if (lastDirection == Vector2.up) newPlayerState = -3; // Walk up
                else if (lastDirection == Vector2.right) newPlayerState = 4; // Walk right;
            }
            else
            {
                if (lastDirection == Vector2.down) newPlayerState = 0; // Idle down
                else if (lastDirection == Vector2.left) newPlayerState = 1;  // Idle left
                else if (lastDirection == Vector2.up) newPlayerState = -2; // Idle up
                else if (lastDirection == Vector2.right) newPlayerState = 3; // Idle right;
            }

            // Only update animation if the state changes
            if (newPlayerState != animState)
            {
                animState = newPlayerState;
                anim.ChangeAnimation(animState);
            }

            // Handle walking sound
            if (horizontalInputs != 0 || verticalInputs != 0)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.Play(); // Play walking sound
                }
            }
            else
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop(); // Stop walking sound when not moving
                }
            }

            // Debug.Log(_playerState);
        }
    }
}
