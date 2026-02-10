using UnityEngine;
using UnityEngine.Audio;
public class AudioSourceController : MonoBehaviour
{
    private AudioSource source;
    public AudioMixerGroup outputMixerGroup;
    public bool IsPlaying => source.isPlaying;
    public AudioSource Source => source;
    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 1f;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.minDistance = 1f;
        source.maxDistance = 20f; 
        if (outputMixerGroup != null)
        {
            source.outputAudioMixerGroup = outputMixerGroup;
        }
    }
    public void PlayClip(AudioClip clip, float volume, float pitch)
    {
        transform.position = Camera.main.transform.position;
        ConfigureAndPlay(clip, volume, pitch);
    }
    public void PlayClipAtPosition(AudioClip clip, Vector3 position, float volume, float pitch)
    {
        transform.position = position;
        ConfigureAndPlay(clip, volume, pitch);
    }
    private void ConfigureAndPlay(AudioClip clip, float volume, float pitch)
    {
        source.pitch = pitch;
        source.volume = volume;
        source.PlayOneShot(clip);
    }
    public void PlayLoop(AudioClip clip, float volume, float pitch)
    {
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.loop = true;
        source.Play();
    }
    public void Stop()
    {
        source.Stop();
        source.clip = null;
        source.loop = false;
    }
}
