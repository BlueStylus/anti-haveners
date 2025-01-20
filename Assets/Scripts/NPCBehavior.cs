using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    // Serialized Fields
    public GameObject[] points; // Array of points
    public float speed;
    public float idleTime;

    private Rigidbody2D rb;
    private BoxCollider2D c;
    // private Animator anim;
    private int index; // Current target index
    private int _enemyState;
    // 1 = idle, 2 = movement

    private bool isIdle = false;

    public Transform[] doNotRotate;

    public bool loop;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();

        _enemyState = 1;
        index = 1;
        Turn(index);
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Length == 0 || isIdle) return; // Ensure points array is not empty

        // Move towards the current point
        Vector2 point = points[index].transform.position - transform.position;
        Vector2 direction = point.normalized;
        rb.velocity = direction * speed;

        if (Vector2.Distance(transform.position, points[index].transform.position) < 0.2f)
        {
            StartCoroutine(Idle());
            // Move to the next point (looping back to start)
            index = (index + 1) % points.Length;
            if (!loop)
            {
                this.enabled = false;
            }
        }
    }

    private void Turn(int index)
    {
        float degrees = points[index].GetComponent<NPCPathPoint>().rotation;

        transform.Rotate(0, 0, degrees);
        foreach (Transform t in doNotRotate)
        {
            t.Rotate(0, 0, -degrees);
        }
        // Debug.Log(transform.rotation);
    }

    IEnumerator Idle()
    {
        isIdle = true;
        _enemyState = 1;

        // Stop movement during idle
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(idleTime);
        _enemyState = 2;
        Turn(index);
        isIdle = false;
    }

    private void OnDrawGizmos()
    {
        if (points.Length > 1)
        {
            for (int i = 0; i < points.Length; i++)
            {
                Gizmos.DrawWireSphere(points[i].transform.position, 0.5f);

                // Draw lines between consecutive points
                if (i < points.Length - 1)
                {
                    Gizmos.DrawLine(points[i].transform.position, points[i + 1].transform.position);
                }
                // Draw a line from the last point to the first
                else
                {
                    Gizmos.DrawLine(points[i].transform.position, points[0].transform.position);
                }
            }
        }
    }
}
