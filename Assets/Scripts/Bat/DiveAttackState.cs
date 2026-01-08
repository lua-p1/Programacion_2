public class DiveAttackState : IState
{
    private BatEnemy _bat;
    private FSM _fsm;
    public DiveAttackState(BatEnemy bat, FSM fsm)
    {
        _bat = bat;
        _fsm = fsm;
    }
    public void OnEnter()
    {
        _bat.Animator.SetTrigger("Dive");
    }
    public void OnUpdate()
    {
        _bat.DiveTowardsPlayer();

        if (_bat.HasReachedAttackPoint())
        {
            _bat.Attack();
            _fsm.ChangeState(FSM.State.Retreat);
        }
    }
    public void OnExit() { }
}