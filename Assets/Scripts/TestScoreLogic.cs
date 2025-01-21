using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScoreLogic : MonoBehaviour
{
    public bool failed;
    private NPCDialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponent<NPCDialogue>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogue.finished)
        {
            if (failed)
            {
                SceneManager.LoadScene("Bedroom");
            }
            else
            {
                SceneManager.LoadScene("Win");
            }
        }
    }
}
