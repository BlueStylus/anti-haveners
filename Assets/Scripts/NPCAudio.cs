using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCAudio : MonoBehaviour
{
    // LIST OF AUDIOS FOR EACH LIL GUY

    public AudioClip[] audios;
    private AudioSource player;
    public bool playNow = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playNow && !player.isPlaying)
        {
            int i = Random.Range(0, audios.Length);
            player.clip = audios[i];
            player.Play();
        } else if (!playNow && player.isPlaying)
        {
            player.Stop();
        }
    }
}
