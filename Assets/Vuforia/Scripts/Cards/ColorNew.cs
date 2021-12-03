using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

namespace Assets.Vuforia.Scripts.Cards
{
    public class ColorNew : ModificationCard
    {
        private ICollection<string> Color;
        Dropdown DropDownEffect => Status == TrackableBehaviour.Status.TRACKED ? CardPanel.gameObject.GetComponentsInChildren<Dropdown>(false).FirstOrDefault(x => x.name == "Dropdown") : null;
        public string SelectedColor { get; set; }
        public ColorNew(string name, 
            string sound, 
            string description,
            string animation) : base(name,
            sound,
            description,
            animation,
            CardType.Effect)
        {
            Color = new List<string> 
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
            base.Mix(card);

            var Color = "";
            
            if(SelectedColor != null)
            {
                ColorName = SelectedColor;
            }
            else
                ColorName =  Color.ElementAt(DropDownColor?.value ?? (new System.Random()).Next(0, Effects.Count - 1));


            if (CardComponent == null)
            {
                SelectedColor = ColorName;
                return;
            }
            else
                SelectedColor = null;


            var oldColor = card.CardComponent?.gameObject.GetComponentsInChildren<Transform>(true).Where(x => x.tag == "Color");
            foreach (var fx in oldEffects)
                GameObject.Destroy(fx.gameObject);

            var oldEffect = card.CardComponent?.gameObject.GetComponentsInChildren<Transform>(true).FirstOrDefault(x => x.transform.name == ColorName);
            if (oldEffect != null)
            {
                SelectedColor = ColorName;
                return;
            }

            var effect = CardComponent.gameObject.GetComponentsInChildren<Transform>(true).FirstOrDefault(x => x.name ==ColorName);
            var newComponent = Transform.Instantiate(Color);
           
    }
}
