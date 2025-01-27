using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using TimeUtil = UnityEngine.Time;

public class TimeTracker : MonoBehaviour
{
    private const float TOLERANCE = 0.01f;

    static TimeTracker _instance;
    public static TimeTracker Instance => _instance;

    private float _lastCheck;
    // Start time: 9:25 AM
    private TimeSpan _startTime = new TimeSpan(9, 25, 0);
    // Trigger time: 9:30 AM
    private TimeSpan _endTime = new TimeSpan(9, 30, 0);

    public TimeSpan currentTime = new TimeSpan(9, 25, 0);

    public TextMeshProUGUI timeText;

    public GameObject gameOverDialogue;

    public bool HasReachedTargetTime { get; private set; } = false;

    public string timeString;

    public bool disabled;

    void Awake()
    {
        timeText = GetComponent<TextMeshProUGUI>();

        DontDestroyOnLoad(this.gameObject);

        if (_instance != null && _instance != this)
        {
            // currentTime = _instance.currentTime;
            Destroy(_instance);
            return;
        }
        
        _lastCheck = TimeUtil.time;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (!disabled)
        {
            // Calculate delta time and add it to the current time
            float deltaTime = TimeUtil.time - _lastCheck;
            _lastCheck = TimeUtil.time;

            currentTime += TimeSpan.FromSeconds(deltaTime);
            Debug.Log(currentTime.ToString(@"h\:mm\:ss"));

            // Update the UI text
            if (timeText != null)
            {
                timeString = currentTime.ToString(@"h\:mm") + " AM";
                timeText.text = timeString;
            }

            // Check if we reached or surpassed the trigger time
            if (!HasReachedTargetTime && currentTime >= _endTime)
            {
                HasReachedTargetTime = true;
                if (!gameOverDialogue.activeSelf)
                {
                    TriggerGameOverDialogue();
                    Debug.Log("Game Over");
                }
            }

            // For Game Over:
            if (gameOverDialogue.GetComponent<NPCDialogue>() != null)
            {
                if (gameOverDialogue.GetComponent<NPCDialogue>().finished)
                {
                    gameOverDialogue.GetComponent<NPCDialogue>().i = 0;
                    gameOverDialogue.GetComponent<NPCDialogue>().finished = false;
                    SceneManager.LoadScene("Bedroom");
                }
            }
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Test")
        {
            disabled = true;
        }
        else if (scene.name == "Bedroom")
        {
            currentTime = _startTime;
            HasReachedTargetTime = false;
            Destroy(gameObject);
        }
        else
        {
            disabled = false;
        }
    }

    public void TriggerGameOverDialogue()
    {
        GameObject[] dialogueSystems = GameObject.FindGameObjectsWithTag("DialogueSystem");

        foreach (GameObject ds in dialogueSystems)
        {
            ds.SetActive(false);
        }

        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

        foreach (GameObject npc in npcs)
        {
            npc.SetActive(false);
        }

        GetComponent<AudioSource>().Play();
        gameOverDialogue.SetActive(true);
        Debug.Log(gameOverDialogue.activeInHierarchy);
    }
}
