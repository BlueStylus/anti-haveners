using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLoopRestartCutscene : MonoBehaviour
{
    private NPCDialogue dialogue;
    private NPCBehavior behavior;

    public GameObject exitZone;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponent<NPCDialogue>();
        behavior = GetComponent<NPCBehavior>();

        dialogue.enabled = true;
        behavior.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogue.finished)
        {
            StartCoroutine(WaitSomeTime());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == exitZone)
        {
            Debug.Log("Collided!");
            //load the lobby of the house
            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator WaitSomeTime()
    {
        yield return new WaitForSeconds(5);
        dialogue.dialogueSystem.SetActive(false);
        dialogue.enabled = false;
        behavior.enabled = true;
    }
}
