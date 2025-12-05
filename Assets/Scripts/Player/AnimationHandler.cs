using UnityEngine;
public class AnimationEventHandler : MonoBehaviour
{
    public void CallDefeatScreen()
    {
        GameManager.instance.ShowDefeatScreen();
    }
    public void FootstepEvent()
    {
      AudioManager.Instance.PlaySFX("footstepFast", transform.position);
    }
    public void OnHurtEvent()
    {
        int n = Random.Range(1, 4);
      AudioManager.Instance.PlaySFX($"hurt_{n}", transform.position);
    }
    public void OnDeathEvent()
    {
      AudioManager.Instance.PlaySFX("hurt_1", transform.position);
    }
}