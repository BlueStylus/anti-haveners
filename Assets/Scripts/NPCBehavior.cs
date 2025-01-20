using System.Collections;
using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    // Serialized Fields
    public GameObject[] points; // Array of points
    public float speed;
    public float idleTime;

    private Rigidbody2D rb;
    private int index; // Current target index
    // 0 = idle down, -1 = walk down, -2 = idle up, -3 = walk up
    // 1 = idle left, 2 = walk left, 3 = idle right, 4 = walk right

    private bool isIdle = false;

    public Transform[] doNotRotate;

    public bool loop;

    public NPCAnimations anim;

    private int npcState = 0;
    private int previousNpcState = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        index = 1;
        Turn(index);
        previousNpcState = npcState; // Initialize previous state
    }

    // Update is called once per frame
    private void Update()
    {
        if (points.Length == 0 || isIdle) return; // Ensure points array is not empty

        // Move towards the current point
        Vector2 point = points[index].transform.position - transform.position;
        Vector2 direction = point.normalized;

        // Set Rigidbody velocity to move the NPC
        rb.velocity = direction * speed;

        // Determine walking state based on velocity
        if (!isIdle && rb.velocity.magnitude > 0.1f)
        {
            if (npcState == 0) npcState = -1; // Walk down
            else if (npcState == 1) npcState = 2; // Walk left
            else if (npcState == -2) npcState = -3; // Walk up
            else if (npcState == 3) npcState = 4; // Walk right

            // Update animation only if npcState changes
            if (npcState != previousNpcState)
            {
                //Debug.Log((npcState, previousNpcState));
                previousNpcState = npcState;
                anim.ChangeAnimation(npcState);
            }
        }

        // Check if the NPC reached the point
        if (Vector2.Distance(transform.position, points[index].transform.position) < 0.2f)
        {
            StartCoroutine(Idle());
            // Move to the next point (looping back to start)
            index = (index + 1) % points.Length;
            if (index == 0 && !loop)
            {
                this.enabled = false;
            }
        }
    }

    private void Turn(int index)
    {
        float degrees = points[index].GetComponent<NPCPathPoint>().rotation;

        // Rotate NPC
        transform.Rotate(0, 0, degrees);

        // Counter-rotate specified objects
        foreach (Transform t in doNotRotate)
        {
            t.Rotate(0, 0, -degrees);
        }

        // Get the normalized rotation angle
        float zRotation = transform.rotation.eulerAngles.z;

        // Determine npcState based on rotation
        if (zRotation >= 315 || zRotation < 45)      // Facing down
            npcState = -2; // Idle down
        else if (zRotation >= 45 && zRotation < 135) // Facing left
            npcState = 1; // Idle left
        else if (zRotation >= 135 && zRotation < 225) // Facing up
            npcState = 0; // Idle up
        else if (zRotation >= 225 && zRotation < 315) // Facing right
            npcState = 3; // Idle right

        // Update animation only if npcState changes
        if (npcState != previousNpcState)
        {
            previousNpcState = npcState;
            anim.ChangeAnimation(npcState);
        }

        // Debug.Log($"NPC State: {npcState}, Rotation: {zRotation}");
    }

    private IEnumerator Idle()
    {
        isIdle = true;

        // Stop movement during idle
        rb.velocity = Vector2.zero;

        // Set idle state based on the current direction
        if (npcState == -1) npcState = 0; // Idle down
        else if (npcState == 2) npcState = 1; // Idle left
        else if (npcState == -3) npcState = -2; // Idle up
        else if (npcState == 4) npcState = 3; // Idle right

        // Update animation only if npcState changes
        if (npcState != previousNpcState)
        {
            previousNpcState = npcState;
            anim.ChangeAnimation(npcState);
        }

        yield return new WaitForSeconds(idleTime);

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
