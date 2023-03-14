using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public UnityEvent<LetterClass> LetterCollectEvent;
    public static PlayerManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D _collider)
    {
        var intactable = _collider.GetComponent<IInteractable>();
        if (intactable != null)
        {
            intactable.Interact(gameObject);
        }

        if (_collider.CompareTag("Letter"))
        {
            var _letter = _collider.GetComponent<LetterClass>();

        }

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        var collider = other.collider;
        var intactable = collider.GetComponent<IInteractable>();
        if (intactable != null)
        {
            intactable.Interact(gameObject);
        }
    }
}
