using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Vuforia.Scripts.Cards;

public class MenuPrincipal : MonoBehaviour
{
    public Button ButtonCreateHero;
    public Button ButtonRandom;
    public Button ButtonAbout;
    public Button ButtonHowtouse;
    public Button ButtonSurvey;

    void Start()
    {
        TrackedCardsCollection.InitializeDataBase();
        CardMixer.Intialize();
        ButtonRandom.onClick.AddListener(Random);
        ButtonCreateHero.onClick.AddListener(Create);
        ButtonHowtouse.onClick.AddListener(Howtouse);
        ButtonAbout.onClick.AddListener(About);
        ButtonSurvey.onClick.AddListener(Survey);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Random()
    {
        SceneManager.LoadScene("Menu_random");
    }

    public void Create()
    {
        SceneManager.LoadScene("LR_cards");
    }
    public void Howtouse()
    {
        SceneManager.LoadScene("How_to_use");
    }
    public void About()
    {
        SceneManager.LoadScene("Scene_Team");
    }
    public void Survey()
    {
        SceneManager.LoadScene("Survey");
    }
}
 