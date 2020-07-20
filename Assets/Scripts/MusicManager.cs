﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    static bool audioBegin = false;
    new public AudioSource audio;
    public GameObject player;
    public GameObject door;
    private EndScene endSceneScript;

    private void Start()
    {
        endSceneScript = player.GetComponent<EndScene>();
    }

    void Awake()
    {
        if (!audioBegin)
        {
            audio.Play();
            DontDestroyOnLoad(audio);
            audioBegin = true;
        }
    }

    private void Update()
    {
        IEnumerator fadingSound = MusicManager.FadeOut(audio, 1500f);

        if (endSceneScript.displayTitle)
        {
            StartCoroutine(fadingSound);
        }
    }

    public static IEnumerator FadeOut(AudioSource audio, float FadeTime)
    {
        float startVolume = audio.volume;

        while (audio.volume > 0)
        {
            audio.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audio.Stop();
        audio.volume = startVolume;
    }
}