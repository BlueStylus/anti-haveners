using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDetection : MonoBehaviour
{
    private GameObject player;
    public float speed;
    private bool FOUNDYOUUUU;
    [SerializeField] private GameObject npc;
    private Rigidbody2D npc_rigidBody;
    [SerializeField] private PointEffector2D gravity;

    public bool stop;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        npc_rigidBody = npc.GetComponent<Rigidbody2D>();

        FOUNDYOUUUU = false;
    }

    void Update() {
        if (FOUNDYOUUUU && !stop) {
            gravity.enabled = false;
            npc_rigidBody.position = Vector3.Lerp(npc_rigidBody.position, player.GetComponent<Rigidbody2D>().position, speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player) {
            Debug.Log("player entered detection");
            FOUNDYOUUUU = true;
        }
    }
}
