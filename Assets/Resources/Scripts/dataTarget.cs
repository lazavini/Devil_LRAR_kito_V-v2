using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
namespace Vuforia
{



    public class dataTarget : MonoBehaviour
    {
        public Text label;
        public Transform TextTargetName;
        public Transform TextDescription;
        public Transform ButtonAction;
        public Transform PanelDescription;
        public AudioSource soundTarget;
        public AudioClip clipTarget;

        // Use this for initialization
        void Start()
        {
            //add Audio Source as new game object component
            TrackedCardsCollection.InitializeDataBase();
            soundTarget = (AudioSource)gameObject.AddComponent<AudioSource>();
            //label = GameObject.Find("LabelName").GetComponent<Text>();
            //label.text = "no name";
            // Debug.Log("Deu start e tem inicialmente nome de = "+label.text);
        }

        // Update is called once per frame
        void Update()
        {
            //foreach (var card in TrackedCardsCollection.Cards)
            //{
            //    //card.CardComponent.gameObject.SetActive(true);
            //    //TrackedCardsCollection.AddCard(card);
            //}
            //Debug.Log(TrackedCardsCollection.Cards.Count);
            //StateManager sm = TrackerManager.Instance.GetStateManager();
            //IEnumerable<TrackableBehaviour> tbs = sm.GetActiveTrackableBehaviours();

            //foreach (TrackableBehaviour tb in tbs)
            //{
            //    string name = tb.TrackableName;
            //    ImageTarget it = tb.Trackable as ImageTarget;
            //    Vector2 size = it.GetSize();
            //    TextTargetName.GetComponent<Text>().text = name;
            //    ButtonAction.gameObject.SetActive(true);
            //    TextDescription.gameObject.SetActive(true);
            //    PanelDescription.gameObject.SetActive(true);

            //    if (name == "computationalThinking_marker")
            //    {
            //        ButtonAction.GetComponent<Button>().onClick.AddListener(delegate { playSound("sounds/robotic-transformer-4"); });
            //        TextDescription.GetComponent<Text>().text = "Computational Thinking is based on the concepts of Pattern Recognition, Abstraction, Problem Decomposition, Algorithms.";
            //        label = GameObject.Find("LabelName").GetComponent<Text>();
            //        label.text = name;


            //    }

            //    if (name == "CT_Head")
            //    {
            //        ButtonAction.GetComponent<Button>().onClick.AddListener(delegate { playSound("sounds/Robot_start"); });
            //        TextDescription.GetComponent<Text>().text = "Making a software";
            //        label.text = gameObject.name.ToString();
            //        //Debug.Log(label.text);
            //    }
            //    if (name == "programing")
            //    {
            //        ButtonAction.GetComponent<Button>().onClick.AddListener(delegate
            //        { playSound("sounds/truck_sound"); });
            //        TextDescription.GetComponent<Text>().text = "Write a set of instruction.";
            //        Debug.Log("Nome do GameObject = " + gameObject.name.ToString());
            //    }

            //    if (name == "patterns")
            //    {
            //        ButtonAction.GetComponent<Button>().onClick.AddListener(delegate {
            //            playSound("sounds/cops");
            //        });
            //        TextDescription.GetComponent<Text>().text = "Patterns - Spotting and using similarities";
            //        label.text = gameObject.name.ToString();
            //    }
            //    if (name == "abstraction")
            //    {
            //        ButtonAction.GetComponent<Button>().onClick.AddListener(delegate {
            //            playSound("sounds/cops");
            //        });
            //        TextDescription.GetComponent<Text>().text = "Abstraction - Removing unnecessary detais";
            //        label.text = gameObject.name.ToString();
            //    }
            //    if (name == "algorithms")
            //    {
            //        ButtonAction.GetComponent<Button>().onClick.AddListener(delegate {
            //            playSound("sounds/tasmanian-devil-fx");
            //        });
            //        TextDescription.GetComponent<Text>().text = "Algorithms - Making steps and rules";
            //        label.text = gameObject.name.ToString();
            //    }

            //    if (name == "decomposition")
            //    {
            //        ButtonAction.GetComponent<Button>().onClick.AddListener(delegate {
            //            playSound("sounds/cops");
            //        });
            //        TextDescription.GetComponent<Text>().text = "Decomposition -Break down into smaller parts";
            //        label.text = gameObject.name.ToString();
            //    }
            //    if (name == "evaluation")
            //    {
            //        ButtonAction.GetComponent<Button>().onClick.AddListener(delegate {
            //            playSound("sounds/cops");
            //        });
            //        TextDescription.GetComponent<Text>().text = "Evaluation -Making judgements";
            //        label.text = gameObject.name.ToString();
            //    }
            //    if (name == "logic")
            //    {
            //        ButtonAction.GetComponent<Button>().onClick.AddListener(delegate {
            //            playSound("sounds/cops");
            //        });
            //        TextDescription.GetComponent<Text>().text = "Logic - predict and analyze";
            //        label.text = gameObject.name.ToString();
            //    }
            //}
        }

        //function to play sound
        void playSound(string ss)
        {
            clipTarget = (AudioClip)Resources.Load(ss);
            soundTarget.clip = clipTarget;
            soundTarget.loop = false;
            soundTarget.playOnAwake = false;
            soundTarget.Play();

        }

    }
}
