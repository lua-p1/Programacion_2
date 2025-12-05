using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class VolumeSettings : MonoBehaviour
{
    [Header("Mixer")]
    public AudioMixer audioMixer;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("Master", 1f);
        musicSlider.value = PlayerPrefs.GetFloat("Music", 1f);
        sfxSlider.value = PlayerPrefs.GetFloat("Sound", 1f);

        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
    }
    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("Master", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Master", value);
    }
    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Music", value);
    }
    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("Sound", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Sound", value);
    }
}