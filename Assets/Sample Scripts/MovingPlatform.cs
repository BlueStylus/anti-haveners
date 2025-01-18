using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public float speed = 3;
    public bool vertical;

    private Transform currentPoint;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        flip();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.position.y);

        Vector2 point = currentPoint.position - transform.position;

        if (vertical)
        {
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(0, -speed);
            }
            else
            {
                rb.velocity = new Vector2(0, speed);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f &&
            currentPoint == pointB.transform)
            {
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f &&
                currentPoint == pointA.transform)
            {
                currentPoint = pointB.transform;
            }
        }
        else
        {
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f &&
            currentPoint == pointB.transform)
            {
                flip();
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f &&
                currentPoint == pointA.transform)
            {
                flip();
                currentPoint = pointB.transform;
            }
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}
