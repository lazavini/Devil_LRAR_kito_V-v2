using Assets.Vuforia.Scripts.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Vuforia;

public static class TrackedCardsCollection
{
    public static ICard FirstPlayer => Cards?.FirstOrDefault(x => x.CardComponent != null && x.CardType == CardType.Character && x.Name == "card_player1") ?? null;
    public static CharacterCard FirstPlayerDataBase => (CharacterCard)cardsDataBase?.FirstOrDefault(x => x.CardType == CardType.Character && x.Name == "card_player1") ?? null;
    public static ICard SecondPlayer => Cards?.FirstOrDefault(x => x.CardComponent != null && x.CardType == CardType.Character && x.Name == "card_player2") ?? null;
    public static CharacterCard SecondPlayerDataBase => (CharacterCard)cardsDataBase?.FirstOrDefault(x => x.CardType == CardType.Character && x.Name == "card_player2") ?? null;


    private static List<ICard> cards;
    private static List<ICard> cardsDataBase;

    public static List<ICard> Cards { get => cards?.Where(x => x.CardComponent != null).ToList() ?? new List<ICard>(); set => cards = value; }
    public static List<CharacterCard> CharacterCards { get => cards?.Where(x => x.CardComponent != null && x.CardType == CardType.Character).Cast<CharacterCard>().ToList() ?? new List<CharacterCard>();}

    public static List<EffectCard> EffectsCards { get => cardsDataBase?.Where(x => x.CardType == CardType.Effect).Cast<EffectCard>().ToList() ?? new List<EffectCard>(); }
    public static List<SkinCard> SkinCards { get => cardsDataBase?.Where(x => x.CardType == CardType.Skin).Cast<SkinCard>().ToList() ?? new List<SkinCard>(); }
    public static List<ScaleCard> ScaleCards { get => cardsDataBase?.Where(x => x.CardType == CardType.Scale).Cast<ScaleCard>().ToList() ?? new List<ScaleCard>(); }
    public static List<ColorCard> ColorCards { get => cardsDataBase?.Where(x => x.CardType == CardType.Color).Cast<ColorCard>().ToList() ?? new List<ColorCard>(); }




    public static List<ICard> CardsDataBase { get => cardsDataBase; set => cardsDataBase = value; }

    public static void InitializeDataBase()
    {
        if (cardsDataBase != null) return;
        cardsDataBase = new List<ICard>
        {
            new CharacterCard("card_player1",
                "sounds/truck_sound",
                "Computational Thinking is based on the concepts of Pattern ."),
            new CharacterCard("card_player2",
                "sounds/truck_sound",
                "Computational Thinking is based on "),
            new ScaleCard("card_scale","","Scale card", 2,2,2),
            new EffectCard("card_power","","Power card","walk"),
            new SkinCard("card_skin","", "Skin card","idle"),
            new ColorCard("card_color","","Colocar card ak", null)



            //new CharacterCard("CT_Head",
            //    "sounds/truck_sound",
            //    "Making a software."),

            ////new CharacterCard("programing",
            ////    "sounds/truck_sound",
            ////    "Write a set of instruction."),

            //  new CharacterCard("patterns",
            //    "sounds/truck_sound",
            //    "spotting and using similarites."),

            //  new CharacterCard("Abstraction",
            //    "sounds/truck_sound",
            //    "Removindo unnecessary detail."),

            //  new CharacterCard("decomposition",
            //    "sounds/truck_sound",
            //    "break down in to smaller parts."),

            //  new CharacterCard("algorithms",
            //    "sounds/truck_sound",
            //    "making steps and rules."),

            //  new CharacterCard("evaluation",
            //    "sounds/truck_sound",
            //    "making judgements"),

            //    new CharacterCard("logic",
            //    "sounds/truck_sound",
            //    "predict and analyse.")
        };
    }


    public static void AddCard(ICard card)
    {
        if (cards == null)
            cards = new List<ICard>();
        cards.Add(card);
    }

    public static void RemoveCard(ICard card)
    {
        cards?.Remove(card);
    }
}

