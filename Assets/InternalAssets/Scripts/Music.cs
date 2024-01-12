using System;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance { get; private set; }

    [SerializeField] private AudioSource _music;

    [SerializeField] private AudioClip _loading;
    [SerializeField] private AudioClip _idle;
    [SerializeField] private AudioClip _fail;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            throw new Exception("Music is already initialized.");
        }
        Instance = this;
        PlayIdle();
    }

    public void PlayLoading()
    {
        _music.Stop();
        _music.clip = _loading;
        _music.Play();
    }

    public void PlayIdle()
    {
        _music.Stop();
        _music.clip = _idle;
        _music.Play();
    }

    public void PlayFail()
    {
        _music.Stop();
        _music.loop = false;
        _music.clip = _fail;
        _music.Play();
    }
}
