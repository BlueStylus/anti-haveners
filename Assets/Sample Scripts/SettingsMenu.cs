using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    // static variables are variables that there is only one instance of
    public AudioMixer ambienceMixer;
    public AudioMixer voiceMixer;
    public static float mouseSens = 0.5f;
    public bool captionToggler;

    void Awake()
    {
        GetComponent<Canvas>().enabled = false;
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSens = sensitivity * 200f;
    }

    public void SetAmbienceVolume(float sliderValue)
    {
        ambienceMixer.SetFloat("musicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetVoiceVolume(float sliderValue)
    {

        voiceMixer.SetFloat("voiceVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetCaptionToggle(bool toggle)
    {

        CaptionHandler.captionToggle = toggle;
    }
}