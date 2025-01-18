using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Fields- local persistent variables for specific Class (like health)
    public float walkSpeed = 5;
    private Rigidbody2D _rigidBody;
    private BoxCollider2D _collider;

    public int _playerState = 0;
    // -1 is detected, 0 is default, 1 is hiding

    // Start is called before the first frame update
    void Start()
    {
        //set variables to components
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerState == -1)
        {
            // disable all controls when detected and run dialogue system
            // pass;
        }
        else if (_playerState == 1)
        {
            // disable all controls except SPACE to unhide yourself
            // pass;
        }
        else
        {
            //movement inputs
            float horizontalInputs = Input.GetAxisRaw("Horizontal");
            float verticalInputs = Input.GetAxisRaw("Vertical");
            _rigidBody.velocity = new Vector2(walkSpeed * horizontalInputs, walkSpeed * verticalInputs);

            //Debug.Log(_playerState);

            /* if completed_a_level:
             *  Instantiate(PlayerPrefab, transform.position, Quaternion.identity);
             *  SceneManager.LoadScene(use the scene build index);
             *  go to file > build settings > drag new scene to the build settings
             *  to go to next level just do like this:
             *  int toLoad = SceneManager.GetActiveScene.buildIndex + 1;
             *  SceneManager.LoadScene(toLoad); > goes to next scene in build settings
             * if die:
             *  Reload Scene somehow
             */
        }
    }
}