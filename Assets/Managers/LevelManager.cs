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

    private void OnEnable()
    {
        OnLevelEnd += EndLevel;
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
    private void EndLevel()
    {
        print("Game over");
    }

    private void OnDisable()
    {
        OnLevelEnd += EndLevel;
    }

}
