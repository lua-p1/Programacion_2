using UnityEngine;

[System.Serializable]
public class SoundData
{
    public string name;
    public AudioClip clip;

    [Header("Pitch Range")]
    public float minPitch = 0.95f;
    public float maxPitch = 1.05f;

    [Header("Spatial")]
    public bool spatialize = true;
    [Range(0f, 1f)] public float spatialBlend = 1f;
}
