using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundFixer : MonoBehaviour
{
    private AudioSource m_AudioSource;
    public AudioClip ButtonSound;
    public AudioClip BGMusic;
    public Toggle m_Toggle;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    public void PlayClickSound()
    {
        m_AudioSource.clip = ButtonSound;
        m_AudioSource.PlayOneShot(ButtonSound);
    }
}