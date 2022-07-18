using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPause = false;

    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
    }
    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void CampMenu()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 0f;
        QuestionUI.Instance.ShowQuestion("Are you sure want back to camp ?", () =>
        {
            Time.timeScale = 1f;
            Debug.Log("BACK TO CAMP LOADED");
        }, () => {
            Time.timeScale = 1f;
        });
    }
    public void QuitGame()
    {
        pauseMenuUI.SetActive(false);
        QuestionUI.Instance.ShowQuestion("Are you sure want to quit the game ?", () =>
        {
            Debug.Log("QUIT!");
            Application.Quit();
        }, () => {
            Time.timeScale = 1f;
        });
    }
}
