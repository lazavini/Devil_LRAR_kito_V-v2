using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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
            Percentage = 0f;
        }

        public static void Intialize()
        {
            Player1Mixer = new CardMixer(TrackedCardsCollection.FirstPlayerDataBase);
            Player2Mixer = new CardMixer(TrackedCardsCollection.SecondPlayerDataBase);
        }

        public static float UpdatePercentages()
        {
            return Player2Mixer.CalculatePercentage();
        }

        public float CalculatePercentage()
        {
            if (Percentage == 1 || RandomGenerated)
                return Percentage;

            Percentage = 0;
            if (PlayerCard.IsActive)
                Percentage += 0.25f;
            else
                return Percentage;

            if (!string.IsNullOrEmpty(SeletedSkin))
                Percentage += 0.25f;
            else
                return Percentage;

            if (!string.IsNullOrEmpty(SelectedEffect))
                Percentage += 0.25f;
            else
                return Percentage;

            if (!string.IsNullOrEmpty(SelectedColor))
                Percentage += 0.25f;
            else
                return Percentage;

            return Percentage;
        }

        public CharacterCard PlayerCard { get; set; }
        public bool PlayerGenerated { get; set; }
        public string SeletedSkin { get; set; }
        public string SelectedEffect { get; set; }
        public string Scale { get; set; }
        public string SelectedColor { get; set; }
        public bool RandomGenerated { get; set; }
        public float Percentage { get; set; }

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
            skinCard.SelectedSkin = null;

            SelectedEffect = effectCard.SelectedEffect;
            effectCard.SelectedEffect = null;


            Scale = scaleCard.X.ToString();
            scaleCard.X = null;


            SelectedColor = colorCard.SelectedColor;
            colorCard.SelectedColor = null;
            PlayerGenerated = false;
            RandomGenerated = true;
        }

        public void MixTrackedCards()
        {
            var secondPlayer = PlayerCard;
            foreach (var c in TrackedCardsCollection.Cards.Where(x => x.CardType != CardType.Character))
            {
                secondPlayer.Mix(c);
                switch (c.CardType)
                {
                    case CardType.Skin:
                        var skinCard = (SkinCard)c;
                        SeletedSkin = skinCard.SelectedSkin;
                        skinCard.SelectedSkin = null;
                        break;
                    case CardType.Scale:
                        var scaleCard = (ScaleCard)c;
                        Scale = scaleCard.X.ToString();
                        scaleCard.X = null;
                        break;
                    case CardType.Effect:
                        var effectCard = (EffectCard)c;
                        SelectedEffect = effectCard.SelectedEffect;
                        effectCard.SelectedEffect = null;
                        break;
                    case CardType.Color:
                        var colorCard = (ColorCard)c;
                        SelectedColor = colorCard.SelectedColor;
                        colorCard.SelectedColor = null;
                        break;
                }
            }
            if (Percentage == 1)
                return;
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
