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
        float batSound = Random.Range(0f, 5f);
        if (batSound < 3f)
        {
            AudioManager.Instance.PlaySoundAtPosition("batIdle", transform.position);
        }
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
