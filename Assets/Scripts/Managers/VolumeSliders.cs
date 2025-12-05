using UnityEngine;
using UnityEngine.UI;

public class VolumeSliders : MonoBehaviour
{
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        // Cargar valores guardados en los sliders
        masterSlider.value = AudioManager.Instance.masterVolume;
        musicSlider.value = AudioManager.Instance.musicVolume;
        sfxSlider.value = AudioManager.Instance.sfxVolume;

        // Suscripción a cambios
        masterSlider.onValueChanged.AddListener(AudioManager.Instance.SetMasterVolume);
        musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
    }
}
