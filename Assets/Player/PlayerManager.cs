using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public event Action OnLetterCollect;
    public static PlayerManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D _collider = collision.collider;
        if (_collider.CompareTag("Letter"))
        {
            var _level = _collider.GetComponent<LetterClass>();
            var _isValid = LevelManager.Instance.TryCollectLetter(_level);
            if (_isValid)
            {
                OnLetterCollect();
                Destroy(_collider.gameObject);
            }
        }
    }
}
