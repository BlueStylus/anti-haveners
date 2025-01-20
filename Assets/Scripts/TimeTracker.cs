using System;
using System.Collections;
using TMPro;
using UnityEngine;
using TimeUtil = UnityEngine.Time;

public class TimeTracker : MonoBehaviour
{
    private const float TOLERANCE = 0.01f;

    static TimeTracker _instance;
    public static TimeTracker Instance => _instance;

    private float _lastCheck;
    private TimeSpan _startTime = new TimeSpan(9, 27, 0); // Start time: 9:27 AM
    private TimeSpan _currentTime;
    private TimeSpan _endTriggerTime = new TimeSpan(9, 30, 0); // Trigger time: 9:30 AM

    private TextMeshProUGUI timeText;

    public bool HasReachedTargetTime { get; private set; } = false;

    void Awake()
    {
        timeText = GetComponent<TextMeshProUGUI>();

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        _currentTime = _startTime;
        _lastCheck = TimeUtil.time;
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
            string time = _currentTime.ToString(@"hh\:mm") + " AM";
            timeText.text = time;
        }

        // Check if we reached or surpassed the trigger time
        if (!HasReachedTargetTime && _currentTime >= _endTriggerTime)
        {
            HasReachedTargetTime = true;
            Debug.Log("Target time reached: 9:30 AM");
        }
    }
}
