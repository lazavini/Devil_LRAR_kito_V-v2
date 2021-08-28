using Assets.Vuforia.Scripts.Cards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GamiIsPause = false;
    public GameObject pauseMenuUI;
    public Component PanelSave;
    public Component PanelLoad;
    public Component PanelLoading;

    public UnityEngine.UI.Image QrcodeImage;
    QrcodeWriter _qrcodeWriter = new QrcodeWriter();
    public UnityEngine.UI.InputField InputSaveName;
    public UnityEngine.UI.InputField InputLoadName;
    public QrcodeReader QrcodeReader;


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
        QrcodeReader = gameObject.GetComponentInChildren<QrcodeReader>(true);
        PanelLoading.gameObject.SetActive(QrcodeReader.Reading);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        PanelLoad.gameObject.SetActive(false);
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

    //public void Load()
    //{
    //    SceneManager.LoadScene("Load");
    //}

    public void Save()
    {
        PanelSave.gameObject.SetActive(true);
        //SceneManager.LoadScene("Load");
    }

    public void SaveHero()
    {
        CardMixer.Save(InputSaveName.text);
        PanelSave.gameObject.SetActive(false);
        Resume();
    }

    public void ShowHeroQrcode()
    {
        string json = CardMixer.GenerateJson();
        var texture =_qrcodeWriter.GenerateQrcode(json);
        QrcodeImage.type = UnityEngine.UI.Image.Type.Simple;
        QrcodeImage.preserveAspect = true;
        QrcodeImage.useSpriteMesh = true;
        QrcodeImage.sprite = Sprite.Create(texture, new Rect(0,0,200,200), new Vector2(0,0));
    }


    public void Load()
    {
        PanelLoad.gameObject.SetActive(true);
    }

    public void LoadHero()
    {
        CardMixer.LoadFromName(InputLoadName.text);
        PanelLoad.gameObject.SetActive(false);
        Resume();
    }

    public void StartReadingQrcode()
    {
        PanelLoad.gameObject.SetActive(false);
        QrcodeReader.StartReading();
        Resume();
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
