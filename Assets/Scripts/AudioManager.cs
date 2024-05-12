using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSourceBackground;
    public AudioSource audioSourceSFX;
    public AudioClip jumpSFX, hitSFX, throwSFX, background;

    private void Start()
    {
        audioSourceBackground.clip = background;
        audioSourceBackground.Play();
    }

    public void PlayEffect(AudioClip clip)
    {
        audioSourceSFX.PlayOneShot(clip);
    }
}
