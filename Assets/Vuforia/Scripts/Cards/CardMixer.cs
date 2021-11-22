using Newtonsoft.Json;
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
    public class CardMixer
    {

        public static CardMixer Player1Mixer { get; set; }
        public static CardMixer Player2Mixer { get; set; }


        public CardMixer(CharacterCard playerCard)
        {
            PlayerCard = playerCard;
            Percentage = 0f;
            PlayerState = new PlayerState();
        }

        public static void Intialize()
        {
            Player1Mixer = new CardMixer(TrackedCardsCollection.FirstPlayerDataBase);
            Player2Mixer = new CardMixer(TrackedCardsCollection.SecondPlayerDataBase);
        }

        public static float UpdatePercentages()
        {
            if(!Player1Mixer.PlayerState.RandomGenerated)
                return Player1Mixer.CalculatePercentage();
            return Player2Mixer.CalculatePercentage();

        }

        public float CalculatePercentage()
        {
            if (!Player1Mixer.PlayerState.RandomGenerated)
            {
                if (Percentage == 1 || PlayerState.RandomGenerated)
                    return Percentage;

                Percentage = 0;
                if (PlayerCard.IsActive)
                    Percentage += 0.2f;
                else
                    return Percentage;

                if (!string.IsNullOrEmpty(PlayerState.SeletedSkin))
                    Percentage += 0.2f;
                else
                    return Percentage;

                if (!string.IsNullOrEmpty(PlayerState.SelectedEffect))
                    Percentage += 0.2f;
                else
                    return Percentage;
                if (!string.IsNullOrEmpty(PlayerState.Scale))
                    Percentage += 0.2f;
                else
                    return Percentage;
                if (!string.IsNullOrEmpty(PlayerState.SelectedColor))
                    Percentage += 0.2f;
                else
                    return Percentage;
            }
            else
            {
                Percentage = 0;
                if (Player2Mixer.PlayerCard.IsActive)
                    Percentage += 0.2f;

                if (Player1Mixer.PlayerState.SeletedSkin == Player2Mixer.PlayerState.SeletedSkin)
                    Percentage += 0.2f;

                if (Player1Mixer.PlayerState.SelectedEffect == Player2Mixer.PlayerState.SelectedEffect)
                    Percentage += 0.2f;
                
                if (Player1Mixer.PlayerState.Scale == Player2Mixer.PlayerState.Scale)
                    Percentage += 0.2f;

                if (Player1Mixer.PlayerState.SelectedColor == Player2Mixer.PlayerState.SelectedColor)
                    Percentage += 0.2f;
            }
            return Percentage;
        }

        

        public CharacterCard PlayerCard { get; set; }
        public bool PlayerGenerated { get; set; }
        public PlayerState PlayerState { get; set; }
        public float Percentage { get; set; }

        private static string SavePath
        {
            get
            {
                try
                {
                    var folder = $"{Application.persistentDataPath}/";
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);
                    return folder;
                }
                catch (Exception)
                {
                    return $"{Application.persistentDataPath}/";
                }
            }
        }

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
            
            PlayerState.SeletedSkin = skinCard.SelectedSkin;
            skinCard.SelectedSkin = null;

            PlayerState.SelectedEffect = effectCard.SelectedEffect;
            effectCard.SelectedEffect = null;
            PlayerState.Scale = scaleCard.Y.ToString();


            PlayerState.SelectedColor = colorCard.SelectedColor;
            colorCard.SelectedColor = null;
            PlayerGenerated = false;
            PlayerState.RandomGenerated = true;
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
                        PlayerState.SeletedSkin = skinCard.SelectedSkin;
                        skinCard.SelectedSkin = null;
                        break;
                    case CardType.Scale:
                        var scaleCard = (ScaleCard)c;
                        PlayerState.Scale = scaleCard.Y.ToString();
                        scaleCard.X = null;
                        break;
                    case CardType.Effect:
                        var effectCard = (EffectCard)c;
                        PlayerState.SelectedEffect = effectCard.SelectedEffect;
                        effectCard.SelectedEffect = null;
                        break;
                    case CardType.Color:
                        var colorCard = (ColorCard)c;
                        PlayerState.SelectedColor = colorCard.SelectedColor;
                        colorCard.SelectedColor = null;
                        break;
                }
            }
        }

        public void RegeneratePlayer()
        {
            PlayerGenerated = false;
            GeneratePlayer();
        }

        public void LoadState(PlayerState playerState)
        {
            PlayerState = playerState;
            PlayerState.IsLoading = true;
            RegeneratePlayer();
        }
    
        public void GeneratePlayer()
        {
            if (PlayerGenerated) return;

            var firstCard = PlayerCard;

            var effectsCards = TrackedCardsCollection.EffectsCards;
            var effectCard = effectsCards.FirstOrDefault();
            effectCard.SelectedEffect = PlayerState.SelectedEffect;
            var skinCards = TrackedCardsCollection.SkinCards;
            var skinCard = skinCards.FirstOrDefault();
            skinCard.SelectedSkin = PlayerState.SeletedSkin;
            var scaleCards = TrackedCardsCollection.ScaleCards;
            var scaleCard = scaleCards.FirstOrDefault();
            scaleCard.X = float.Parse(PlayerState.Scale);
            firstCard.Mix(scaleCard);
            var colorCards = TrackedCardsCollection.ColorCards;
            var colorCard = colorCards.FirstOrDefault();
            colorCard.SelectedColor = "#" + PlayerState.SelectedColor;
            firstCard.Mix(skinCard);
            firstCard.Mix(effectCard);
            //firstCard.Mix(scaleCard);
            firstCard.Mix(colorCard);
            PlayerGenerated = firstCard.CardComponent != null;
            PlayerState.IsLoading = !PlayerGenerated;
        }
    

        public static IEnumerable<string> GetSavesNames()
        {
            var files = new List<string>();
            foreach(var path in GetSaves())
            {
                var fileInfo = new FileInfo(path);
                files.Add(fileInfo.Name.Replace(fileInfo.Extension,""));
            }
            return files.OrderBy(x => x);
        }

        public static IEnumerable<string> GetSaves()
        {
            return Directory.EnumerateFiles(SavePath);
        }

        public static void Save(string saveName)
        {
            string destination = $"{SavePath}{saveName}.json";
            File.WriteAllText(destination, GenerateJson());
        }

        public static void LoadFromFile(string file)
        {
            if (!File.Exists(file))
                return;
            LoadFromJson(File.ReadAllText(file));
        }

        public static void LoadFromName(string saveName)
        {
            string destination = $"{SavePath}{saveName}.json";
            if (!File.Exists(destination))
                return;
            LoadFromJson(File.ReadAllText(destination));
        }

        public static void LoadFromJson(string json)
        {
            var states = JsonConvert.DeserializeObject<List<PlayerState>>(json);
            Player1Mixer.LoadState(states[0]);
            Player2Mixer.LoadState(states[1]);
        }

        public static string GenerateJson()
        {
            return JsonConvert.SerializeObject(new List<PlayerState> { Player1Mixer.PlayerState, Player2Mixer.PlayerState });
        }
    }
}
