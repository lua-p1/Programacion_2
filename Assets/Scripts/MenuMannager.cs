using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPrincipal : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void MostrarControles(GameObject panelControles)
    {
        panelControles.SetActive(true);
    }
    public void CerrarControles(GameObject panelControles)
    {
        panelControles.SetActive(false);
    }
    public void Salir()
    {
        Application.Quit();
        Debug.Log("El juego se cerraria (en editor no se cierra).");
    }
}