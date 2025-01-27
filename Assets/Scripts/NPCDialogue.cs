using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class NPCDialogue : MonoBehaviour
{
    // FIELDS
    public Sprite portrait;
    public string npc_name;
    public GameObject dialogueSystem;
    public Image dialogueSystemPortrait;
    public TextMeshProUGUI dialogueSystemName;
    public TextMeshProUGUI dialogueSystemText;

    public GameObject dialogueSystemChoice1;
    public TextMeshProUGUI choice1Text;
    public GameObject dialogueSystemChoice2;
    public TextMeshProUGUI choice2Text;

    private NPCBehavior behaviorComponent;

    private NPCAudio audioComponent;

    public GameObject detectionCone;
    public NPCDetection detectionMeter;

    [System.Serializable]
    public class DialogueTree
    {
        public string text;
        public string[] choices;
        public int jumpIfChoice1;
        public int jumpIfChoice2;
        public bool finisher;
    }

    public DialogueTree[] dialogue;

    [SerializeField] private float typingSpeed = 0.04f;
    private bool canContinueDialogue = false;
    [SerializeField] private GameObject continueIcon;

    public int i = 0;

    public bool finished;

    private GameObject player;

    public bool bustsKneecaps;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] dialogueSystems = GameObject.FindGameObjectsWithTag("DialogueSystem");

        foreach (GameObject ds in dialogueSystems)
        {
            ds.SetActive(false);
        }

        dialogueSystem.SetActive(true);

        dialogueSystemPortrait.sprite = portrait;
        dialogueSystemName.text = npc_name;

        behaviorComponent = GetComponent<NPCBehavior>();
        audioComponent = GetComponent<NPCAudio>();
        player = GameObject.FindGameObjectWithTag("Player");

        finished = false;

        DisplayText(i);
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished && Input.GetMouseButtonDown(0) && canContinueDialogue)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            var results = new System.Collections.Generic.List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            GameObject clickedObject = null;
            if (results.Count > 0)
            {
                clickedObject = results[0].gameObject;
                // Debug.Log(clickedObject);
            }

            if (clickedObject == dialogueSystemChoice1 || clickedObject == dialogueSystemChoice2
                || clickedObject == choice1Text.gameObject || clickedObject == choice2Text.gameObject)
            {
                // Do nothing; let the button handle the event.
                return;
            }

            i++;
            DisplayText(i);
        }
        else if (finished && Input.GetMouseButtonDown(0) && canContinueDialogue)
        {
            GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

            foreach (GameObject npc in npcs)
            {
                npc.GetComponent<NPCBehavior>().enabled = false;
                npc.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

            this.enabled = false;
            behaviorComponent.enabled = true;
            player.GetComponent<Player>().disableInputs = false;
            dialogueSystem.SetActive(false);
            detectionCone.SetActive(false);
            detectionMeter.gameObject.SetActive(false);
            detectionMeter.enabled = false;
        }
    }

    void DisplayText(int i)
    {
        dialogueSystemChoice1.SetActive(false);
        dialogueSystemChoice2.SetActive(false);

        StartCoroutine(DisplayLine(dialogue[i].text));
    }

    public void Choose1()
    {
        i = dialogue[i].jumpIfChoice1;
        // Debug.Log(i);
        DisplayText(i);
    }

    public void Choose2()
    {
        i = dialogue[i].jumpIfChoice2;
        // Debug.Log(i);
        DisplayText(i);
    }

    private IEnumerator DisplayLine(string line)
    {
        audioComponent.playNow = true;
        continueIcon.SetActive(false);

        // empty the dialogue text
        dialogueSystemText.text = "";

        canContinueDialogue = false;

        //display each letter at a time
        foreach (char letter in line.ToCharArray())
        {
            dialogueSystemText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        if (dialogue[i].finisher)
        {
            finished = true;
            if (bustsKneecaps)
            {
                player.GetComponent<Player>().walkSpeed = 2;
            }
        }

        if (dialogue[i].choices.Length == 0)
        {
            canContinueDialogue = true;
            continueIcon.SetActive(true);
            dialogueSystemChoice1.SetActive(false);
            dialogueSystemChoice2.SetActive(false);

        }
        else if (dialogue[i].choices.Length == 1)
        {
            continueIcon.SetActive(false);
            choice2Text.text = dialogue[i].choices[0];
            dialogueSystemChoice1.SetActive(false);
            dialogueSystemChoice2.SetActive(true);
        }
        else
        {
            continueIcon.SetActive(false);
            choice1Text.text = dialogue[i].choices[0];
            choice2Text.text = dialogue[i].choices[1];
            dialogueSystemChoice1.SetActive(true);
            dialogueSystemChoice2.SetActive(true);
        }

        audioComponent.playNow = false;
    }
}
