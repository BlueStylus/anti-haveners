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
