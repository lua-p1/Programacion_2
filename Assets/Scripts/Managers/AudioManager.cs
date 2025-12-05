using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Mixer References")]
    public AudioMixer audioMixer;
    public AudioMixerGroup musicGroup;
    public AudioMixerGroup sfxGroup;

    [Header("Music")]
    public AudioSource musicSource;

    [Header("SFX Settings")]
    public List<SoundData> sounds = new List<SoundData>();
    public int poolSize = 10;
    private AudioSource[] sfxPool;
    private int poolIndex = 0;

    [Header("Volumes (0–1 UI)")]
    public float masterVolume = 1f;
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);

        LoadVolumes();
        ApplyMixerVolumes();
        CreatePool();
    }
    private void Start()
    {
        // Reproducir música automáticamente si hay clip
        if (musicSource.clip != null && !musicSource.isPlaying)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // ----------------------------------------------------
    // POOLING
    // ----------------------------------------------------
    private void CreatePool()
    {
        sfxPool = new AudioSource[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = new GameObject("SFX_Channel_" + i);
            obj.transform.parent = this.transform;

            AudioSource src = obj.AddComponent<AudioSource>();
            src.playOnAwake = false;
            src.loop = false;
            src.spatialBlend = 1f;

            //  Asignar AudioMixerGroup de SFX
            src.outputAudioMixerGroup = sfxGroup;

            sfxPool[i] = src;
        }

        // música también al grupo correspondiente
        musicSource.outputAudioMixerGroup = musicGroup;
    }

    // ----------------------------------------------------
    // VOLUMEN (UI → Mixer)
    // ----------------------------------------------------
    public void SetMasterVolume(float v)
    {
        masterVolume = v;
        SaveVolumes();
        ApplyMixerVolumes();
    }

    public void SetMusicVolume(float v)
    {
        musicVolume = v;
        SaveVolumes();
        ApplyMixerVolumes();
    }

    public void SetSFXVolume(float v)
    {
        sfxVolume = v;
        SaveVolumes();
        ApplyMixerVolumes();
    }

    private void ApplyMixerVolumes()
    {
        audioMixer.SetFloat("MasterVolume", LinearToDB(masterVolume));
        audioMixer.SetFloat("MusicVolume", LinearToDB(musicVolume));
        audioMixer.SetFloat("SFXVolume", LinearToDB(sfxVolume));
    }

    // Conversión lineal (0–1) → decibeles para AudioMixer
    private float LinearToDB(float value)
    {
        return value <= 0.0001f ? -80f : Mathf.Log10(value) * 20f;
    }

    // ----------------------------------------------------
    // REPRODUCCIÓN SFX
    // ----------------------------------------------------
    public void PlaySFX(string soundName, Vector3 position)
    {
        SoundData s = sounds.Find(x => x.name == soundName);
        if (s == null)
        {
            Debug.LogWarning("No existe el sonido: " + soundName);
            return;
        }

        AudioSource src = GetPooledSource();

        src.transform.position = position;
        src.clip = s.clip;
        src.pitch = Random.Range(s.minPitch, s.maxPitch);
        src.spatialBlend = s.spatialize ? s.spatialBlend : 0f;

        src.Stop();
        src.Play();
    }

    private AudioSource GetPooledSource()
    {
        for (int i = 0; i < poolSize; i++)
        {
            int idx = (poolIndex + i) % poolSize;

            if (!sfxPool[idx].isPlaying)
            {
                poolIndex = (idx + 1) % poolSize;
                return sfxPool[idx];
            }
        }

        AudioSource oldest = sfxPool[poolIndex];
        poolIndex = (poolIndex + 1) % poolSize;
        return oldest;
    }

    // ----------------------------------------------------
    // PERSISTENCIA
    // ----------------------------------------------------
    private void SaveVolumes()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    private void LoadVolumes()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
    }
}