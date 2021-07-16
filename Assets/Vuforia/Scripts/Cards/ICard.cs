using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vuforia;

namespace Assets.Vuforia.Scripts.Cards
{
    public interface ICard
    {
        string Name { get; set; }
        string Sound { get; set; }
        string Description { get; set; }
        Component CardComponent { get; set; }
        Animator Animator { get; set; }
        CardType CardType { get; set; }

        void Mix(ICard card);
        void ChangeCardComponent(Component newComponent);
        void CardTrackChanged(TrackableBehaviour.Status status);
    }
}
