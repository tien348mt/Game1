using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioScene : MonoBehaviour
{
    public AudioClip newMusic;

    private void Start()
    {
        AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (audioManager != null)
        {
            AudioSource audioSource = audioManager.GetComponent<AudioSource>();
            if (audioSource.clip != newMusic)
            {
                audioSource.clip = newMusic;
                audioSource.Play();
            }
        }
    }
}
