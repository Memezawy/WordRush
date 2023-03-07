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
        levelWordText.text = Utiles.ReplaceCharWithString(levelWordText.text, letter.Letter,
         $"<color=#{ColorUtility.ToHtmlStringRGBA(foundColor)}>{letter.Letter}</color>");
    }


    private void OnDisable()
    {
        PlayerManager.Instance.OnLetterCollect += OnLetterCollect;
    }

}
