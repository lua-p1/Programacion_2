using UnityEngine;
public class DiveAttackState : IState
{
    private BatEnemy _bat;
    private FSM _fsm;
    private float _attackDuration = 1.5f;
    private float _timer;
    private bool _isAlreadyAttack;
    public DiveAttackState(BatEnemy bat, FSM fsm)
    {
        _bat = bat;
        _fsm = fsm;
    }
    public void OnEnter()
    {
        Debug.Log("Enter Dive");
        _isAlreadyAttack = false;
        _timer = 0f;
        _bat.Animator.SetTrigger("Dive");
    }
    public void OnUpdate()
    {
        _bat.DiveTowardsTarget();
        if (!_isAlreadyAttack && _bat.HasReachedAttackPoint())
        {
            Debug.Log("Attack");
            _bat.Attack();
            _isAlreadyAttack = true;
            _timer = 0f;
        }
        if (_isAlreadyAttack)
        {
            _timer += Time.deltaTime;
            if (_timer >= _attackDuration)
            {
                _fsm.ChangeState(FSM.State.Retreat);
            }
        }
    }
    public void OnExit()
    {
        _bat.Animator.SetBool("IsFlying", false);
    }
}