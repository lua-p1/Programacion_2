using UnityEngine;
public class BatEnemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public Animator Animator => _animator;
    [Header("Hearing")]
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
    [SerializeField] private Transform _attackTarget;
    private FSM _fsm;
    private float _currentNoiseValue;
    private float _baseHeight;
    private void Awake()
    {
        if (_animator == null)
            _animator = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        _baseHeight = transform.position.y;
        _fsm = new FSM();
        _fsm.AddState(FSM.State.Roost, new RoostState(this, _fsm));
        _fsm.AddState(FSM.State.Listen, new ListenState(this, _fsm, 1.2f));
        _fsm.AddState(FSM.State.DiveAttack, new DiveAttackState(this, _fsm));
        _fsm.AddState(FSM.State.Retreat, new RetreatState(this, _fsm));
        _fsm.ChangeState(FSM.State.Roost);
    }
    private void Update()
    {
        _fsm.OnUpdate();
    }
    public bool DetectBestNoiseTarget()
    {
        _attackTarget = null;
        _currentNoiseValue = 0f;
        var player = GameManager.instance.player;
        var inputs = player.GetComponent<ThirdPersonInputs>();
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (playerDistance <= hearingRange && inputs.CurrentNoise >= noiseThreshold)
        {
            _currentNoiseValue = inputs.CurrentNoise;
            _attackTarget = inputs.AttackPoint;
        }
        foreach (var emitter in FindObjectsOfType<NoiseEmitter>())
        {
            if (!emitter.IsActive) continue;
            float dist = Vector3.Distance(transform.position, emitter.transform.position);
            if (dist > hearingRange) continue;

            if (emitter.NoiseAmount > _currentNoiseValue)
            {
                _currentNoiseValue = emitter.NoiseAmount;
                _attackTarget = emitter.NoisePoint;
            }
        }
        return _attackTarget != null;
    }
    public void DiveTowardsTarget()
    {
        if (_attackTarget == null) return;
        Vector3 targetPos = _attackTarget.position;
        Vector3 direction = targetPos - transform.position;
        if (direction.sqrMagnitude < 0.01f) return;
        Quaternion rot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 6f);
        transform.position = Vector3.MoveTowards(transform.position,targetPos,_diveSpeed * Time.deltaTime);
    }
    public bool HasReachedAttackPoint()
    {
        if (_attackTarget == null) return false;

        return Vector3.Distance(transform.position, _attackTarget.position) <= _attackRange;//
    }
    public void Attack()
    {
        if (_attackTarget == null) return;

        if (_attackTarget.root.TryGetComponent<IAttackable>(out var attackable))
        {
            attackable.OnAttacked(_damage);
        }
    }
    public void SetRetreatPoint()
    {
        _retreatPoint = new Vector3(transform.position.x,_baseHeight + _retreatHeight,transform.position.z);
    }
    public void MoveToRetreat()
    {
        transform.position = Vector3.MoveTowards(transform.position,_retreatPoint,_retreatSpeed * Time.deltaTime);
    }
    public bool AtRetreatPoint()
    {
        return Vector3.Distance(transform.position, _retreatPoint) < 0.1f;
    }
    private void OnDrawGizmos()
    {
        if (_attackTarget == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_attackTarget.position, 0.15f);
        Gizmos.DrawLine(transform.position, _attackTarget.position);
    }
}