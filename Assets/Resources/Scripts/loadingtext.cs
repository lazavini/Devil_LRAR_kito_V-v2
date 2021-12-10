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
        AudioSource.PlayClipAtPoint(clipTarget,
                                    new Vector3(0, 0, 0),
                             1);
    }

    // Update is called once per frame
    void Update () 
    {
        var a = imageComp.fillAmount;
        if (a == 0)
        {
            textStatus.text = "Coloque a carta player!";
            textInstruction.text = "Coloque a carta player";
            PlaySound("Sound_fala/Carta_player");
        }

        if (a == 0.2f)
        {
            textStatus.text = "Coloque a carta skin!";
            textInstruction.text = "Coloque skin card";
            PlaySound("Sound_fala/Carta_skin");
        }

        if (a == 0.4f)
        {
            textStatus.text = "Coloque a carta power!";
            textInstruction.text = "Coloque power card";
            PlaySound("Sound_fala/Carta_power");
        }

        if (a == 0.6f)
        {
            textStatus.text = "Coloque a carta scale!";
            textInstruction.text = "Coloque scale card";
            PlaySound("Sound_fala/Carta_scale");
        }


        if (a == 0.8f)
        {
            textStatus.text = "Coloque a carta color!";
            textInstruction.text = "Coloque color card";
            PlaySound("Sound_fala/carta_color");
        }

        if (a == 1)
        {
            textStatus.text = "Heroi completo!";
            textInstruction.text = "";
            PlaySound("Sound_fala/Heroi_completo");
        }

        amount = imageComp.fillAmount;
        text.text = a * 100 + "%";
    }
}
