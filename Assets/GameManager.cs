using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

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
