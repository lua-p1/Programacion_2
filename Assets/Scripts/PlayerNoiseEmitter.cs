using UnityEngine;
public class PlayerNoiseEmitter : MonoBehaviour
{
    public static PlayerNoiseEmitter Instance;
    [SerializeField] private float walkNoiseRadius = 3f;
    [SerializeField] private float noiseInterval = 0.4f;
    private float timer;
    private void Awake()
    {
        Instance = this;
    }
    public void EmitWalkNoise()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, walkNoiseRadius);
        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out SoundEnemyListener listener))
            {
                listener.OnHearSound(transform.position);
            }
        }
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    public bool CanEmitNoise()
    {
        if (timer >= noiseInterval)
        {
            timer = 0;
            return true;
        }
        return false;
    }
}

