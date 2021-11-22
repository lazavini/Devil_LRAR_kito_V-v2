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

        void PlaySound()
        {
            var clipTarget = (AudioClip)Resources.Load($"Sounds_fala/{Name}");

            if (clipTarget == null)
                return;

            AudioSource.PlayClipAtPoint(clipTarget, CardComponent.transform.position);
        }

        private void Hide()
        {
            if (CardComponent == null) return;
            var rendererComponents = CardComponent.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = CardComponent.GetComponentsInChildren<Collider>(true);
            
            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            var mainContainer = GameObject.FindObjectsOfType<Component>()
                    .FirstOrDefault(x => x.name == "MainContainer");

            var modificationMenu = mainContainer.gameObject.GetComponentsInChildren<RectTransform>(true)
                .FirstOrDefault(x => Name == x.name);

            modificationMenu?.gameObject?.SetActive(false);
        }

        private void Show()
        {
            if (CardComponent == null) return;
            var rendererComponents = CardComponent.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = CardComponent.GetComponentsInChildren<Collider>(true);
            if (Canvas == null)
                Canvas = CardComponent.GetComponentsInChildren<Canvas>(true).Where(x => x.name == Name);
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
            var modificationMenu = mainContainer.gameObject.GetComponentsInChildren<RectTransform>(true)
                .FirstOrDefault(x => Name == x.name);
            modificationMenu?.gameObject?.SetActive(true);
            PlaySound();
        }
    }
}