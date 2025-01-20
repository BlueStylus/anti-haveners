using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollision : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public NPCDetection detection;
    [SerializeField] private GameObject detection_cone;

    private NPCDialogue dialogueComponent;
    private NPCBehavior behaviorComponent;

    private bool collidedOnce;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        dialogueComponent = GetComponent<NPCDialogue>();
        dialogueComponent.enabled = false;
        behaviorComponent = GetComponent<NPCBehavior>();
        behaviorComponent.enabled = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!collidedOnce)
        {
            if (col.gameObject == player)
            {
                detection.stop = true;
                col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                player.GetComponent<Player>().disableInputs = true;
                rb.velocity = new Vector2(0, 0);
                detection_cone.SetActive(false);
                dialogueComponent.enabled = true;
                behaviorComponent.enabled = false;
                collidedOnce = true;
            }
        }
    }

}
