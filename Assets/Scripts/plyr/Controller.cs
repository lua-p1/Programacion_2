using UnityEngine;
public class Controller
{
    private Movement _movement;
    private Vector2 _movInputs;
    public Controller(Movement _movement)
    {
        this._movement = _movement;
    }
    public void OnUpdate()
    {
        GetMovementInputs();
    }
    public void OnFixedUpdate()
    {
        _movement.Move(_movInputs);
    }
    private void GetMovementInputs()
    {
        _movInputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}