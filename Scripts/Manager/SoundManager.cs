using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BgmType { Stage = 0, Boss }

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] public AudioSource bgmPlayer;
    [SerializeField] private AudioSource sfxPlayer;
    [SerializeField] private AudioClip[] sfxClips;
    [SerializeField] private AudioClip[] bgmClips;

    [SerializeField] private Slider bgm_slider;
    [SerializeField] private Slider sfx_slider;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;

        DontDestroyOnLoad(this);

        bgm_slider.onValueChanged.AddListener(ChangeBgmVolume);
        sfx_slider.onValueChanged.AddListener(ChangeSfxVolume);
    }

    private void Init()
    {
        bgm_slider = bgm_slider.GetComponent<Slider>();
        sfx_slider = sfx_slider.GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        if (bgm_slider == null && sfx_slider == null)
        {
            AddSliders();
        }
    }

    private void AddSliders()
    {
        bgm_slider = GameManager.instance.bgmSlider;
        sfx_slider = GameManager.instance.sfxSlider;

        bgm_slider.onValueChanged.AddListener(ChangeBgmVolume);
        sfx_slider.onValueChanged.AddListener(ChangeSfxVolume);
    }

    private void ChangeSfxVolume(float value)
    {
        sfxPlayer.volume = value;
    }

    private void ChangeBgmVolume(float value)
    {
        bgmPlayer.volume = value;
    }

    public void PlayeSfx(string type)
    {
        int index = 0;
        switch (type)
        {
            case "Bounce_wall": index = 0; break;
            case "Bouncel_paddle": index = 1; break;
            case "Victory": index = 2; break;
            case "Lose": index = 3; break;
            case "BossHit": index = 4; break;
        }

        sfxPlayer.clip = sfxClips[index];
        sfxPlayer.Play();
    }

    public void ChangeBgm(BgmType type)
    {
        bgmPlayer.Stop();
        bgmPlayer.clip = bgmClips[(int)type];
        bgmPlayer.Play();
    }
}
