using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGate : MonoBehaviour
{
    [SerializeField] private Color32 closedColor, openedColor;
    [SerializeField] private AudioClip openSFX;
    private bool canPass = false;
    private Collider2D colider;
    private SpriteRenderer spriteRenderer;
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelEnd += OnLevelEnd;
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        colider = GetComponent<Collider2D>();
        spriteRenderer.color = closedColor;
        colider.isTrigger = false;
    }
    private void OnLevelEnd()
    {
        spriteRenderer.color = openedColor;
        colider.isTrigger = true;
        canPass = true;
        SoundManager.Instance.PlaySound(openSFX);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canPass)
        {
            LevelLoader.Instance?.GoToNextLevel();
        }
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnLevelEnd -= OnLevelEnd;
    }

}
