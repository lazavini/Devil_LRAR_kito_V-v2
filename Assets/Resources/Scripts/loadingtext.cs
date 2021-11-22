using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingtext : MonoBehaviour {

    private RectTransform rectComponent;
    private Image imageComp;

    public float speed = 200f;
    public Text text;
    public Text textStatus;
    public Text textInstruction;
    float amount = -1;



    // Use this for initialization
    void Start () 
    {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        imageComp.fillAmount = 0.0f;
    }

    void PlaySound(string sound)
    {
        if (amount == imageComp.fillAmount)
            return;
        var clipTarget = (AudioClip)Resources.Load(sound);
        AudioSource.PlayClipAtPoint(clipTarget, new Vector3(0,0,0), 1);
    }

    // Update is called once per frame
    void Update () 
    {
        var a = imageComp.fillAmount;
        if (a == 0)
        {
            textStatus.text = "Coloque a carta player...";
            textInstruction.text = "Coloque a carta player";
            PlaySound("Sound_fala/STEMS LAZIM 1-16-Audio 2");
        }

        if (a == 0.2f)
        {
            textStatus.text = "Coloque a carta skin...";
            textInstruction.text = "Add skin card";
            PlaySound("Sound_fala/STEMS LAZIM 2-20-Audio 1");
        }

        if (a == 0.4f)
        {
            textStatus.text = "Coloque a carta power...";
            textInstruction.text = "Add power card";
            PlaySound("Sound_fala/STEMS LAZIM 4-27-Audio 1");
        }

        if (a == 0.6f)
        {
            textStatus.text = "Coloque a carta scale...";
            textInstruction.text = "Add scale card";
            PlaySound("Sound_fala/STEMS LAZIM 6-36-Audio 1");
        }


        if (a == 0.8f)
        {
            textStatus.text = "Coloque a carta color...";
            textInstruction.text = "Add color card";
            PlaySound("Sound_fala/STEMS LAZIM 8-11-44-Audio 1 3");
        }

        if (a == 1)
        {
            textStatus.text = "Heroi completo!";
            textInstruction.text = "";
            PlaySound("Sound_fala/STEMS LAZIM 10-52-Audio 1");
        }

        amount = imageComp.fillAmount;
        text.text = a * 100 + "%";
    }
}
