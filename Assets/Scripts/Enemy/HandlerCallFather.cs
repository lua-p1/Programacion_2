using UnityEngine;
public class HandlerCallFather : MonoBehaviour
{
    private Enemy _enemy;
    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    public void Attack()
    {
        _enemy.Attack();
    }
    public void OnWalk()
    {
        AudioManager.Instance.PlaySound("enemyFootstep");
    }
}
