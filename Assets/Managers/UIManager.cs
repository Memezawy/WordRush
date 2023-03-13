using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelWordText;
    [SerializeField] private Color32 notFoundColor, foundColor;
    [SerializeField] private GameObject pauseMenu, settingsMenu;


    private string levelWord;
    private string[] letterArray;

    private void OnEnable()
    {
        PlayerManager.Instance.OnLetterCollect += OnLetterCollect;
    }

    private void Start()
    {
        levelWord = LevelManager.Instance.LevelWord;
        levelWordText.text = levelWord;
        levelWordText.color = notFoundColor;
        letterArray = new string[levelWord.Length];
        for (int i = 0; i < levelWord.Length; i++)
            letterArray[i] = levelWord[i].ToString();
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TriggerPauseMenu();
            settingsMenu.SetActive(false);
        }
    }

    private void TriggerPauseMenu()
    {
        if (GameManager.Instance.IsPaused)
            GameManager.Instance.ResumeGame();
        else
            GameManager.Instance.PauseGame();

        pauseMenu.SetActive(GameManager.Instance.IsPaused);
    }


    private void OnLetterCollect(LetterClass letter)
    {
        var letterIndex = levelWord.IndexOf(letter.Letter);
        var newString = $"<color=#{ColorUtility.ToHtmlStringRGBA(foundColor)}>{letter.Letter}</color>";
        levelWordText.text = Replace(letterIndex, newString);
    }

    private string Replace(int letterIndex, string newString)
    {
        letterArray[letterIndex] = newString;

        string s = "";
        foreach (var letter in letterArray)
        {
            s += letter;
        }
        return s;
    }

    private void OnDisable()
    {
        PlayerManager.Instance.OnLetterCollect += OnLetterCollect;
    }

}
