using UnityEngine;
using UnityEngine.SceneManagement;
public class VictoriaButtons : MonoBehaviour
{
    public void Reintentar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void VolverAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void SalirDelJuego()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("Juego cerrado (en editor no se cierra)");
    }
}

