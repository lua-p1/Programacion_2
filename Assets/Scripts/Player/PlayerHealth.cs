using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth
{
    private float _currentHealth;
    private Animator _animator;
    private ThirdPersonInputs _playerInputs;
    private Slider _healthSlider;
    public PlayerHealth(float _currentHealth, Animator _animator, ThirdPersonInputs _playerInputs, Slider _healthSlider)
    {
        this._currentHealth = _currentHealth;
        this._animator = _animator;
        this._playerInputs = _playerInputs;
        this._healthSlider = _healthSlider;
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
        if(_currentHealth > 0)
        {
            _currentHealth -= damage;
        }
        if (_healthSlider != null)
        {
            _healthSlider.value = _currentHealth;
        }
        CheckHealth();
    }
    private void Die()
    {
        if (_healthSlider != null)
        {
            _healthSlider.value = 0;
        }
        _animator.SetBool("OnDeath", true);
        _playerInputs.OnDeath();
    }
    public float GetLife { get => _currentHealth; }
}