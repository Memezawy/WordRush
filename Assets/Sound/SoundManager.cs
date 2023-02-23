using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource, effectsSource;
    public static SoundManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
    public void SetMusic(AudioClip music)
    {
        musicSource.PlayOneShot(music);
    }

    public void ChangeMasterVolume(float vol)
    {
        AudioListener.volume = vol;
    }
}
