using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGate : MonoBehaviour
{
    private bool canPass = false;
    private Collider2D colider;
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelEnd += OnLevelEnd;
    }
    private void Start()
    {
        colider = GetComponent<Collider2D>();
        colider.isTrigger = false;
    }
    private void OnLevelEnd()
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        colider.isTrigger = true;
        canPass = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canPass)
        {
            LevelLoader.Instance.GoToNextLevel();
        }
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnLevelEnd -= OnLevelEnd;
    }

}
