using System.Collections;
using UnityEngine;
using Vuforia;
using System.Linq;
using System.Collections.Generic;

namespace Assets.Vuforia.Scripts.Cards
{
    public abstract class ModificationCard : ICard
    {
        public ModificationCard(string name, string sound, string description, string animation, CardType cardType)
        {
            Name = name;
            Sound = sound;
            Description = description;
            CardType = cardType;
            Animation = animation;
        }

        public string Name { get ; set ; }
        public string Sound { get ; set ; }
        public string Description { get ; set ; }
        public Component CardComponent { get ; set ; }
        public CardType CardType { get ; set ; }
        public string Animation { get; set; }
        public Animator Animator { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public TrackableBehaviour.Status Status { get; set; }
        public IEnumerable<Canvas> Canvas { get; set; }

        public virtual void CardTrackChanged(TrackableBehaviour.Status status)
        {
            if (status == Status)
                return;

            if (status == TrackableBehaviour.Status.TRACKED)
            {
                Show();
            }
            else
            {
                Hide();
            }
            Status = status;
        }

        public virtual void ChangeCardComponent(Component newComponent)
        {
            CardComponent = newComponent;
        }

        public virtual void Mix(ICard card)
        {
            if (!string.IsNullOrEmpty(Animation))
                card.Animator?.Play(Animation);



        }

        private void Hide()
        {
            if (CardComponent == null) return;
            var rendererComponents = CardComponent.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = CardComponent.GetComponentsInChildren<Collider>(true);
            if(Canvas == null)
                Canvas = CardComponent.GetComponentsInChildren<Canvas>(true);
            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            var mainContainer = GameObject.FindObjectsOfType<Component>()
                    .FirstOrDefault(x => x.name == "MainContainer");
            // Disable canvas':
            foreach (var component in Canvas)
            {
                if (mainContainer == null)
                    break;

                var oldMenu = mainContainer.gameObject.GetComponentsInChildren<Canvas>(true)
                    .FirstOrDefault(x => x.name == component.name);

                if (oldMenu != null)
                    oldMenu.gameObject.SetActive(false);

                component.gameObject.SetActive(false);
            }
        }

        private void Show()
        {
            if (CardComponent == null) return;
            var rendererComponents = CardComponent.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = CardComponent.GetComponentsInChildren<Collider>(true);
            if (Canvas == null)
                Canvas = CardComponent.GetComponentsInChildren<Canvas>(true);
            //Enable rendering:
            foreach (var component in rendererComponents)
            {
                component.enabled = true;
            }
            //Enable colliders:
            foreach (var component in colliderComponents)
                component.enabled = true;
            // Enable canvas':
            var mainContainer = GameObject.FindObjectsOfType<Component>()
                    .FirstOrDefault(x => x.name == "MainContainer");

            foreach (var component in Canvas)
            {
                component.gameObject.SetActive(true);
                var oldMenu = mainContainer.gameObject.GetComponentsInChildren<Canvas>(true)
                    .FirstOrDefault(x => x.name == component.name);


                if (oldMenu != null)
                {
                    oldMenu.gameObject.SetActive(true);
                }
                else
                {
                    component.transform.parent = mainContainer.transform;
                }
            }
            RecreateMenus();
        }

        void RecreateMenus()
        {
            if (CardComponent == null) return;
            
            var mainContainer = GameObject.FindObjectsOfType<Component>()
                    .FirstOrDefault(x => x.name == "MainContainer");
            var allPanels = mainContainer.GetComponentsInChildren<RectTransform>()
                .Where(x => x.name == "Panel")
                .ToArray();

            float y = 0;
            for(var i = 0; i < allPanels.Length; i++)
            {
                var panel = allPanels[i];
                float z = panel.gameObject.transform.localPosition.z;
                float xPosition = panel.gameObject.transform.localPosition.x;
                var children = panel.gameObject.GetComponentsInChildren<RectTransform>()
                    .Where(x => x.name != "Panel");
                var maxHeight = children.Max(x => x.rect.height);
                panel.localPosition = new Vector3(xPosition, y + panel.rect.height, z);
                y += maxHeight;
            }
        }
    }
}