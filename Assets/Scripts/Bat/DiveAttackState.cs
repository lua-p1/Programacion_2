using UnityEngine;
public class DiveAttackState : IState
{
    private BatEnemy _bat;
    private FSM _fsm;
    private float _attackDuration = 2f;
    private float _timer;
    public DiveAttackState(BatEnemy bat, FSM fsm)
    {
        _bat = bat;
        _fsm = fsm;
    }
    public void OnEnter()
    {
        _timer = 0;
        _bat.Animator.SetTrigger("Dive");
    }
    public void OnUpdate()
    {
        _timer += Time.deltaTime;
        _bat.DiveTowardsTarget();
        if (_bat.HasReachedAttackPoint())
        {
            _bat.Attack();
            _fsm.ChangeState(FSM.State.Retreat);
        }
        if (_timer > _attackDuration)
        {
            _fsm.ChangeState(FSM.State.Retreat);
        }
    }
    public void OnExit()
    {
        _bat.Animator.SetBool("IsFlying", false);
    }
}