using Assets.Vuforia.Scripts.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class CharacterCard : ICard
{
    public CharacterCard(string name, string sound, string description)
    {
        Name = name;
        Sound = sound;
        Description = description;
        Debug.Log($"Componente {Name}");
        CardType = CardType.Character;
    }
    public string Name { get; set; }
    public string Sound { get; set; }
    public string Description { get; set; }
    public Component CardComponent { get; set; }
    //public Component CardComponent => GameObject.Find(Name)?.GetComponentsInChildren<Renderer>().FirstOrDefault(x => x.name == Name);
    public ICollection<CardPart> Parts => CardComponent == null ? null : CardComponent.GetComponentsInChildren<Renderer>(true)
            .Select(x => new CardPart(x.name, x))?.ToList();

    public ICollection<Transform> ActiveComponents => CardComponent == null ? null : CardComponent.GetComponentsInChildren<Transform>(false);


    public ICollection<Action> ButtonActions { get; set; }
    public CardType CardType { get; set ; }
    public Animator Animator { get => CardComponent == null ? null : CardComponent.GetComponentsInChildren<Animator>().FirstOrDefault(); set => throw new NotImplementedException(); }
    public TrackableBehaviour.Status Status { get; set; }

    public Transform ActiveSkin => ActiveComponents?.FirstOrDefault(x => x.tag == "skin");
    public Transform ActiveEffect => ActiveComponents?.FirstOrDefault(x => x.tag == "effect");
    public bool IsActive => Status == TrackableBehaviour.Status.TRACKED;

    public void ChangeCardComponent(Component component)
    {
        CardComponent = component;
    }

    public void CardTrackChanged(TrackableBehaviour.Status status)
    {
        if(status == TrackableBehaviour.Status.TRACKED)
        {
            Show();
            //var parts = Parts?.ToList();
            //var arms = parts?.FirstOrDefault(x => x.Type == CardPartType.Arms);
            ////arms?.CardPartRenderer?.gameObject?.SetActive(false);
            ///
            Animator?.Play("idle");
            //var head = parts?.FirstOrDefault(x => x.Type == CardPartType.Head);
            //head?.CardPartRenderer?.gameObject?.SetActive(false);
        }
        else
        {
            Hide();
        }
        Status = status;
    }

    private void Hide()
    {
        if (CardComponent == null) return;
        var rendererComponents = CardComponent.GetComponentsInChildren<Renderer>(true);
        var colliderComponents = CardComponent.GetComponentsInChildren<Collider>(true);
        var canvasComponents = CardComponent.GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;
    }

    private void Show()
    {
        if (CardComponent == null) return;
        var rendererComponents = CardComponent.GetComponentsInChildren<Renderer>(true);
        var colliderComponents = CardComponent.GetComponentsInChildren<Collider>(true);
        var canvasComponents = CardComponent.GetComponentsInChildren<Canvas>(true);
        //Enable rendering:
        foreach (var component in rendererComponents)
        {
            component.enabled = true;
            Debug.Log("Component name " + component.name);
            if (component.GetComponent<Animation>())
            {
                component.GetComponent<Animation>().Play();
                // Debug.Log("Animation playing " + component.GetComponent<Animation>().isPlaying);
            }
        }
        //Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;
        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;



    }


    public void AddButtonPlaySound(Button button)
    {
        button.onClick.AddListener(PlaySound);
    }



    public void MixCards(params ICard[] cards)
    {
        if (cards == null) return;
        foreach (var card in cards)
            Mix(card);
        
    }


    public void Mix(ICard card)
    {
        if (card.CardType == CardType.Character)
        {
            return;
        }
        card.Mix(this);
    }

    public void Rotate()
    {
        var oldRotation = CardComponent.transform.localRotation;
        CardComponent.transform.localRotation = new Quaternion(oldRotation.x + 1, oldRotation.y, oldRotation.z, oldRotation.w);
    }

    void PlaySound(string sound)
    {

    }

    public void ChangeHead(Renderer newArm)
    {
        Parts?.FirstOrDefault(x => x.Type == CardPartType.Head)?.ChangeModel(newArm);
    }


    public void ChangeArms(Renderer newArm)
    {
        Parts?.FirstOrDefault(x => x.Type == CardPartType.Arms)?.ChangeModel(newArm);
    }


    void PlaySound()
    {
        var clipTarget = (AudioClip)Resources.Load(Sound);
        var soundTarget = new AudioSource
        {
            clip = clipTarget,
            loop = false,
            playOnAwake = false
        };
        soundTarget.Play();
    }

    public override bool Equals(object obj)
    {
        return obj is ICard card &&
                Name == card.Name;
    }

    public override int GetHashCode()
    {
        return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
    }

    void ICard.Mix(ICard card)
    {
        if (card.CardType == CardType.Character)
        {
            return;
        }
        card.Mix(this);
    }

    public void DestroyObjects()
    {
        var oldSkins = CardComponent?.gameObject.GetComponentsInChildren<Transform>(true)
            .Where(x => x.name.Contains("player")) ?? Enumerable.Empty<Transform>();

        foreach (var oldskin in ActiveComponents.Where(x => x.tag == "skin" || x.tag == "effect"))
            oldskin.gameObject.SetActive(false);
            //GameObject.Destroy(oldskin.gameObject);
    }
}
