
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    public GameObject gameOverUI;
    public GameObject pauseGameUI;
    public GameObject EndLevelGameUI;

    public GameObject mobileButtonsUI;

    public FinishLine traguardo;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        //if (gameOverUI.activeInHierarchy || pauseGameUI.activeInHierarchy || EndLevelGameUI.activeInHierarchy)
        //{
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;

        //}
        //else
        //{
        //    Cursor.visible = false;
        //    Cursor.lockState = CursorLockMode.Locked;
        //}

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (pauseGameUI.activeInHierarchy)
            {
                PauseGame(false);
            }
            else
                PauseGame(true);
        }

    }
    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void PauseGame(bool status)
    {
        if (!traguardo.hasSuccessfullyEnter) { 
        pauseGameUI.SetActive(status);

        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
        }
    }
    public void FinishGame()
    {
        if (traguardo.hasSuccessfullyEnter)
        {
            Time.timeScale = 0;
            EndLevelGameUI.SetActive(true);
        }
    }

    public void ResumeGameMobileButton()
    {
        if (!traguardo.hasSuccessfullyEnter)
        {
            pauseGameUI.SetActive(false);
            mobileButtonsUI.SetActive(true);
            Time.timeScale = 1;
        }
    }

    public void PauseGameMobileButton()
    {
        if (!traguardo.hasSuccessfullyEnter)
        {
            pauseGameUI.SetActive(true);
            mobileButtonsUI.SetActive(false);

            Time.timeScale = 0;
        }
    }

}