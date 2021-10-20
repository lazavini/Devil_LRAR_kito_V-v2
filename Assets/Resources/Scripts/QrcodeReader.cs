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

public class QrcodeReader : MonoBehaviour
{
    public bool Reading { get; set; }
    public Button ButtonStartReading;
    private bool _isFrameSet = false;
    QRCodeReader _reader;
    void Start()
    {
        _reader = new QRCodeReader();
        ButtonStartReading?.onClick.AddListener(StartReading);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Reading)
            return;

        if (!_isFrameSet)
        {
            try
            {
                _isFrameSet = CameraDevice.Instance.SetFrameFormat(PIXEL_FORMAT.GRAYSCALE, true);
            }
            catch (Exception) { }
        }
        
        try
        {
            var cameraFeed = CameraDevice.Instance.GetCameraImage(PIXEL_FORMAT.GRAYSCALE);
            var source = new RGBLuminanceSource(cameraFeed.Pixels, cameraFeed.Width, cameraFeed.Height, RGBLuminanceSource.BitmapFormat.Gray8);
            


            
            var binarizer = new HybridBinarizer(source);
            var binBitmap = new BinaryBitmap(binarizer);
            var result = _reader.decode(binBitmap);
            if (result != null)
            {
                var resultTex = result.Text;
                var playerStates = JsonConvert.DeserializeObject<List<PlayerState>>((string)resultTex);
                CardMixer.LoadFromJson((string)resultTex);
                Reading = false;
                var pauseMenu = gameObject.GetComponentInChildren<PauseMenu>(true);
                pauseMenu.Resume();
            }
        }
        catch (Exception) { }
    }

    public void StartReading()
    {
        Reading = true;
    }

}
