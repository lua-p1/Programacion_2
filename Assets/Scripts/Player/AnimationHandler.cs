using UnityEngine;
public class AnimationEventHandler : MonoBehaviour
{
    public void CallDefeatScreen()
    {
        GameManager.instance.ShowDefeatScreen();
    }
}