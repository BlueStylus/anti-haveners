using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [System.Serializable] public class DialogueTree{
        public string text;
        public string[] choices;
        public int jumpIfChoice1;
        public int jumpIfChoice2;
    }

    public DialogueTree[] dialogue;

    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        dialogueSystem.SetActive(true);

        dialogueSystemPortrait.sprite = portrait;
        dialogueSystemName.text = npc_name;

        DisplayText(i);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            i++;
            Debug.Log(i);
        }
    }

    void DisplayText(int i) {
        dialogueSystemText.text = dialogue[i].text;
        if (dialogue[i].choices.Length != 0) {
            choice1Text.text = dialogue[i].choices[0];
            choice2Text.text = dialogue[i].choices[1];
            dialogueSystemChoice1.SetActive(true);
            dialogueSystemChoice2.SetActive(true);
        } else {
            dialogueSystemChoice1.SetActive(false);
            dialogueSystemChoice1.SetActive(false);
        }
    }

    public void Choose1() {
        i = dialogue[i].jumpIfChoice1;
    }

    public void Choose2() {
        i = dialogue[i].jumpIfChoice2;
    }
}
