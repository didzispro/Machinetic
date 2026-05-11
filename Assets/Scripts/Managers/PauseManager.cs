using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Space(5)]
    public GameObject pauseMenuCanvas;

    private PlayerController playerController;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        pauseMenuCanvas.SetActive(false);

        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        IsPaused();
    }

    private void IsPaused()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !playerController.isItGameOver)
        {
            if (isPaused)
            {
                ResumeButton();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuCanvas.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeButton()
    {
        pauseMenuCanvas.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
