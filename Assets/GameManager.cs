using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    private void OnEnable()
    {
        PlayerManager.Instance.OnLetterCollect += OnCollect;
    }
    private void Start()
    {
        SoundManager.Instance.SetMusic(music);
    }

    private void OnCollect(LetterClass letter)
    {
        print("Collected " + letter.Letter);
    }


    private void OnDisable()
    {
        PlayerManager.Instance.OnLetterCollect -= OnCollect;
    }


}
