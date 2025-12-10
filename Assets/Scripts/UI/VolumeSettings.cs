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
        float master = PlayerPrefs.GetFloat("Master", 1f);
        float music = PlayerPrefs.GetFloat("Music", 1f);
        float sfx = PlayerPrefs.GetFloat("Sound", 1f);
        masterSlider.value = master;
        musicSlider.value = music;
        sfxSlider.value = sfx;
        SetMasterVolume(master);
        SetMusicVolume(music);
        SetSFXVolume(sfx);
    }
    public void SetMasterVolume(float value)
    {
        float volume = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("Master", volume);
        PlayerPrefs.SetFloat("Master", value);
    }
    public void SetMusicVolume(float value)
    {
        float volume = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat("Music", value);
    }
    public void SetSFXVolume(float value)
    {
        float volume = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 1f)) * 20;
        audioMixer.SetFloat("Sound", volume);
        PlayerPrefs.SetFloat("Sound", value);
        if (AudioManager.Instance != null)
            AudioManager.Instance.masterVolume = value;
    }
}
