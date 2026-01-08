public class RetreatState : IState
{
    private BatEnemy _bat;
    private FSM _fsm;

    public RetreatState(BatEnemy bat, FSM fsm)
    {
        _bat = bat;
        _fsm = fsm;
    }

    public void OnEnter()
    {
        _bat.SetRetreatPoint();
    }

    public void OnUpdate()
    {
        _bat.MoveToRetreat();

        if (_bat.AtRetreatPoint())
        {
            _fsm.ChangeState(FSM.State.Roost);
        }
    }

    public void OnExit() { }
}
