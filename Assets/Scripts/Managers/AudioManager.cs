using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Clips Disponibles")]
    public AudioClip[] clips;

    [Header("Ajustes Globales")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    public float minPitch = 0.95f;
    public float maxPitch = 1.05f;

    [Header("Pool Settings")]
    public int initialSources = 10;

    private List<AudioSourceController> sources = new List<AudioSourceController>();

    private void Awake()
    {
        Instance = this;
        CreateInitialSources();
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
        sources.Add(controller);
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
        foreach (AudioSourceController src in sources)
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