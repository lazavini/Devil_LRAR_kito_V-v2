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
    public class ColorCard : ModificationCard
    {
        FlexibleColorPicker ColorPicker => Status == TrackableBehaviour.Status.TRACKED ? CardComponent.gameObject.GetComponentInChildren<FlexibleColorPicker>(true) : null;
        public string SelectedColor { get; set; }
        public ColorCard(string name,
            string sound, 
            string description,
            string animation) : base(name,
            sound,
            description,
            animation,
            CardType.Color)
        {
        }
        public override void CardTrackChanged(TrackableBehaviour.Status status)
        {
            base.CardTrackChanged(status);
        }

        public override void Mix(ICard card)
        {
            Color color;
            if(SelectedColor != null)
            {
                ColorUtility.TryParseHtmlString(SelectedColor, out color);
            }
            else
                color = ColorPicker?.color ??
                    new Color(UnityEngine.Random.Range(0, 255),
                    UnityEngine.Random.Range(0, 255),
                    UnityEngine.Random.Range(0, 255),
                    UnityEngine.Random.Range(0,1));

            if (CardComponent == null)
            {
                SelectedColor = ColorUtility.ToHtmlStringRGBA(color);
                return;
            }
            else
                SelectedColor = null;

            foreach (var render in card.CardComponent.GetComponentsInChildren<Renderer>())
            {
                render.material.SetColor("_Color", color);
            }
        }
    }
}
