using UnityEngine;
using UnityEngine.AI;
public class SoundEnemyListener : MonoBehaviour
{
    private NavMeshAgent agent;
    private Enemy enemy;
    [SerializeField] private float attackRange = 1.5f;
    private Vector3 lastSoundPosition;
    private bool hasSoundTarget;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
    }
    public void OnHearSound(Vector3 soundPosition)
    {
        lastSoundPosition = soundPosition;
        hasSoundTarget = true;
        agent.SetDestination(soundPosition);
    }
    private void Update()
    {
        if (!hasSoundTarget) return;
        float dist = Vector3.Distance(transform.position, lastSoundPosition);
        if (dist <= attackRange)
        {
            agent.isStopped = true;
            enemy.Attack();
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(lastSoundPosition);
        }
    }
}
