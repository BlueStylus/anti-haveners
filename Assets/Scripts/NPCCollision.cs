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

    public AudioClip sound;
    private AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        dialogueComponent = GetComponent<NPCDialogue>();
        dialogueComponent.enabled = false;
        behaviorComponent = GetComponent<NPCBehavior>();
        behaviorComponent.enabled = true;

        audioPlayer = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!collidedOnce)
        {
            if (col.gameObject == player)
            {
                detection.stop = true;
                col.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                player.GetComponent<Player>().disableInputs = true;
                rb.velocity = Vector2.zero;
                detection_cone.SetActive(false);
                dialogueComponent.enabled = true;
                behaviorComponent.enabled = false;
                collidedOnce = true;
                audioPlayer.clip = sound;
                audioPlayer.Play();

                GameObject[] dialogueSystems = GameObject.FindGameObjectsWithTag("DialogueSystem");

                foreach (GameObject ds in dialogueSystems)
                {
                    ds.SetActive(false);
                }

                GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

                foreach (GameObject npc in npcs)
                {
                    npc.GetComponent<NPCBehavior>().enabled = false;
                    npc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
            }
        }
    }

}
