using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip stepSFX, jumpSFX, collectSFX;

    public static PlayerAudioManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        PlayerManager.Instance.OnLetterCollect += PlayCollectSound;
    }

    private void PlayCollectSound(LetterClass _)
    {
        SoundManager.Instance.PlaySound(collectSFX);
    }


    public void PlayerStepSound()
    {
        SoundManager.Instance.PlaySound(stepSFX);
    }

    public void PlayerJumpSound()
    {
        SoundManager.Instance.PlaySound(jumpSFX);
    }
}
