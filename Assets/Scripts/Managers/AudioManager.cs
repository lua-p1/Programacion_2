using UnityEngine;
using System.Collections.Generic;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioClip[] clips;
    public int initialSources = 10;
    private List<AudioSource> sources = new List<AudioSource>();
    private void Awake()
    {
        Instance = this;
        for (int i = 0; i < initialSources; i++)
        {
            CreateNewSource();
        }
    }
    private AudioSource CreateNewSource()
    {
        AudioSource src = gameObject.AddComponent<AudioSource>();
        sources.Add(src);
        return src;
    }
    public void PlaySound(string soundName)
    {
        AudioClip clip = FindAudio(soundName);
        if (clip == null)
        {
            Debug.LogWarning($"{soundName} no está en la lista");
            return;
        }
        AudioSource src = GetAvailableSource();
        src.PlayOneShot(clip);
    }
    private AudioSource GetAvailableSource()
    {
        foreach (AudioSource src in sources)
        {
            if (!src.isPlaying)
                return src;
        }
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
