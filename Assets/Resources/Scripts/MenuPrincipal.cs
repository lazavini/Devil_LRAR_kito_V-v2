using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    // Start is called before the first frame update

    public Button ButtonPlay;
    void Start()
    {
        ButtonPlay.onClick.AddListener(Play);
    }

    // Update is called once per frame
    void Update()
    {
        if (TrackedCardsCollection.FirstCharacterCard != null)
            Show();
        else
            Hide();
    }

    public void Play()
    {
        var firstCard = TrackedCardsCollection.FirstCharacterCard;
        foreach (var c in TrackedCardsCollection.Cards)
        {
            if (c == firstCard) continue;

            firstCard.Mix(c);
        }
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
