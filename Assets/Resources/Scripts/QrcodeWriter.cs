using Assets.Vuforia.Scripts.Cards;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.Rendering;

public class QrcodeWriter
{
    QRCodeWriter _writer;
    Color32Renderer _render;
    public Button ButtonWrite;
    public QrcodeWriter()
    {
        _writer = new QRCodeWriter();
        _render = new Color32Renderer();
    }

    public Texture2D GenerateQrcode(string text)
    {
        var matrix = _writer.encode(text, BarcodeFormat.QR_CODE, 200, 200);
        var img = _render.Render(matrix, BarcodeFormat.QR_CODE, text, new EncodingOptions { Height = 200, Width = 200 });
        var encoded = new Texture2D(200, 200);
        encoded.SetPixels32(img);
        encoded.Apply();
        return encoded;
    }
}
