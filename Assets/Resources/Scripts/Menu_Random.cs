using Assets.Vuforia.Scripts.Cards;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Random : MonoBehaviour
{
    public Button ButtonMakeRandom;
    public Button ButtonBack;
    public Button ButtonPlay;


    public InputField Input_Hero;
    public InputField Input_Power;
    public InputField Input_Color;
    public InputField Input_Scale;

    public Component Tela_random;
    public Component Panel_char;


    void Start()
    {
        ButtonMakeRandom.onClick.AddListener(Random);
        ButtonBack.onClick.AddListener(Back);
        ButtonPlay.onClick.AddListener(Play);
    }


    public void Random()
    {
        CardMixer.Player1Mixer.RandomCards();
        Input_Hero.text = CardMixer.Player1Mixer.SeletedSkin;
        Input_Power.text = CardMixer.Player1Mixer.SelectedEffect;
        Input_Color.text = CardMixer.Player1Mixer.SelectedColor;
        Input_Scale.text = CardMixer.Player1Mixer.Scale;
        Panel_char.gameObject.SetActive(true);
        Tela_random.gameObject.SetActive(false);
    }


    public void Back()
    {
        Panel_char.gameObject.SetActive(false);
        Tela_random.gameObject.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene("LR_cards");
    }
}
