using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    [SerializeField]private float _visionRange;
    [SerializeField]private NavMeshAgent _navMeshAgent;
    private Vector3 directionToPlayer;
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        Follow();
    }
    private void Follow()
    {
        if (GameManager.instance.player == null) return;
        directionToPlayer = GameManager.instance.player.transform.position - this.transform.position;
        if (directionToPlayer.magnitude <= _visionRange)
        {
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            _navMeshAgent.SetDestination(GameManager.instance.player.transform.position);
        }
        else
        {
            _navMeshAgent.SetDestination(transform.position);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _visionRange);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, directionToPlayer.normalized * _visionRange);
    }
}
