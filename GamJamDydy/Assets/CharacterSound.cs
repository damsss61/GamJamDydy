using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    AudioSource audio;
    public AudioClip step;
    public AudioClip blabla;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void PlayStep()
    {
        audio.PlayOneShot(step);

    }

    public void PlayBlabla()
    {
        audio.PlayOneShot(blabla);

    }
}
