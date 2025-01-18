using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenLeft : MonoBehaviour
{
    // Serialized Fields
    public GameObject pointA;
    public GameObject pointB;
    public float speed;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;

    public bool isDead;

    private int _enemyState;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;

        _enemyState = 1;
        flip();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -8)
        {
            Die();
        }

        //Debug.Log(transform.position.y);

        Vector2 point = currentPoint.position - transform.position;

        if (isDead)
        {
            _enemyState = 0;
        }
        else
        {
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
                _enemyState = 2;
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
                _enemyState = 2;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f &&
                currentPoint == pointB.transform)
            {
                StartCoroutine(idle());
                flip();
                _enemyState = 2;
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f &&
                currentPoint == pointA.transform)
            {
                StartCoroutine(idle());
                flip();
                _enemyState = 2;
                currentPoint = pointB.transform;
            }
        }

        anim.SetInteger("enemyState", _enemyState);
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    IEnumerator idle()
    {
        _enemyState = 1;
        yield return new WaitForSeconds(1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }

    public void Die()
    {
        Destroy(gameObject, 0.5f);
    }
}
