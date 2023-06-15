using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private Slider volumeSlider;
    private AudioSource music;

    void Awake()
    {
        volumeSlider = GetComponent<Slider>();
    }

    void Start()
    {
        music = BackgroundMusic.instance.GetComponent<AudioSource>();
        float volume = PlayerPrefs.GetFloat("volume", 1.0f);
        volumeSlider.value = volume;
        music.volume = volumeSlider.value;
    }

    public void ChangeVolume()
    {
        float volume = volumeSlider.value;
        music.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }
}
