using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    private AudioSource music;
    [SerializeField] private Slider volumeSlider;

    void Awake()
    {
        music = GetComponent<AudioSource>();
        float volume = PlayerPrefs.GetFloat("volume", 1.0f);
        music.volume = volume;
        volumeSlider.value = volume;
    }

    public void ChangeVolume()
    {
        float volume = volumeSlider.value;
        music.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }
}
