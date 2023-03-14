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
    [HideInInspector] public bool IsPlayingMusic => musicSource.isPlaying;
    [HideInInspector] public bool IsPlayingSfx => effectsSource.isPlaying;
    public static SoundManager Instance { get; private set; }
    public const string MASTER_KEY = "masterVolume";
    public const string SFX_KEY = "sfxVolume";
    public const string MUSIC_KEY = "musicVolume";


    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SetupSliders();
    }

    public void SetupSliders()
    {
        Slider[] _sliders = Resources.FindObjectsOfTypeAll<Slider>();
        musicSlider = _sliders[1];
        float _musicVol, _sfxVol;
        mixer.GetFloat(MUSIC_KEY, out _musicVol);
        musicSlider.value = Mathf.Pow(10, _musicVol / 20);
        musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        musicSlider.onValueChanged.Invoke(musicSlider.value);


        sfxSlider = _sliders[0];
        mixer.GetFloat(SFX_KEY, out _sfxVol);
        sfxSlider.value = Mathf.Pow(10, _sfxVol / 20);
        sfxSlider.onValueChanged.AddListener(ChangeSfxVolume);
        sfxSlider.onValueChanged.Invoke(sfxSlider.value);
    }

    public void PlaySound(AudioClip clip)
    {
        effectsSource.PlayOneShot(clip);
    }
    public void ChangeMusicVolume(float v)
    {
        mixer.SetFloat(MUSIC_KEY, Mathf.Log10(v) * 20);
    }
    public void ChangeSfxVolume(float v)
    {
        mixer.SetFloat(SFX_KEY, Mathf.Log10(v) * 20);
    }
    public void SetMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }
}
