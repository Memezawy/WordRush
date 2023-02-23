using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    void Start()
    {
        SoundManager.Instance.SetMusic(music);
    }

}
