using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using Assets.Vuforia.Scripts.Extentions;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Assets.Vuforia.Scripts.Cards
{
    public class EffectCard : ModificationCard
    {
        private ICollection<string> Effects;
        Dropdown DropDownEffect => null; 
        
        
        //CardComponent.gameObject
        //    .GetComponentsInChildren<Dropdown>(false)
        //    .FirstOrDefault(x => x.name == "Dropdown" && x.enabled);


        public EffectCard(string name, 
            string sound, 
            string description,
            string animation) : base(name,
            sound,
            description,
            animation,
            CardType.Effect)
        {
            Effects = new List<string> 
            { 
                "Fire", 
                "Spirits", 
                "Explosion", 
                "Tornado", "Smoke" , 
                "Diamond", 
                "Electric_1" , 
                "Electric_2", 
                "DarkPower" 
            };
        }
        public override void CardTrackChanged(TrackableBehaviour.Status status)
        {
            base.CardTrackChanged(status);
        }

        public override void Mix(ICard card)
        {
            base.Mix(card);
            var name = Effects.ElementAt(DropDownEffect?.value ?? (new System.Random()).Next(0, Effects.Count - 1));
            var oldEffect = card.CardComponent.gameObject.GetComponentsInChildren<Transform>(true).FirstOrDefault(x => x.transform.name == name);
            if (oldEffect != null)
            {
                oldEffect.gameObject.SetActive(!oldEffect.gameObject.activeSelf);
                return;
            }

            var effect = CardComponent.gameObject.GetComponentsInChildren<Transform>(true).FirstOrDefault(x => x.name == name);
            var newComponent = Transform.Instantiate(effect);

            newComponent.name = name;
            newComponent.position = card.CardComponent.transform.position;
            newComponent.gameObject.SetActive(true);
            newComponent.parent = card.CardComponent.transform;

            card.Animator?.Play(name);
        }
    }
}
