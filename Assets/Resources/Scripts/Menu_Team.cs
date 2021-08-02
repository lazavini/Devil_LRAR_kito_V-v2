using Assets.Vuforia.Scripts.Cards;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_Team : MonoBehaviour
{
    public Button ButtonBack;
    public Button ButtonQuit;

    private void Start()
    {
        ButtonBack.onClick.AddListener(Back);
        ButtonQuit.onClick.AddListener(Quit);

    }

    public void Back()
    {
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
