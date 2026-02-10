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
    public Transform CurrentNoiseTarget { get; private set; }
    private float _currentNoiseValue;
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
    public void DiveTowardsTarget()
    {
        if (CurrentNoiseTarget == null) return;
        _attackPoint = CurrentNoiseTarget.position;
        Vector3 direction = _attackPoint - transform.position;
        direction.y = 0f;
        if (direction.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 6f);
            transform.position = Vector3.MoveTowards(transform.position, _attackPoint, _diveSpeed * Time.deltaTime);
        }
    }
    public bool HasReachedAttackPoint()
    {
        return Vector3.Distance(transform.position, _attackPoint) <= _attackRange || transform.position.y <= GameManager.instance.player.transform.position.y;
    }
    public void Attack()
    {
        if (CurrentNoiseTarget == null) return;
        if (CurrentNoiseTarget.CompareTag("Player"))
        {
            var life = CurrentNoiseTarget.GetComponent<ThirdPersonInputs>().GetPlayerComponentLife;
            life.TakeDamage(_damage);
        }
        else
        {
            NoiseEmitter emitter = CurrentNoiseTarget.GetComponent<NoiseEmitter>();
            if (emitter != null)
                emitter.Consume();
        }
    }
    public bool DetectBestNoiseTarget()
    {
        CurrentNoiseTarget = null;
        _currentNoiseValue = 0f;
        var player = GameManager.instance.player;
        var playerInputs = player.GetComponent<ThirdPersonInputs>();
        float playerDistance = Vector3.Distance(transform.position, player.transform.position);
        if (playerDistance <= hearingRange && playerInputs.CurrentNoise >= noiseThreshold)
        {
            _currentNoiseValue = playerInputs.CurrentNoise;
            CurrentNoiseTarget = player.transform;
        }
        NoiseEmitter[] emitters = FindObjectsOfType<NoiseEmitter>();
        foreach (var emitter in emitters)
        {
            if (!emitter.IsActive) continue;
            float distance = Vector3.Distance(transform.position, emitter.transform.position);
            if (distance > hearingRange) continue;
            if (emitter.NoiseAmount > _currentNoiseValue)
            {
                _currentNoiseValue = emitter.NoiseAmount;
                CurrentNoiseTarget = emitter.transform;
            }
        }
        return CurrentNoiseTarget != null;
    }
}