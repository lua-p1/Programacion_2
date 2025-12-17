using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Enemy))]
public class SoundHunter : MonoBehaviour
{
    [Header("Detección y Ataque")]
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float stoppingDistance = 0.2f;

    private NavMeshAgent agent;
    private Enemy _enemy;
    private Vector3 lastSoundPosition;
    private bool hasSoundTarget = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        _enemy = GetComponent<Enemy>();

        if (!_enemy)
            Debug.LogError("SoundHunter necesita un componente Enemy para poder atacar.");

        agent.stoppingDistance = stoppingDistance;
    }

    void Update()
    {
        if (hasSoundTarget)
        {
            // Moverse hacia el sonido
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                hasSoundTarget = false;
                agent.isStopped = true;
            }
        }

        CheckAttack();
    }

    /// <summary>
    /// Llamar desde PlayerNoiseEmitter cuando se genera ruido
    /// </summary>
    /// <param name="soundPosition"></param>
    public void OnHearSound(Vector3 soundPosition)
    {
        if (!agent.enabled)
            return;

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
    private void TryPlaceOnNavMesh()
    {
        NavMeshHit hit;
        Vector3 basePosition = transform.position;
        basePosition.y = 0.1f; // cerca de la base, ajusta según el modelo
        if (NavMesh.SamplePosition(transform.position, out hit, 5f, NavMesh.AllAreas))
        {
            float dist = Vector3.Distance(transform.position, hit.position);
            Debug.Log($"SamplePosition encontró punto a distancia: {dist}");
            if (dist <= 1.0f) // ajustar este límite según lo que permite Warp
            {
                agent.Warp(hit.position);
            }
            else
            {
                Debug.LogWarning($"Punto de NavMesh muy lejano para Warp: {dist}");
            }
        }
        else
        {
            Debug.LogWarning("SoundHunter no se pudo colocar en el NavMesh.");
        }
    }

    private void CheckAttack()
    {
        if (GameManager.instance.player == null || _enemy == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, GameManager.instance.player.transform.position);
        if (distanceToPlayer <= attackRange)
        {
            agent.isStopped = true;
            _enemy.Attack();
        }
    }
    private void OnDrawGizmos()
    {
        if (hasSoundTarget)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, lastSoundPosition);
        }
    }
}
