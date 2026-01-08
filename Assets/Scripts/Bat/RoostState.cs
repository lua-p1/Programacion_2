public class RoostState : IState
{
    private FSM _fsm;
    private BatEnemy _bat;
    public RoostState(BatEnemy bat, FSM fsm)
    {
        _bat = bat;
        _fsm = fsm;
    }
    public void OnEnter()
    {
        _bat.Animator.SetBool("IsFlying", false);
    }
    public void OnUpdate()
    {
        if (_bat.CanHearPlayer())
        {
            _fsm.ChangeState(FSM.State.Listen);
        }
    }
    public void OnExit() { }
}