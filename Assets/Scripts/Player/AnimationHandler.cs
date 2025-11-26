using UnityEngine;
public class AnimationEventHandler : MonoBehaviour
{
    public void CallDefeatScreen()
    {
        GameManager.instance.ShowDefeatScreen();
    }
    public void FootstepEvent()
    {
        AudioManager.Instance.PlaySound("footstepFast");
    }
    public void OnHurtEvent()
    {
        int n = Random.Range(1, 4);
        AudioManager.Instance.PlaySound($"hurt_{n}");
    }
    public void OnDeathEvent()
    {
        AudioManager.Instance.PlaySound("hurt_1");
    }
}