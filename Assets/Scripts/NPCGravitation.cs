using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGravitation : MonoBehaviour
{
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player) {
            Debug.Log("player entered gravity");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player) {
            Debug.Log("player exit gravity");
        }
    }
}
