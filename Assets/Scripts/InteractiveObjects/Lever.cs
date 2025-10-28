using UnityEngine;
public class Lever : MonoBehaviour
{
    public Animator leverAnimator;
    public Animator doorAnimator;
    private bool isPlayerNear = false;
    private bool isDoorOpen = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            ToggleDoor();
        }
    }
    private void ToggleDoor()
    {
        if (isDoorOpen)
        {
            leverAnimator.SetTrigger("Off");
            doorAnimator.SetTrigger("Close");
            isDoorOpen = false;
        }
        else
        {
            leverAnimator.SetTrigger("On");
            doorAnimator.SetTrigger("Open");
            isDoorOpen = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}

