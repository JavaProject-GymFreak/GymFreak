using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SleepWindow : MonoBehaviour
{
    #region º¯¼ö

    [SerializeField]
    private ActivityManager activityManager;

    [SerializeField]
    private GameData gameData;

    [SerializeField]
    private TMP_Text text_sleepTime, text_changeTime;

    [SerializeField]
    private Button button_prev, button_next;

    [SerializeField]
    private Image panel_sleep;

    private int sleepTime;

    #endregion

    private void OnEnable()
    {
        sleepTime = 5;
        SetWindow();
    }

    public void AddTime()
    {
        sleepTime++;
        button_prev.interactable = true;
        if(sleepTime >= 12)
        {
            button_next.interactable = false;
            sleepTime = 12;
        }
        SetWindow();
    }

    public void SubTime()
    {
        sleepTime--;
        button_next.interactable = true;
        if (sleepTime <= 5)
        {
            button_prev.interactable = false;
            sleepTime = 5;
        }
        SetWindow();
    }

    private void SetWindow()
    {
        text_sleepTime.text = sleepTime + "";
        text_changeTime.text = gameData.currentDate.ToString("HH:mm") + " -> " + 
                               gameData.currentDate.AddHours(sleepTime).ToString("HH:mm");
    }

    public void Sleep()
    {
        gameObject.SetActive(false);
        panel_sleep.gameObject.SetActive(true);
        panel_sleep.GetComponent<SleepPanel>().OpenPanel(sleepTime);
    }

}
