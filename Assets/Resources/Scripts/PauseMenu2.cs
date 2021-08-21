using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu2 : MonoBehaviour
{
    public static bool GamiIsPause = false;
    public GameObject pauseMenuUI;
    public Component PanelSave;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//trocar por um botão na tela
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
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GamiIsPause = true;
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    public void Load()
    {
        SceneManager.LoadScene("Load");
    }

    public void Save()
    {

        SceneManager.LoadScene("Load");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
    public void LoadLR_cards()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LR_cards");
    } 
}
