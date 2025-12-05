using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pausePanel;       
    public GameObject volumePanel;   
    private bool isPaused = false;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }
    public void PauseGame()
    {
        pausePanel.SetActive(true);
        volumePanel.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        volumePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void OpenOptions()
    {
        pausePanel.SetActive(false);
        volumePanel.SetActive(true);
    }
    public void CloseOptions()
    {
        volumePanel.SetActive(false);
        pausePanel.SetActive(true);
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }
}

