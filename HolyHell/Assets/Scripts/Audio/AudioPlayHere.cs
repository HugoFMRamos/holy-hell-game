using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayHere : MonoBehaviour
{
    public AudioClip audioClip;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    public void PlayThisAudio()
    {
        audioSource.PlayOneShot(audioClip);
    }
}
