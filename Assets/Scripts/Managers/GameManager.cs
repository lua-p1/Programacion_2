using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        player = GameObject.FindAnyObjectByType<ThridPersonInputs>().gameObject;
        if(player == null)
        {
            Debug.LogError("No se encontro el jugador");
        }
        else
        {
            Debug.Log("Jugador encontrado");
        }
    }
}
