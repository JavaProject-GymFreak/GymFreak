using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bgm, click;

    public void StopBgm()
    {
        bgm.Stop();
    }
    public void PlayBgm()
    {
        bgm.Play();
    }
    public void StopClick()
    {
        click.Stop();
    }
    public void PlayClick()
    {
        click.Play();
    }
}
