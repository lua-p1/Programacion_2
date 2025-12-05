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
        //AudioManager.Instance.PlaySoundAtPosition("enemySlash", transform.position);
        _enemy.Attack();
    }
    public void OnWalk()
    {
        //AudioManager.Instance.PlaySoundAtPosition("enemyFootstep", transform.position);
    }
}
