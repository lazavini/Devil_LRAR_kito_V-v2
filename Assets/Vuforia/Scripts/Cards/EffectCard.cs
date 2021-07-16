using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using Assets.Vuforia.Scripts.Extentions;

namespace Assets.Vuforia.Scripts.Cards
{
    public class EffectCard : ModificationCard
    {
        private ICollection<string> Effects;


        public EffectCard(string name, 
            string sound, 
            string description,
            string animation) : base(name,
            sound,
            description,
            animation)
        {
            Effects = new List<string> { "Fire", "Spirits", "Explosion", "Tornado", "Smoke" , "Diamond", "Electric_1" , "Electric_2", "DarkPower" };
        }
        public override void CardTrackChanged(TrackableBehaviour.Status status)
        {
            base.CardTrackChanged(status);
        }

        public override void Mix(ICard card)
        {
            base.Mix(card);
            var name = Effects.ElementAt(CardComponent.gameObject.GetComponentsInChildren<Dropdown>().FirstOrDefault(x => x.name == "Dropdown").value);
            var oldEffect = card.CardComponent.gameObject.GetComponentsInChildren<Transform>().FirstOrDefault(x => x.name == name);
            
            if (oldEffect != null)
            {
                oldEffect.gameObject.SetActive(!oldEffect.gameObject.activeSelf);
                //oldEffect.parent = this.CardComponent.transform;
                return;
            }

            var effect = CardComponent.gameObject.GetComponentsInChildren<Transform>(true).FirstOrDefault(x => x.name == name);
            var newComponent = Transform.Instantiate(effect);
            //var newComponent = newTransform.GetCopyOf(effect);

            newComponent.name = name;
            newComponent.position = card.CardComponent.transform.position;
            newComponent.gameObject.SetActive(true);
            newComponent.parent = card.CardComponent.transform;

            if (name == "Fire")
                card.Animator?.Play("die");
        }
    }
}
