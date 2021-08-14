using Assets.Vuforia.Scripts.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

public class QrcodeReader : MonoBehaviour
{
    public bool Reading { get; set; }
    public Button ButtonStartReading { get; set; }
    QRCodeReader _reader;
    void Start()
    {
        _reader = new QRCodeReader();
        ButtonStartReading.onClick.AddListener(StartReading);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Reading)
            return;

        var cameraFeed = CameraDevice.Instance.GetCameraImage(PIXEL_FORMAT.RGBA8888);
        var source = new RGBLuminanceSource(cameraFeed.Pixels, cameraFeed.Width, cameraFeed.Height, RGBLuminanceSource.BitmapFormat.RGBA32);
        var binarizer = new HybridBinarizer(source);
        var binBitmap = new BinaryBitmap(binarizer);
        var result = _reader.decode(binBitmap);
        if (result != null)
        {
            var resultado = result.Text;
            Reading = false;
        }
    }

    void StartReading()
    {
        Reading = true;
    }

}
