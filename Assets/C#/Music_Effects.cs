using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Music_Effects : MonoBehaviour
{
    private AudioSource m_AudioSource;
    public AudioClip StartMusic;
    public AudioClip WaveSound;
    public AudioClip DoorSound;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        m_AudioSource.PlayOneShot(StartMusic);

    }
    public void WaveSoundPlay()
    {
        m_AudioSource.PlayOneShot(WaveSound);
    }
    public void DoorSoundPlay()
    {
        m_AudioSource.PlayOneShot(DoorSound);
    }
}