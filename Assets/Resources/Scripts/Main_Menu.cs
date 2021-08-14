using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Vuforia.Scripts.Cards;
using Assets.Resources.Scripts;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using Vuforia;

public class Main_Menu : MonoBehaviour
{
    public Button ButtonCreateHero;
    public Button ButtonRandom;
    public Button ButtonAbout;
    public Button ButtonHowtouse;
    public Button ButtonSurvey;
    public Button ButtonLoad;


    void Start()
    {
        TrackedCardsCollection.InitializeDataBase();
        CardMixer.Intialize();
        ButtonRandom.onClick.AddListener(Random);
        ButtonCreateHero.onClick.AddListener(Create);
        ButtonHowtouse.onClick.AddListener(Howtouse);
        ButtonAbout.onClick.AddListener(About);
        ButtonSurvey.onClick.AddListener(Survey);
        ButtonLoad.onClick.AddListener(Load);
        NavigationHistory.ActualScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Random()
    {
        NavigationHistory.PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Menu_random");
    }

    public void Create()
    {
        NavigationHistory.PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("LR_cards");
    }
    public void Howtouse()
    {
        NavigationHistory.PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("How_to_use");
    }
    public void About()
    {
        NavigationHistory.PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Scene_Team");
    }
    public void Load()
    {
        NavigationHistory.PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Load");
    }

    public void Survey()
    {
        NavigationHistory.PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Survey");
    }
}
 