using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // This script is an example of a SINGLETON OBJECT

    // This singular line makes the class PauseManager into a Singleton
    // the get; set; is changing the set to private but keeping get as public
    // means any object has permission to get the instance, no object has permission to set/change it

    // MAKE SURE YOU PUT A NEW PAUSE MANAGER GAME OBJECT IN EVERY SCENE!!!!

    public static PauseManager Instance { get; private set; }

    public bool isPaused { get; private set; }

    [SerializeField] private GameObject _pauseMenu;

    private void Awake()
    {
        //if there is already an instance of pause, delete self
        if (Instance != null && Instance != this)
        {
            // this = object that has this component
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }

    // These 2 functions COULD be one function that takes a bool
    public void Pause()
    {
        isPaused = true;
        _pauseMenu.GetComponent<Canvas>().enabled = true;
        Cursor.lockState = CursorLockMode.None;
        // change the rate at which real time occurs to stop completely
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void Unpause()
    {
        isPaused = false;
        _pauseMenu.GetComponent<Canvas>().enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }
}
