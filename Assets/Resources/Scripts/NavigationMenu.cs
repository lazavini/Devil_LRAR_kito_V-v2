using Assets.Resources.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigationMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public Button ButtonBack;
    public Button ButtonClose;
    public Button ButtonHome;


    void Start()
    {
        ButtonBack.onClick.AddListener(Back);
        ButtonClose.onClick.AddListener(Close);
        ButtonHome.onClick.AddListener(Home);
    }

    void Back()
    {
        var previous = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(NavigationHistory.PreviousScene);
        NavigationHistory.PreviousScene = previous;
    }
    void Close()
    {
        Application.Quit();
    }

    void Home()
    {
        NavigationHistory.PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Main");
    }
}
