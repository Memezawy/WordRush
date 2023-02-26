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
        text.text = Letter.ToString();
    }
}
