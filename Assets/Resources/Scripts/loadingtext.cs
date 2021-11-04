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



    // Use this for initialization
    void Start () 
    {
        rectComponent = GetComponent<RectTransform>();
        imageComp = rectComponent.GetComponent<Image>();
        imageComp.fillAmount = 0.0f;
    }
	
	// Update is called once per frame
	void Update () 
    {
        var a = imageComp.fillAmount;
        if (a == 0)
        {
            textStatus.text = "Coloque a carta player...";
            textInstruction.text = "Coloque a carta player";
            //textInstruction.text = "Add player card";
        }

        if(a == 0.25f)
        {
            textStatus.text = "Coloque a carta skin...";
            textInstruction.text = "Add skin card";

        }

        if (a == 0.5f)
        {
            textStatus.text = "Coloque a carta power...";
            textInstruction.text = "Add power card";
        }


        if (a == 0.75f)
        {
            textStatus.text = "Coloque a carta color...";
            textInstruction.text = "Add color card";
        }

        if(a == 1)
        {
            textStatus.text = "Heroi completo!";
            textInstruction.text = "";
        }

        text.text = a * 100 + "%";
    }
}
