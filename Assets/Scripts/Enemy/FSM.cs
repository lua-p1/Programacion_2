using System.Collections.Generic;
public class FSM
{
    public enum State
    {
        Patrol,
        Chase,
        Roost,
        Listen,
        DiveAttack,
        Retreat
    }
    private Dictionary<State, IState> _allStates = new Dictionary<State, IState>();
    private IState _currentState;
    public void AddState(State key,IState value) 
    {
        if(_allStates.ContainsKey(key)) return;
        _allStates.Add(key, value);
    }
    public void DeleteState(State key) 
    {
        if(!_allStates.ContainsKey(key)) return;
        _allStates.Remove(key);
    }
    public void ChangeState(State newState) 
    {
        if (!_allStates.ContainsKey(newState)) return;
        _currentState?.OnExit();
        _currentState = _allStates[newState];
        _currentState?.OnEnter();
    }
    public void OnUpdate() 
    {
        _currentState?.OnUpdate();
    }
}