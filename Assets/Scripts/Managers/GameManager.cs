using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;
    [SerializeField]private GameObject _defeatCanvas;
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
        player = GameObject.FindAnyObjectByType<ThirdPersonInputsw>().gameObject;
        if (player == null)
        {
            Debug.LogError("No se encontro el jugador");
        }
        else
        {
            Debug.Log("Jugador encontrado");
        }
        _defeatCanvas.SetActive(false);
    }
    public void ShowDefeatScreen()
    {
        Debug.Log("Mostrando pantalla de derrota...");
        _defeatCanvas.SetActive(true);
    }
}