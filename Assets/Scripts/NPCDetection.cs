using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDetection : MonoBehaviour
{
    private GameObject player;
    public float speed;
    private bool FOUNDYOUUU;
    private bool in_detection_cone;
    [SerializeField] private GameObject npc;
    private Rigidbody2D npc_rigidBody;
    [SerializeField] private PointEffector2D gravity;

    public bool stop;

    public float detection_progress = 0;
    [SerializeField] private Slider progress_meter;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        npc_rigidBody = npc.GetComponent<Rigidbody2D>();
    }

    void Update() {
        if (in_detection_cone)
        {
            if (detection_progress < 1) detection_progress += 0.003f;
            else FOUNDYOUUU = true;
        }
        else
        {
            if (detection_progress > 0) detection_progress -= 0.003f;
            else detection_progress = 0;
        }

        if (!stop && FOUNDYOUUU) {
            progress_meter.gameObject.SetActive(false);
            gravity.enabled = false;
            npc_rigidBody.position = Vector3.Lerp(npc_rigidBody.position, player.GetComponent<Rigidbody2D>().position, speed);
        }
        else if (detection_progress == 0)
        {
            progress_meter.gameObject.SetActive(false);
        }
        else
        {
            progress_meter.value = detection_progress;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player) {
            in_detection_cone = true;
            progress_meter.gameObject.SetActive(true);
            Debug.Log("player entered detection");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            in_detection_cone = false;
            Debug.Log("player exited detection");
        }
    }
}
