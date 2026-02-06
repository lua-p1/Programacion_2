using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBatHandler : MonoBehaviour
{
    private BatEnemy _bat;
    private void Start()
    {
        _bat = GetComponentInParent<BatEnemy>();
    }
    public void Idle()
    {
        AudioManager.Instance.PlaySoundAtPosition("batWings", transform.position);
    }
    public void Attack()
    {
        AudioManager.Instance.PlaySoundAtPosition("batAttack", transform.position);
        _bat.Attack();
    }
    public void OnFly()
    {
        AudioManager.Instance.PlaySoundAtPosition("batFlying", transform.position);
    }
}
