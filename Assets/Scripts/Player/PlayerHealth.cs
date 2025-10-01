using UnityEngine;
public class PlayerHealth
{
    private float _currentHealth;
    private Animator _animator;
    private ThirdPersonInputs _playerInputs;
    public PlayerHealth(float _currentHealth, Animator _animator, ThirdPersonInputs _playerInputs)
    {
        this._currentHealth = _currentHealth;
        this._animator = _animator;
        this._playerInputs = _playerInputs;
    }
    private void CheckHealth()
    {
        if (_currentHealth <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        Debug.Log("Player Health: " + _currentHealth);
        CheckHealth();
    }
    private void Die()
    {
        Debug.Log("Player Died");
        _animator.SetBool("OnDeath", true);
        _playerInputs.OnDeath();
    }
    public float GetLife { get => _currentHealth; }
}