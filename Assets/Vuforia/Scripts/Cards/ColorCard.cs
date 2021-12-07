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
        private ICollection<string> Colors;
        Dropdown ColorPicker => Status == TrackableBehaviour.Status.TRACKED ? CardPanel.gameObject.GetComponentInChildren<Dropdown>(true) : null;
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
            Colors = new List<string>
            {
                "Branca", //#F8F8FF
                "Azul", //#0000FF
                "Rosa", //#FF1493
                "Vermelha",//#FF0000
                "Verde" , //#008000
                "Preta", //#000000
                "Amarela" , //#FFFF00
                "Roxa",//#8A2BE2
                "Laranja" //#FF4500
            };
        }
        public override void CardTrackChanged(TrackableBehaviour.Status status)
        {
            base.CardTrackChanged(status);
        }

        public override void Mix(ICard card)
        {
            Color color;
            if (SelectedColor != null)
                ColorUtility.TryParseHtmlString(SelectedColor, out color);
            else
                ColorUtility.TryParseHtmlString(ConvertColor(Colors.ElementAt(ColorPicker?.value ?? (new System.Random()).Next(0, Colors.Count - 1))), out color);

            if (CardComponent == null)
            {
                SelectedColor = ColorUtility.ToHtmlStringRGBA(color);
                return;
            }
            else
                SelectedColor = null;

            foreach (var skin in card.CardComponent.GetComponentsInChildren<Component>(true).Where(x => x.tag == "skin"))
            {
                foreach(var render in skin.GetComponentsInChildren<Renderer>(true))
                    render.material.SetColor("_Color", color);
            }
            SelectedColor = ColorUtility.ToHtmlStringRGBA(color);
        }

        private static string ConvertColor(string color)
        {
            switch (color)
            {
                case "Branca":
                    return "#F8F8FF";
                case "Azul":
                    return "#0000FF";
                case "Rosa":
                    return "#FF1493";
                case "Vermelha":
                    return "#FF0000";
                case "Verde":
                    return "#008000";
                case "Preta":
                    return "#000000";
                case "Amarela":
                    return "#FFFF00";
                case "Roxa":
                    return "#8A2BE2";
                case "Laranja":
                    return "#FF4500";
                default:
                    return "";
            }

        }
    }
}
