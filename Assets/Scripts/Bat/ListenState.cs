using UnityEngine;
public class ListenState : IState
{
    private BatEnemy _bat;
    private FSM _fsm;
    private float _listenTime;
    private float _timer;
    public ListenState(BatEnemy bat, FSM fsm, float listenTime)
    {
        _bat = bat;
        _fsm = fsm;
        _listenTime = listenTime;
    }
    public void OnEnter()
    {
        Debug.Log("Enter Listen");
        _timer = 0f;
        _bat.Animator.SetBool("IsFlying", true);
    }
    public void OnUpdate()
    {
        _timer += Time.deltaTime;
        if (_bat.DetectBestNoiseTarget())
        {
            _fsm.ChangeState(FSM.State.DiveAttack);
            return;
        }
        if (_timer >= _listenTime)
        {
            _fsm.ChangeState(FSM.State.Roost);
        }
    }
    public void OnExit() { }
}