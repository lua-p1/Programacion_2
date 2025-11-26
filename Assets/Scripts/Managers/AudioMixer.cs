using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixer : MonoBehaviour
{
    [SerializeField] UnityEngine.Audio.AudioMixer mixer;
    [SerializeField] private Slider _soundVolume;
    [SerializeField] private Slider _musicVolume;
    private void Start()
    {
        float _sfxMusic = PlayerPrefs.GetFloat("Music", -23f);
        _musicVolume.value = _sfxMusic;
        float _sfxSound = PlayerPrefs.GetFloat("Sound", -23f);
        _soundVolume.value = _sfxSound;
    }
    public void SetMusicVolume(float value)
    {
        mixer.SetFloat("Music",value);
    }
    public void SetSoundVolume(float value)
    {
        mixer.SetFloat("Sound", value);
    }
}
