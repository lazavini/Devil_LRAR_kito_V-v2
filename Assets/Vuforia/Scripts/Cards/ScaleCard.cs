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
        public float? X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        Slider SliderScale => Status == TrackableBehaviour.Status.TRACKED ? CardPanel.gameObject.GetComponentsInChildren<Slider>().FirstOrDefault(x => x.name == "Slider") : null;

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
            var scale = (float)0.0;
            var random = new System.Random();

            if (X.HasValue)
            {
                scale = X.Value;
            }
            else
                scale = SliderScale?.value  ?? (new System.Random()).Next(1,4);

            if (CardComponent == null)
            {
                X = scale;
                return;
            }
            else
                X = null;

            CardPanel.gameObject.GetComponentsInChildren<Text>().FirstOrDefault(x => x.name == "Text").text = $"Scale: {scale}";
            var skin = card.CardComponent.GetComponentsInChildren<Component>(true)
                .FirstOrDefault(x => x.tag == "skin");
            if(skin != null)
                skin.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
