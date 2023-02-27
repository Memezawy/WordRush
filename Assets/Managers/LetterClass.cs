using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LetterClass : MonoBehaviour
{
    public char Letter;

    private TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = Letter.ToString();
    }
}
