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
        _timer = 0f;
    }

    public void OnUpdate()
    {
        _timer += Time.deltaTime;

        if (_timer >= _listenTime)
        {
            _fsm.ChangeState(FSM.State.DiveAttack);
        }
    }

    public void OnExit() { }
}
