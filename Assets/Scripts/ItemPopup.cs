using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPopup : MonoBehaviour
{
    // FIELDS
    public GameObject message;
    private GameObject player;

    public bool moving;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (moving)
        {
            message.transform.position = new Vector2(transform.position.x, transform.position.y + 1.5f);
        }
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
