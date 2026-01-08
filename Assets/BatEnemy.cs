using UnityEngine;
public class BatEnemy : MonoBehaviour
{
    private Animator _animator;
    public float hearingRange;
    public float noiseThreshold;
    public bool CanHearPlayer()
    {
        float distance = Vector3.Distance(transform.position,GameManager.instance.player.transform.position);

        return distance <= hearingRange && GameManager.instance.playerNoiseLevel >= noiseThreshold;
    }
}
