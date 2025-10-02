using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuPrincipal : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ShowControls(GameObject panelControles)
    {
        panelControles.SetActive(true);
    }
    public void CloseControls(GameObject panelControles)
    {
        panelControles.SetActive(false);
    }
    public void QuitGame()
    {
        Debug.Log("El juego se cerro");
        Application.Quit();
    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}