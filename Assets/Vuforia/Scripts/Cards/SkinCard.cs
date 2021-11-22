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
        public List<string> Skins { get; set; }
        public string LastSelectedModel { get; set; }
        public string SelectedSkin { get; set; }

        Dropdown DropDownModel => Status == TrackableBehaviour.Status.TRACKED ? CardPanel.gameObject.GetComponentsInChildren<Dropdown>(false).FirstOrDefault(x => x.name == "Dropdown") : null;

        public SkinCard(string name,
            string sound, 
            string description,
            string animation) : base(name,
            sound,
            description,
            animation,
            CardType.Skin)
        {
            Skins = new List<string> 
            { 
                "Turtle",  
                "Slime",
                "SoldadoEspacial",
                "Astronauta",
                "Knight",
                "BOT",
                "AranhaMonster",
                "AranhaMonster2", 
                "SkeletonArmor",
                "Skeleton_Nu",
                "spider",
                "SoldadoHigh",
                "Devil",
                 "tractor",
                "Male",
                "Female",
                "Berserker",
                "Kevin"
            };
        }
        public override void CardTrackChanged(TrackableBehaviour.Status status)
        {
            base.CardTrackChanged(status);
        }

        public override void Mix(ICard card)
        {
            var skinName = "";
            if(SelectedSkin != null)
            {
                skinName = SelectedSkin;
            }
            else
                skinName = Skins.ElementAt(DropDownModel?.value ?? (new System.Random()).Next(0, Skins.Count - 1));


            if (CardComponent == null)
            {
                SelectedSkin = skinName;
                return;
            }

            var oldSkins = card.CardComponent?.gameObject.GetComponentsInChildren<Transform>(true)
                .Where(x => x.tag == "skin");

            var oldSkinFounded = false;
            foreach (var oldSkin in oldSkins)
            {
                oldSkinFounded = oldSkin.name == skinName;
                oldSkin.gameObject.SetActive(oldSkinFounded);
            }
            if(oldSkinFounded)
            {
                return;
            }

            var skin = CardComponent.gameObject.GetComponentsInChildren<Transform>(true).FirstOrDefault(x => x.name == skinName);
            var newComponent = Transform.Instantiate(skin);
            newComponent.name = skinName;
            newComponent.position = card.CardComponent.transform.position;
            //newComponent.rotation = skin.rotation;
            newComponent.gameObject.SetActive(true);
            newComponent.parent = card.CardComponent.transform;
            SelectedSkin = skinName;
        }
    }
}
