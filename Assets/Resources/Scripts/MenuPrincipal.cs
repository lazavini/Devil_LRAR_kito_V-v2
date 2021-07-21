using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Start is called before the first frame update

    public Button ButtonPlay;
    public Button ButtonRandom;
    public Button ButtonRandomHero;
    public Button ButtonCreateHero;
    public Button ButtonAbout;
    public Button ButtonCredits;



    void Start()
    {
        ButtonPlay.onClick.AddListener(Play);
        ButtonRandom.onClick.AddListener(Random);
    }

    // Update is called once per frame
    void Update()
    {
        if (TrackedCardsCollection.FirstPlayer != null)
            Show();
        else
            Hide();
    }

    public void Play()
    {
        var secondPlayer = TrackedCardsCollection.SecondPlayer;
        foreach (var c in TrackedCardsCollection.Cards.Where(x => x.CardType != Assets.Vuforia.Scripts.Cards.CardType.Character))
        {
            secondPlayer.Mix(c);
        }
    }

    public void Random()
    {
        var random = new System.Random();
        var firstCard = TrackedCardsCollection.FirstPlayer;
        var effectsCards = TrackedCardsCollection.EffectsCards;
        var effectCard = effectsCards.ElementAt(random.Next(0, effectsCards.Count - 1));
        var skinCards = TrackedCardsCollection.SkinCards;
        var skinCard = skinCards.ElementAt(random.Next(0, effectsCards.Count - 1));
        firstCard.Mix(skinCard);
        firstCard.Mix(effectCard);
        firstCard.CardComponent.gameObject.SetActive(true);
    }

    private void Hide()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LR_cards");
    }
    private void Show()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);
        //Enable rendering:
        foreach (var component in rendererComponents)
        {
            component.enabled = true;
            Debug.Log("Component name " + component.name);
            if (component.GetComponent<Animation>())
            {
                component.GetComponent<Animation>().Play();
                // Debug.Log("Animation playing " + component.GetComponent<Animation>().isPlaying);
            }
        }
        //Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;
        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;


    }
}
