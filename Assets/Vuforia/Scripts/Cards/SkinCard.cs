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
        FlexibleColorPicker ColorPicker => CardComponent.gameObject.activeSelf ? CardComponent.gameObject.GetComponentInChildren<FlexibleColorPicker>(true) : null;
        Dropdown DropDownModel => CardComponent.gameObject.activeSelf ?  CardComponent.gameObject.GetComponentsInChildren<Dropdown>(false).FirstOrDefault(x => x.name == "Dropdown") : null;

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
                "Player1", 
                "Turtle",  
                "Slime", 
                "Soldier", 
                "SoldierHi", 
                "SoldierPoly",
                "Knight", 
                "HumanoidBot",
                "BattleSpider01", 
                "BattleSpider02", 
                "SkeletonArmor",
                "Skeleton_NoArmor" 
            };
        }
        public override void CardTrackChanged(TrackableBehaviour.Status status)
        {
            base.CardTrackChanged(status);
        }

        public override void Mix(ICard card)
        {
            var name = Skins.ElementAt(DropDownModel?.value ?? (new System.Random()).Next(0, Skins.Count - 1));
            var oldSkin = card.CardComponent.gameObject.GetComponentsInChildren<Transform>(true)
                .FirstOrDefault(x => x.name == name);
            if (LastSelectedModel != null && name != LastSelectedModel)
            {
                var lastSelectedModel = card.CardComponent.gameObject.GetComponentsInChildren<Transform>(true)
                    .FirstOrDefault(x => x.name == LastSelectedModel);
                lastSelectedModel.gameObject.SetActive(false);
            }

            LastSelectedModel = name;
            if(oldSkin != null)
            {
                oldSkin.gameObject.SetActive(true);
                ChangeComponentColor(oldSkin);
                return;
            }

            var skin = CardComponent.gameObject.GetComponentsInChildren<Transform>(true).FirstOrDefault(x => x.name == name);
            var newComponent = Transform.Instantiate(skin);
            newComponent.name = name;
            newComponent.position = card.CardComponent.transform.position;
            newComponent.gameObject.SetActive(true);
            newComponent.parent = card.CardComponent.transform;
            ChangeComponentColor(newComponent);
        }

        private void ChangeComponentColor(Transform component)
        {
            var color = ColorPicker?.color ?? 
                new Color(UnityEngine.Random.Range(-1, 5), 
                UnityEngine.Random.Range(-1, 5), 
                UnityEngine.Random.Range(-1, 5),
                1);

            foreach (var render in component.GetComponentsInChildren<Renderer>())
            {
                render.material.SetColor("_Color", color);
            }
        }
    }
}
