using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class CardPart
{
    public CardPart(string type, Renderer cardPartRenderer)
    {
        switch (type)
        {
            case "Arms":
                Type = CardPartType.Arms;
                break;
            case "FootLeft":
                Type = CardPartType.FootLeft;
                break;
            case "FootRight":
                Type = CardPartType.FootRight;
                break;
            case "Head":
                Type = CardPartType.Head;
                break;
            case "HornLeft":
                Type = CardPartType.HornLeft;
                break;
            case "HornRight":
                Type = CardPartType.HornRight;
                break;
            case "MaskLeft":
                Type = CardPartType.MaskLeft;
                break;
            case "MaskRight":
                Type = CardPartType.MaskRight;
                break;
            case "Mounth":
                Type = CardPartType.Mounth;
                break;
            case "Effect":
                Type = CardPartType.Effect;
                break;
            case "Ground":
                Type = CardPartType.Ground;
                break;
        };
        CardPartRenderer = cardPartRenderer;
    }

    public void ChangeModel(Renderer renderer)
    {
        CardPartRenderer.material = renderer.material;
        //renderer.transform.localPosition = CardPartRenderer.transform.localPosition;
        //renderer.transform.localScale = CardPartRenderer.transform.localScale;
        //renderer.transform.localRotation = CardPartRenderer.transform.localRotation;
        //renderer.gameObject.SetActive(true);
        //CardPartRenderer?.gameObject.SetActive(false);
        //CardPartRenderer = renderer;
    }

    public CardPartType Type { get; private set; }
    public Renderer CardPartRenderer { get; private set; }
}
