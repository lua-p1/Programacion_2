using UnityEngine;
public class Movement
{
    private float _speed;
    private Rigidbody _rb;
    public Movement(float _speed, Rigidbody _rb)
    {
        this._speed = _speed;
        this._rb = _rb;
    }
    public void Move(Vector2 inputs)
    {
        var _dir = new Vector3(inputs.x, 0, inputs.y);
        if (_dir != Vector3.zero)
        {
            Quaternion rot = Quaternion.LookRotation(_dir, Vector3.up);
            _rb.MoveRotation(Quaternion.Slerp(_rb.rotation, rot, 4.25f * Time.fixedDeltaTime));
        }
        Vector3 _newPos = (_rb.position + _dir.normalized * _speed * Time.fixedDeltaTime);
        _rb.MovePosition(_newPos);
    }
}