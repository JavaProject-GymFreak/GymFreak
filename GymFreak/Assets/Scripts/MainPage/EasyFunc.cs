using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyFunc : MonoBehaviour
{
    public Color HexToColor(string hex)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
}
