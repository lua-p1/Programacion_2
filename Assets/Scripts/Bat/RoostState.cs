using UnityEngine;
public class RoostState : IState
{
    private BatEnemy _bat;
    private FSM _fsm;
    public RoostState(BatEnemy bat, FSM fsm)
    {
        _bat = bat;
        _fsm = fsm;
    }
    public void OnEnter()
    {
        Debug.Log("Enter roost");
        _bat.Animator.SetBool("IsFlying", false);
    }
    public void OnUpdate()
    {
        if (_bat.DetectBestNoiseTarget())
        {
            _fsm.ChangeState(FSM.State.Listen);
        }
    }
    public void OnExit() { }
}