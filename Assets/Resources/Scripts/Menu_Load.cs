using Assets.Vuforia.Scripts.Cards;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class Menu_Load : MonoBehaviour
{
    public Image QrcodeImage;
    public Component QrcodePanel;
    QrcodeWriter _qrcodeWriter = new QrcodeWriter();
    public Dropdown DropdownHeros;



    void Start()
    {
        try
        {
            PopulateHerosDropdown();
        }
        catch (Exception) { }
    }

    private void PopulateHerosDropdown()
    {
        DropdownHeros.ClearOptions();
        DropdownHeros.AddOptions(CardMixer.GetSavesNames().Select(x => new Dropdown.OptionData(x)).ToList());
    }

    public void CloseQrcodePanel()
    {
        CardMixer.Intialize();
        QrcodePanel.gameObject.SetActive(false);
    }


    public void ViewHero()
    {
        var saveFile = CardMixer.GetSaves().ElementAt(DropdownHeros.value);
        CardMixer.LoadFromFile(saveFile);
        SceneManager.LoadScene("LR_cards");
    }


    public void LoadHeroQrcode()
    {
        var saveFile = CardMixer.GetSaves().ElementAt(DropdownHeros.value);
        CardMixer.LoadFromFile(saveFile);
        string json = CardMixer.GenerateJson();
        var texture = _qrcodeWriter.GenerateQrcode(json);
        
        QrcodePanel.gameObject.SetActive(true);
        QrcodeImage.type = UnityEngine.UI.Image.Type.Simple;
        QrcodeImage.preserveAspect = true;
        QrcodeImage.useSpriteMesh = true;
        QrcodeImage.sprite = Sprite.Create(texture, new Rect(0, 0, 200, 200), new Vector2(0, 0));
    }

    public void LoadQrcode()
    {
        SceneManager.LoadScene("LR_cards");
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        try
        {
            var qrcodeReader = GameObject.FindObjectOfType<QrcodeReader>();
            qrcodeReader.StartReading();
            SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
        }
        catch(Exception) { }
    }
}
