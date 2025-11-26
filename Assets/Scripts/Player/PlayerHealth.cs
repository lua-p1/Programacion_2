using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth
{
    private float _currentHealth;
    private Animator _animator;
    private ThirdPersonInputsw _playerInputs;
    private Slider _healthSlider;
    private ParticleSystem _damageParticles;
    public PlayerHealth(float _currentHealth, Animator _animator, ThirdPersonInputsw _playerInputs, Slider _healthSlider, ParticleSystem damageParticles)
    {
        this._currentHealth = _currentHealth;
        this._animator = _animator;
        this._playerInputs = _playerInputs;
        this._healthSlider = _healthSlider;
        _damageParticles = damageParticles;
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
            AudioManager.Instance.PlaySound("hurt_3");
        }
        if (_damageParticles != null)
        {
            _damageParticles.Play();
        }
        if(_animator != null)
        {
            _animator.SetTrigger("OnHurt");
        }
        CheckHealth();
        Debug.Log(_currentHealth);
    }
    private void Die()
    {
        if (_healthSlider != null)
        {
            _healthSlider.value = 0;
        }
        AudioManager.Instance.PlaySound("hurt_1");
        _animator.SetBool("OnDeath", true);
        _playerInputs.OnDeath();
    }
    public float GetLife { get => _currentHealth; }
}