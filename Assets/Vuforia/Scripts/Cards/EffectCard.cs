using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

namespace Assets.Vuforia.Scripts.Cards
{
    public class EffectCard : ModificationCard
    {
        private ICollection<string> Effects;
        Dropdown DropDownEffect => Status == TrackableBehaviour.Status.TRACKED ? CardComponent.gameObject.GetComponentsInChildren<Dropdown>(false).FirstOrDefault(x => x.name == "Dropdown") : null;
        public string SelectedEffect { get; set; }
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
                "Tornado",
                "Smoke" , 
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
            var effectName = "";
            
            if(SelectedEffect != null)
            {
                effectName = SelectedEffect;
            }
            else
                effectName =  Effects.ElementAt(DropDownEffect?.value ?? (new System.Random()).Next(0, Effects.Count - 1));


            if (CardComponent == null)
            {
                SelectedEffect = effectName;
                return;
            }
            else
                SelectedEffect = null;



            var oldEffect = card.CardComponent?.gameObject.GetComponentsInChildren<Transform>(true).FirstOrDefault(x => x.transform.name == effectName);
            if (oldEffect != null)
            {
                oldEffect.gameObject.SetActive(!oldEffect.gameObject.activeSelf);
                if(oldEffect.gameObject.activeSelf)
                    SelectedEffect = effectName;
                return;
            }

            var effect = CardComponent.gameObject.GetComponentsInChildren<Transform>(true).FirstOrDefault(x => x.name == effectName);
            var newComponent = Transform.Instantiate(effect);

            newComponent.name = effectName;
            newComponent.position = card.CardComponent.transform.position;
            newComponent.gameObject.SetActive(true);
            newComponent.parent = card.CardComponent.transform;
            card.Animator?.Play(effectName);
            SelectedEffect = effectName;
        }
    }
}
