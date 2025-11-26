using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource speaker;
    public static AudioManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void PlaySound(string soundName)
    {
        AudioClip clip = FindAudio(soundName);
        if (clip != null)
        {
            speaker.PlayOneShot(clip);
            Debug.Log(clip.name);
        }
        else
        {
            Debug.LogWarning($"{soundName} no esta en lista");
        }
    }
    public AudioClip FindAudio(string soundName)
    {
        for (int i = 0; i < clips.Length; i++)
        {
            if (clips[i].name==soundName)
            {
                return clips[i];
            }
        }
        return null;
    }
}
