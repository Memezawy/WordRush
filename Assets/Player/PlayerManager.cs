using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public event Action<LetterClass> OnLetterCollect;
    public static PlayerManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D _collider = other;
        if (_collider.CompareTag("Letter"))
        {
            var _letter = _collider.GetComponent<LetterClass>();
            var _isValid = LevelManager.Instance.TryCollectLetter(_letter);
            if (_isValid)
            {
                OnLetterCollect(_letter);
                Destroy(_collider.gameObject);
            }
        }
    }
}
