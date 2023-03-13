using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string LevelWord;
    [SerializeField] private LetterClass[] letters;
    [HideInInspector] public int CurrentLetterIndex = 0;
    public event Action OnLevelEnd;

    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        for (int i = 0; i < LevelWord.Length; i++)
        {
            letters[i].Letter = LevelWord[i];
        }
    }
    public bool TryCollectLetter(LetterClass letter)
    {
        if (letter.Letter.ToString().ToUpper() == LevelWord.ToCharArray()[CurrentLetterIndex].ToString().ToUpper())
        {
            CurrentLetterIndex++;
            if (CurrentLetterIndex > (LevelWord.Length - 1))
            {
                OnLevelEnd();
            }
            return true;
        }
        else
            return false;
    }
}
