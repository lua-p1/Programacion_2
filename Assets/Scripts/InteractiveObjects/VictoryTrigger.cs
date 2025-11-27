using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    [SerializeField] private VictoriaUI victoriaUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            victoriaUI.gameObject.SetActive(true);
            victoriaUI.IniciarFade();
        }
    }
}
