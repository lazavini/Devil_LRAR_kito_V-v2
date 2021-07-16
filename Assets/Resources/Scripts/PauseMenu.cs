using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamiIsPause = false;
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamiIsPause)
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
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GamiIsPause = false;
            
        }
        void Pause()
        {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamiIsPause = true;
        }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main_menu");
    }
    public void QuitGame()
    {

        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
