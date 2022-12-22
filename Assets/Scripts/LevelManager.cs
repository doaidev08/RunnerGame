using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameMain");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene("GameMain");
    }
    public void PauseGame()
    {
        if(!PlayerManager.isGamePaused && !PlayerManager.gameOver)
        {
            Time.timeScale = 0;
            PlayerManager.isGamePaused = true;
        }

    }
    public void ResumeGame()
    {
        if (PlayerManager.isGamePaused)
        {
            Time.timeScale = 1;
            PlayerManager.isGamePaused = false;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
