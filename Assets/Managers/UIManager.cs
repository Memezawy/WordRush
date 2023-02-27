using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text levelWordText;


    private void OnEnable()
    {
    }

    private void Start()
    {
        PlayerManager.Instance.OnLetterCollect += OnLetterCollect;
        levelWordText.text = "";
    }

    private void OnLetterCollect(LetterClass letter)
    {
        levelWordText.text += letter.Letter;
    }



}
