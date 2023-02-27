using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string LevelWord;
    public int CurrentLetterIndex = 0;

    public event Action OnLevelEnd;

    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public bool TryCollectLetter(LetterClass letter)
    {
        if (letter.Letter == LevelWord.ToCharArray()[CurrentLetterIndex])
        {
            CurrentLetterIndex++;
            if (CurrentLetterIndex >= LevelWord.Length)
            {
                OnLevelEnd();
            }
            return true;
        }
        else
            return false;
    }
}
