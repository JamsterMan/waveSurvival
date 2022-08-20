using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    private bool _isPaused = false;
    public GameObject shopMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
                UnpauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        _isPaused = true;
        pauseMenu.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        _isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void MainManu()
    {
        UnpauseGame();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit.");
        Application.Quit();
    }

    public bool IsGamePaused()
    {
        return _isPaused;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void SettingsMenu()
    {
        Debug.Log("Not Implemented");
    }

    public void ShopScreen()
    {
        shopMenu.SetActive(true);
    }

    public void ReturnToMenuScreen()
    {
        shopMenu.SetActive(false);
    }
}
