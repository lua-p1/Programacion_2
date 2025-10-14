using UnityEngine;
public class VictoryTrigger : MonoBehaviour
{
    [SerializeField] private GameObject victoryCanvas;

    private void Start()
    {
        victoryCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            victoryCanvas.SetActive(true);
        }
    }
}