using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    // static variables are variables that there is only one instance of
    public AudioMixer musicMixer;
    public AudioMixer soundMixer;
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
        musicMixer.SetFloat("musicVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetVoiceVolume(float sliderValue)
    {

        soundMixer.SetFloat("voiceVolume", Mathf.Log10(sliderValue) * 20);
    }

    public void SetCaptionToggle(bool toggle)
    {

        CaptionHandler.captionToggle = toggle;
    }
}