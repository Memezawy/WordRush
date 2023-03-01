using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelWordText;
    [SerializeField] private Color32 notFoundColor, foundColor;
    [SerializeField] private GameObject pauseMenu;
    private string levelWord;


    private void OnEnable()
    {
        PlayerManager.Instance.OnLetterCollect += OnLetterCollect;
    }

    private void Start()
    {
        levelWord = LevelManager.Instance.LevelWord;
        levelWordText.text = levelWord;
        levelWordText.color = notFoundColor;
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TriggerPauseMenu();
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
        levelWordText.text = ReplaceCharWithString(letter.Letter, $"<color=#{ColorUtility.ToHtmlStringRGBA(foundColor)}>{letter.Letter}</color>");

    }

    private void OnDisable()
    {
        PlayerManager.Instance.OnLetterCollect += OnLetterCollect;
    }

    private string ReplaceCharWithString(char charToReplace, string stringToReplaceWith)
    {
        // Split the original string into an array of characters
        char[] chars = levelWordText.text.ToCharArray();

        // Loop through each character in the array
        for (int i = 0; i < chars.Length; i++)
        {
            // If the character matches the one we want to replace
            if (chars[i] == charToReplace)
            {
                // Remove the character and insert the new string
                levelWordText.text = levelWordText.text.Remove(i, 1).Insert(i, stringToReplaceWith);
                i += stringToReplaceWith.Length - 1; // Adjust index for the added length
            }
        }

        return levelWordText.text;
    }


}
