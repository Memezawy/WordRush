using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource musicSource, effectsSource;
    [SerializeField] private Slider musicSlider, sfxSlider;
    public static SoundManager Instance { get; private set; }

    public const string MASTER_KEY = "masterVolume";
    public const string SFX_KEY = "sfxVolume";
    public const string MUSIC_KEY = "musicVolume";


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        sfxSlider.onValueChanged.AddListener(ChangeSfxVolume);
    }

    public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
    private void ChangeMusicVolume(float v)
    {
        mixer.SetFloat(MUSIC_KEY, Mathf.Log10(v) * 20);
    }
    private void ChangeSfxVolume(float v)
    {
        mixer.SetFloat(SFX_KEY, Mathf.Log10(v) * 20);
    }
    public void SetMusic(AudioClip music)
    {
        musicSource.Stop();
        musicSource.PlayOneShot(music);
    }
}
