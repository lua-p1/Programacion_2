using UnityEngine;
public class PlayerHealth
{
    private float _currentHealth;
    public PlayerHealth(float _currentHealth)
    {
        this._currentHealth = _currentHealth;
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
    }
    public float GetLife { get => _currentHealth; }
}
