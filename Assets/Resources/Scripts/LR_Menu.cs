using Assets.Vuforia.Scripts.Cards;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LR_Menu : MonoBehaviour
{
    public Button PlayButton;
    public Button ExitButton;
    public Component Menu;


    public Text TextCardPlayer;
    public Text TextCardEffect;
    public Text TextCardSkin;




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



        TextCardPlayer.gameObject.SetActive(false);
        TextCardEffect.gameObject.SetActive(false);
        TextCardSkin.gameObject.SetActive(false);
        if (TrackedCardsCollection.Cards.Count(x => x.CardType == CardType.Character) == 0)
            TextCardPlayer.gameObject.SetActive(true);
        else if(TrackedCardsCollection.Cards.Count(x => x.CardType == CardType.Skin) == 0)
            TextCardSkin.gameObject.SetActive(true);
        else if (TrackedCardsCollection.Cards.Count(x => x.CardType == CardType.Effect) == 0)
            TextCardEffect.gameObject.SetActive(true);
    }

    public void Play()
    {
        CardMixer.Player2Mixer.MixTrackedCards();
        CardMixer.Player1Mixer.MixTrackedCards();
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main");
    }
}
