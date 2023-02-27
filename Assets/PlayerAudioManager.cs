using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip stepSFX, jumpSFX;
    [SerializeField] private AudioSource audioSource;

    public static PlayerAudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayerStepSound()
    {
        audioSource.PlayOneShot(stepSFX);
    }

    public void PlayerJumpSound()
    {
        audioSource.PlayOneShot(jumpSFX);
    }
}
