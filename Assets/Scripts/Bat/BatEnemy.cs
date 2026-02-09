using UnityEngine;
public class BatEnemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public Animator Animator => _animator;
    public float hearingRange;
    public float noiseThreshold;
    [Header("Retreat")]
    [SerializeField] private float _retreatHeight = 5f;
    [SerializeField] private float _retreatSpeed = 6f;
    private Vector3 _retreatPoint;
    [Header("Dive Attack")]
    [SerializeField] private float _diveSpeed = 10f;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private float _damage = 10f;
    private Vector3 _attackPoint;
    private FSM _fsm;
    private void Awake()
    {
        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        _fsm.OnUpdate();
    }
    private void Start()
    {
        _fsm = new FSM();
        _fsm.AddState(FSM.State.Roost, new RoostState(this, _fsm));
        _fsm.AddState(FSM.State.Listen, new ListenState(this, _fsm, 1.2f));
        _fsm.AddState(FSM.State.DiveAttack, new DiveAttackState(this, _fsm));
        _fsm.AddState(FSM.State.Retreat, new RetreatState(this, _fsm));
        _fsm.ChangeState(FSM.State.Roost);
    }
    public bool CanHearPlayer()
    {
        float distance = Vector3.Distance(transform.position,GameManager.instance.player.transform.position);
        ThirdPersonInputs player = GameManager.instance.player.GetComponent<ThirdPersonInputs>();
        return distance <= hearingRange && player.CurrentNoise >= noiseThreshold;
    }
    public void SetRetreatPoint()
    {
    _retreatPoint = transform.position + Vector3.up * _retreatHeight;
    }
    public void MoveToRetreat()
    {
        transform.position = Vector3.MoveTowards(transform.position,_retreatPoint,_retreatSpeed * Time.deltaTime);
    }
    public bool AtRetreatPoint()
    {
        return Vector3.Distance(transform.position, _retreatPoint) < 0.1f;
    }
    public void DiveTowardsPlayer()
    {
        if (GameManager.instance.player == null) return;
        Transform player = GameManager.instance.player.transform;
        _attackPoint = player.position;
        Vector3 direction = _attackPoint - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation,Time.deltaTime * 6f);
            transform.position = Vector3.MoveTowards(transform.position,_attackPoint,_diveSpeed * Time.deltaTime);
        }
    }
    public bool HasReachedAttackPoint()
    {
        return Vector3.Distance(transform.position, _attackPoint) <= _attackRange || transform.position.y <= GameManager.instance.player.transform.position.y;
    }
    public void Attack()
    {
        if (GameManager.instance.player == null) return;
        float distance = Vector3.Distance(transform.position,GameManager.instance.player.transform.position);
        if (distance <= _attackRange)
        {
            //Debug.Log($"la distancia es de " + distance);
            var life = GameManager.instance.player.GetComponent<ThirdPersonInputs>().GetPlayerComponentLife;
            life.TakeDamage(_damage);
        }

    }
}