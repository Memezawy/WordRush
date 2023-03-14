using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LetterClass : MonoBehaviour, IInteractable
{
    public char Letter;
    [SerializeField] private float animationSpeed, animationAplitude;
    private TMP_Text text;
    private Vector2 originalPos;
    private float animationOffset;
    private void Start()
    {
        gameObject.name = "Letter " + Letter;
        text = GetComponent<TMP_Text>();
        text.text = Letter.ToString();
        originalPos = transform.position;
        animationOffset = Random.Range(-1f, 1f);
    }

    private void FixedUpdate()
    {
        var pos = new Vector2(originalPos.x, originalPos.y + Mathf.Sin(animationOffset + (Time.time * animationSpeed)) * animationAplitude);
        transform.position = pos;
    }

    public void Interact(GameObject player)
    {
        if (LevelManager.Instance.TryCollectLetter(this))
        {
            player.GetComponent<PlayerManager>().LetterCollectEvent.Invoke(this);
            Destroy(gameObject);
        }
    }
}
