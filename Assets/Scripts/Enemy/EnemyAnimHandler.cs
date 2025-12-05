using UnityEngine;
public class EnemyAnimHandler : MonoBehaviour
{
    private Enemy _enemy;
    private void Start()
    {
        _enemy = GetComponentInParent<Enemy>();
    }
    public void Attack()
    {
        AudioManager.Instance.PlaySFX("enemySlash", transform.position);
        _enemy.Attack();
    }
    public void OnWalk()
    {
        AudioManager.Instance.PlaySFX("enemyFootstep", transform.position);
    }
}
