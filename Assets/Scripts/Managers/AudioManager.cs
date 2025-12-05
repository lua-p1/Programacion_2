using UnityEngine;
using System.Collections.Generic;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Music")]
    public AudioSource musicSource;
    [Header("Clips Disponibles")]
    public AudioClip[] clips;
    [Header("Ajustes Globales")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    public float minPitch = 0.95f;
    public float maxPitch = 1.05f;
    [Header("Pool Settings")]
    public int initialSources = 10;
    private List<AudioSourceController> _sources = new List<AudioSourceController>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        CreateInitialSources();
        masterVolume = PlayerPrefs.GetFloat("Master", 1f);
        float musicVolume = PlayerPrefs.GetFloat("Music", 1f);
        if (musicSource != null)
            musicSource.volume = musicVolume;
        if (musicSource != null && !musicSource.isPlaying)
            musicSource.Play();
    }
    private void CreateInitialSources()
    {
        for (int i = 0; i < initialSources; i++)
            CreateNewSource();
    }
    private AudioSourceController CreateNewSource()
    {
        GameObject obj = new GameObject("AudioSourceController");
        obj.transform.parent = this.transform;
        AudioSourceController controller = obj.AddComponent<AudioSourceController>();
        _sources.Add(controller);
        return controller;
    }
    public void PlaySound(string soundName)
    {
        AudioClip clip = FindAudio(soundName);
        if (clip == null)
        {
            Debug.LogWarning($"Audio '{soundName}' no encontrado.");
            return;
        }
        AudioSourceController source = GetAvailableSource();
        source.PlayClip(clip, masterVolume, Random.Range(minPitch, maxPitch));
    }
    public void PlaySoundAtPosition(string soundName, Vector3 position)
    {
        AudioClip clip = FindAudio(soundName);
        if (clip == null)
        {
            Debug.LogWarning($"Audio '{soundName}' no encontrado.");
            return;
        }
        AudioSourceController source = GetAvailableSource();
        source.PlayClipAtPosition(clip, position, masterVolume, Random.Range(minPitch, maxPitch));
    }
    private AudioSourceController GetAvailableSource()
    {
        foreach (AudioSourceController src in _sources)
            if (!src.IsPlaying) return src;

        return CreateNewSource();
    }
    public AudioClip FindAudio(string soundName)
    {
        foreach (AudioClip clip in clips)
            if (clip.name == soundName)
                return clip;
        return null;
    }
}
