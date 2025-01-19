using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPopup : MonoBehaviour
{
    // FIELDS
    public GameObject message;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player) {
            message.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player) {
            message.SetActive(false);
        }
    }
}
