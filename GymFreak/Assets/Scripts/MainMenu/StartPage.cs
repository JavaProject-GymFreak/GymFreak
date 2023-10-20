using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartPage : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textTouchToStart;

    private void Start()
    {
        StartCoroutine("PadeIn");
    }

    private IEnumerator PadeIn()
    {
        yield return new WaitForSeconds(1f);

        Color textColor = textTouchToStart.color;

        while (textColor.a >= 0.5f)
        {
            textColor.a -= 0.08f;
            textTouchToStart.color = textColor;

            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine("PadeOut");
    }

    private IEnumerator PadeOut()
    {
        Color textColor = textTouchToStart.color;

        while (textColor.a <= 1f)
        {
            textColor.a += 0.08f;
            textTouchToStart.color = textColor;

            yield return new WaitForSeconds(0.1f);
        }

        StartCoroutine("PadeIn");
    }
}
