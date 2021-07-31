using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Vuforia.Scripts.Cards
{
    [Serializable]
    public class CardMixer
    {
        public static CardMixer Player1Mixer { get; set; }
        public static CardMixer Player2Mixer { get; set; }


        public CardMixer(CharacterCard playerCard)
        {
            PlayerCard = playerCard;
        }

        public static void Intialize()
        {
            Player1Mixer = new CardMixer(TrackedCardsCollection.FirstPlayerDataBase);
            Player2Mixer = new CardMixer(TrackedCardsCollection.SecondPlayerDataBase);
        }

        public CharacterCard PlayerCard { get; set; }
        public bool PlayerGenerated { get; set; }
        public string SeletedSkin { get; set; }
        public string SelectedEffect { get; set; }
        public string Scale { get; set; }
        public string SelectedColor { get; set; }
        public bool RandomGenerated { get; set; }

        public void RandomCards()
        {
            var firstCard = PlayerCard;
            var effectsCards = TrackedCardsCollection.EffectsCards;
            var effectCard = effectsCards.FirstOrDefault();
            var skinCards = TrackedCardsCollection.SkinCards;
            var skinCard = skinCards.FirstOrDefault();
            var scaleCards = TrackedCardsCollection.ScaleCards;
            var scaleCard = scaleCards.FirstOrDefault();
            var colorCards = TrackedCardsCollection.ColorCards;
            var colorCard = colorCards.FirstOrDefault();
            firstCard.Mix(skinCard);
            firstCard.Mix(effectCard);
            firstCard.Mix(scaleCard);
            firstCard.Mix(colorCard);
            SeletedSkin = skinCard.SelectedSkin;
            SelectedEffect = effectCard.SelectedEffect;
            Scale = scaleCard.X.ToString();
            SelectedColor = colorCard.SelectedColor;
            PlayerGenerated = false;
            RandomGenerated = true;
        }

        public void MixTrackedCards()
        {
            var secondPlayer = PlayerCard;
            foreach (var c in TrackedCardsCollection.Cards.Where(x => x.CardType != Assets.Vuforia.Scripts.Cards.CardType.Character))
            {
                secondPlayer.Mix(c);
                switch (c.CardType)
                {
                    case CardType.Skin:
                        SeletedSkin = ((SkinCard)c).SelectedSkin;
                        break;
                    case CardType.Scale:
                        Scale = ((ScaleCard)c).X.ToString();
                        break;
                    case CardType.Effect:
                        SelectedEffect = ((EffectCard)c).SelectedEffect;
                        break;
                    case CardType.Color:
                        SelectedColor = ((ColorCard)c).SelectedColor;
                        break;
                }
            }
        }
    
        public void GeneratePlayer()
        {
            if (PlayerGenerated) return;

            var firstCard = PlayerCard;
            var effectsCards = TrackedCardsCollection.EffectsCards;
            var effectCard = effectsCards.FirstOrDefault();
            effectCard.SelectedEffect = SelectedEffect;

            var skinCards = TrackedCardsCollection.SkinCards;
            var skinCard = skinCards.FirstOrDefault();
            skinCard.SelectedSkin = SeletedSkin;


            var scaleCards = TrackedCardsCollection.ScaleCards;
            var scaleCard = scaleCards.FirstOrDefault();
            //scaleCard.X = float.Parse(Scale);

            var colorCards = TrackedCardsCollection.ColorCards;
            var colorCard = colorCards.FirstOrDefault();
            colorCard.SelectedColor = SelectedColor;
            firstCard.Mix(skinCard);
            firstCard.Mix(effectCard);
            firstCard.Mix(scaleCard);
            firstCard.Mix(colorCard);
            PlayerGenerated = true;
        }
    
        public void Save()
        {
            string destination = Application.persistentDataPath + $"/{PlayerCard.Name}.dat";
            FileStream file;
            if (File.Exists(destination)) file = File.OpenWrite(destination);
            else file = File.Create(destination);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, this);
            file.Close();
        }


        public void LoadFromFile()
        {
            string destination = Application.persistentDataPath + $"/{PlayerCard.Name}.dat";
            FileStream file;

            if (File.Exists(destination)) file = File.OpenRead(destination);
            else
            {
                Debug.LogError("File not found");
                return;
            }

            BinaryFormatter bf = new BinaryFormatter();
            CardMixer data = (CardMixer)bf.Deserialize(file);
            file.Close();

            SeletedSkin = data.SeletedSkin;
            SelectedEffect = data.SelectedEffect;
            Scale = data.Scale;
            SelectedColor = data.SelectedColor;
            PlayerGenerated = false;
        }
    }
}
