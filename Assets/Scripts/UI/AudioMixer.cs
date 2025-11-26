using UnityEngine;

public class AudioSourceController : MonoBehaviour
{
    private AudioSource source;

    public bool IsPlaying => source.isPlaying;

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.playOnAwake = false;

        // Configuración por defecto del sonido 3D
        source.spatialBlend = 1f;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.minDistance = 1f;
        source.maxDistance = 20f;
    }

    public void PlayClip(AudioClip clip, float volume, float pitch)
    {
        transform.position = Camera.main.transform.position; // sonido asociado al jugador
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
}
