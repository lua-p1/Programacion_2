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
        if (!agent.enabled) return;

        if (!agent.isOnNavMesh)
        {
            TryPlaceOnNavMesh();
            return;
        }
        lastSoundPosition = soundPosition;
        hasSoundTarget = true;
        agent.isStopped = false;
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
    void TryPlaceOnNavMesh()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 2f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }
    }

}
