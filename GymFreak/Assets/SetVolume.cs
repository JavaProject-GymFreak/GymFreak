using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetVolume : MonoBehaviour
{
    public Slider slider;
    public AudioSource au;
    public TMP_Text text;

    public void SetLevel(float sliderValue)
    {
        au.volume = sliderValue;
        text.text = (int)(sliderValue * 100) + "";
    }
}
