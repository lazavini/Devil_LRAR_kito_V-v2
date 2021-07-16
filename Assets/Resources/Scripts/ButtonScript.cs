using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Button Button;
    void Start()
    {
        Button.onClick.AddListener(Play);
    }

    public void Play()
    {
        if (TrackedCardsCollection.Cards.Count(x => x.CardComponent != null) >= 2)
        {
            var firstCard = TrackedCardsCollection.FirstCharacterCard;

            foreach (var c in TrackedCardsCollection.Cards)
            {
                firstCard.Mix(c);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
