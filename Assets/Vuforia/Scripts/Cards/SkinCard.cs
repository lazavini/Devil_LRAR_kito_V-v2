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
    public class SkinCard : ModificationCard
    {
        private ICollection<string> Skins;


        public SkinCard(string name, 
            string sound, 
            string description,
            string animation) : base(name,
            sound,
            description,
            animation)
        {
            Skins = new List<string> { "Turtle", "spider", "Slime", "SoldierHi", "Soldier", "HumanoidBot", "SoldierPoly", "Knight" };
        }
        public override void CardTrackChanged(TrackableBehaviour.Status status)
        {
            base.CardTrackChanged(status);
        }

        public override void Mix(ICard card)
        {
            base.Mix(card);
            var name = Skins.ElementAt(CardComponent.gameObject.GetComponentsInChildren<Dropdown>().FirstOrDefault(x => x.name == "Dropdown").value);
            var oldSkin = card.CardComponent.gameObject.GetComponentsInChildren<Transform>().FirstOrDefault(x => x.name == name);
            if (oldSkin != null)
            {
                oldSkin.gameObject.SetActive(false);
                oldSkin.parent = this.CardComponent.transform;
                return;
            }
            var skin = CardComponent.gameObject.GetComponentsInChildren<Transform>(true).FirstOrDefault(x => x.name == name);
            skin.gameObject.SetActive(true);
            skin.transform.position = card.CardComponent.transform.position;
            skin.parent = card.CardComponent.transform;
        }
    }
}
