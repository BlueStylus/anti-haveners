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
    private TimeSpan _currentTime;
    // Trigger time: 9:30 AM
    private TimeSpan _endTriggerTime = new TimeSpan(9, 30, 0);

    private TextMeshProUGUI timeText;

    public bool HasReachedTargetTime { get; private set; } = false;

    private bool _resetOnScene4;

    public string timeString;

    void Awake()
    {
        timeText = GetComponent<TextMeshProUGUI>();

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        _currentTime = _startTime;
        _lastCheck = TimeUtil.time;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        // Calculate delta time and add it to the current time
        float deltaTime = TimeUtil.time - _lastCheck;
        _lastCheck = TimeUtil.time;

        _currentTime += TimeSpan.FromSeconds(deltaTime);

        // Update the UI text
        if (timeText != null)
        {
            timeString = _currentTime.ToString(@"hh\:mm") + " AM";
            timeText.text = timeString;
        }

        // Check if we reached or surpassed the trigger time
        if (!HasReachedTargetTime && _currentTime >= _endTriggerTime)
        {
            HasReachedTargetTime = true;
            Debug.Log("Game Over!");
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Bedroom")
        {
            _currentTime = _startTime;
            HasReachedTargetTime = false;
        }
    }
}
