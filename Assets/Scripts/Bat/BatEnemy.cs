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
    [SerializeField] private float _attackRange = 1.2f;
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
        // Punto arriba del murciélago (techo / oscuridad)
       // _retreatPoint = transform.position + Vector3.up * _retreatHeight;
        _retreatPoint = transform.position + Vector3.up * _retreatHeight + transform.forward * Random.Range(-2f, 2f);
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
        _attackPoint = GameManager.instance.player.transform.position;
        _attackPoint.y = 0.40f;
        Debug.Log(_attackPoint);
        transform.position = Vector3.MoveTowards(transform.position,_attackPoint,_diveSpeed * Time.deltaTime);
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
            var life = GameManager.instance.player.GetComponent<ThirdPersonInputs>().GetPlayerComponentLife;
            life.TakeDamage(_damage);
        }

    }
}