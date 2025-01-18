using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Fields- local persistent variables for specific Class (like health)
    public float RunSpeed;
    public float JumpSpeed;
    public LayerMask GroundMask;
    public Animator Anim;
    public Player PlayerPrefab;

    private Rigidbody2D _rigidBody;
    private BoxCollider2D _collider;

    private int _playerState = 0;
    private int _jumpCounter = 0;

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
        //fall to death and respawn
        if (transform.position.y < -8)
        {
            _playerState = -1;
            Die();
        }

        if (_playerState == -1) {
            Die();
        } 
        else
        {
            //horizontal movement inputs
            float horizontalInputs = Input.GetAxis("Horizontal");
            _rigidBody.velocity = new Vector2(RunSpeed * horizontalInputs,
                _rigidBody.velocity.y);

            //Debug.Log(_playerState);

            if (!isGrounded())
            {
                //fall
                if (_rigidBody.velocity.y < 0)
                {
                    _playerState = 3;

                    //double jump
                    if (Input.GetButtonDown("Jump") && _jumpCounter < 2)
                    {
                        _rigidBody.velocity = Vector2.up * JumpSpeed;
                        _playerState = 4;
                        _jumpCounter += 2;
                    }
                }
                //also jump
                else if (_rigidBody.velocity.y > 0)
                {
                    if (_jumpCounter == 2)
                    {
                        _playerState = 4;
                    }
                    else
                    {
                        _playerState = 2;
                    }

                    //double jump
                    if (Input.GetButtonDown("Jump") && _jumpCounter < 2)
                    {
                        _rigidBody.velocity = Vector2.up * JumpSpeed;
                        _playerState = 4;
                        _jumpCounter += 1;
                    }
                }
            }
            else
            {
                _jumpCounter = 0;

                //left movement
                if (_rigidBody.velocity.x < 0)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                    _playerState = 1;
                }
                //right movement
                else if (_rigidBody.velocity.x > 0)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                    _playerState = 1;
                }
                //idle animation
                else if (_rigidBody.velocity.x == 0 && _rigidBody.velocity.y == 0)
                {
                    _playerState = 0;
                }

                //jump
                if (Input.GetButtonDown("Jump"))
                {
                    _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, JumpSpeed);
                    _playerState = 2;
                    _jumpCounter += 1;
                }
            }
        }

        Anim.SetInteger("playerState", _playerState);

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

    bool isGrounded()
    {
        return Physics2D.BoxCast(_collider.bounds.center, _collider.bounds.size, 
            0, Vector2.down, 0.1f, GroundMask);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Die()
    {
        _rigidBody.gravityScale = 0;
        _rigidBody.velocity = new Vector2(0, 0);
        StartCoroutine(Respawn());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Chicken chicken = collision.collider.GetComponent<Chicken>();
        if (chicken != null)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.9f)
                {
                    Vector2 velocity = _rigidBody.velocity;
                    velocity.y = JumpSpeed * 0.5f;
                    _rigidBody.velocity = velocity;
                    chicken.isDead = true;
                    chicken.Die();
                }
                else
                {
                    //Anim.SetInteger("playerState", -1);
                    _playerState = -1;
                }
            }
        }

        ChickenLeft chickenLeft = collision.collider.GetComponent<ChickenLeft>();
        if (chicken != null)
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.y >= 0.9f)
                {
                    Vector2 velocity = _rigidBody.velocity;
                    velocity.y = JumpSpeed * 0.5f;
                    _rigidBody.velocity = velocity;
                    chickenLeft.isDead = true;
                    chickenLeft.Die();
                }
                else
                {
                    //Anim.SetInteger("playerState", -1);
                    _playerState = -1;
                }
            }
        }
    }
}
