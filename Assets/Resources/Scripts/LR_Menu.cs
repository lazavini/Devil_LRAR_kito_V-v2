using Assets.Resources.Scripts;
using Assets.Vuforia.Scripts.Cards;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vuforia;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

public class LR_Menu : MonoBehaviour
{
    public Button PlayButton;
    public Button ExitButton;
    public Component Menu;
    public UnityEngine.UI.Image ImageProgressBar;



    // Start is called before the first frame update
    void Start()
    {
        PlayButton.onClick.AddListener(Play);
        ExitButton.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        if ((TrackedCardsCollection.SecondPlayer != null || TrackedCardsCollection.FirstPlayer != null) && TrackedCardsCollection.Cards.Count(x => x.CardType != CardType.Character) > 0)
            Menu.gameObject.SetActive(true);
        else
            Menu.gameObject.SetActive(false);

        ImageProgressBar.fillAmount = CardMixer.UpdatePercentages();
    }

    public void Play()
    {
        if (!CardMixer.Player2Mixer.PlayerState.RandomGenerated)
            CardMixer.Player2Mixer.MixTrackedCards();
        if(!CardMixer.Player1Mixer.PlayerState.RandomGenerated)
            CardMixer.Player1Mixer.MixTrackedCards();
    }

    public void Exit()
    {
        NavigationHistory.PreviousScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("Main");
    }
}
