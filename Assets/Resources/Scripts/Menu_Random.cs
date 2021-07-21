using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Random : MonoBehaviour
{
    public Button ButtonMakeRandom;
    public InputField Input_Hero;
    public InputField Input_Power;
    public InputField Input_Color;
    public InputField Input_Scale;

    void Start()
    {
        ButtonMakeRandom.onClick.AddListener(Random);
    }


    public void Random()
    {
        var random = new System.Random();
        var firstCard = TrackedCardsCollection.FirstPlayerDataBase;
        var effectsCards = TrackedCardsCollection.EffectsCards;
        var effectCard = effectsCards.ElementAt(random.Next(0, effectsCards.Count - 1));
        var skinCards = TrackedCardsCollection.SkinCards;
        var skinCard = skinCards.ElementAt(random.Next(0, effectsCards.Count - 1));
       
        
        firstCard.Mix(skinCard);
        firstCard.Mix(effectCard);

        firstCard.
    
       // firstCard.CardComponent.gameObject.SetActive(true);
       // SceneManager.LoadScene("LR_cards");
    }




}
