
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool pauseMenuActive;

    private void Start()
    {
        pauseMenu.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseMenuActive == false)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }  
        }
    }

    void PauseGame()
    {
        pauseMenu.gameObject.SetActive(true);
        pauseMenuActive = true;
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        pauseMenu.gameObject.SetActive(false);
        pauseMenuActive = false;
        Time.timeScale = 1;
    }

    public void ResumeButtonClick()
    {
        ResumeGame();
    }

    public void BackToMenuButtonClick()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void RestartLevelButtonClick()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);

        ResumeGame();
    }

    public void ExitButtonClick()
    {
        Application.Quit();
    }
}
