using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class DoorAnimation : MonoBehaviour
{
    Tilemap tilemap;

    [SerializeField] TileBase[] bottom_tiles;
    [SerializeField] TileBase[] top_tiles;
    // 0 = closed, 1 = open, 2 = closed

    private bool hidden;

    public GameObject hideInstructions;
    public TextMeshProUGUI instructionsText;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        hideInstructions.SetActive(false);

        hidden = false;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (hideInstructions.activeSelf && Input.GetButtonDown("Jump"))
        {
            if (!hidden)
            {
                instructionsText.text = "[SPACE] to leave";
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 1f);
                player.SetActive(false);
                StartCoroutine(PlayBoth());
            }
            else
            {
                instructionsText.text = "[SPACE] to hide";
                StartCoroutine(PlayBoth());
                player.SetActive(true);
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - 1.5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            hideInstructions.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            hideInstructions.SetActive(false);
        }
    }

    IEnumerator BottomTiles()
    {
        for (int i = 0; i < bottom_tiles.Length - 1; i++)
        {
            tilemap.SwapTile(bottom_tiles[i], bottom_tiles[i + 1]);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator TopTiles()
    {
        for (int i = 0; i < top_tiles.Length - 1; i++)
        {
            tilemap.SwapTile(top_tiles[i], top_tiles[i + 1]);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator PlayBoth()
    {
        hideInstructions.SetActive(false);

        Coroutine bottomCoroutine = StartCoroutine(BottomTiles());
        Coroutine topCoroutine = StartCoroutine(TopTiles());

        // Wait for both animations to complete
        yield return bottomCoroutine;
        yield return topCoroutine;

        hidden = !hidden;
        hideInstructions.SetActive(true);
    }
}
