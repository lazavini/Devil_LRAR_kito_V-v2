using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

namespace Assets.Vuforia.Scripts.Cards
{
    public class ScaleCard : ModificationCard
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        Slider SliderScale => CardComponent.gameObject.GetComponentsInChildren<Slider>().FirstOrDefault(x => x.name == "Slider");

        public ScaleCard(string name, 
            string sound, 
            string description,
            float x,
            float y,
            float z) : base(name,
            sound,
            description,
            null,
            CardType.Scale)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public override void CardTrackChanged(TrackableBehaviour.Status status)
        {
            base.CardTrackChanged(status);
        }

        public override void Mix(ICard card)
        {
            base.Mix(card);
            X = SliderScale?.value ?? 1;
            CardComponent.gameObject.GetComponentsInChildren<Text>().FirstOrDefault(x => x.name == "Text").text = $"Scale: {X}";
            card.CardComponent.transform.localScale = new Vector3(X, X, X);
        }
    }
}
