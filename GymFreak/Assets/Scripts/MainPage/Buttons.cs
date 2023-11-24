using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    GameData gameData;
    ActivityManager activityManager;

    private void Awake()
    {
        gameData = GetComponent<GameData>();
        activityManager = GetComponent<ActivityManager>();
    }

    public void ChangeSpeed()
    {
        gameData.speedLevel *= 2;
        activityManager.SetAnimSpeed(gameData.speedLevel);

        if(gameData.speedLevel > 4)
        {
            gameData.speedLevel = 1;
            activityManager.SetAnimSpeed(gameData.speedLevel);
        }
    }

    public void Pause()
    {
        activityManager.SetAnimSpeed(0);
        gameData.pause = true;
    }
    public void Resume()
    {
        gameData.pause = false;
        activityManager.SetAnimSpeed(gameData.speedLevel);
    }

    public void ChangeActivityToExercise()
    {
        if (gameData.currentActivityType == ActivityType.Exercise)
            return;

        if (activityManager.ChangeActivity(ActivityType.Exercise))
        {
            ClosePanelActivity();
        }
        
    }
    public void ChangeActivityToRest()
    {
        if (gameData.currentActivityType == ActivityType.Rest)
            return;

        if (activityManager.ChangeActivity(ActivityType.Rest))
        {
            ClosePanelActivity();
        }
    }
    public void ChangeActivityToWork()
    {
        if (gameData.currentActivityType == ActivityType.Work)
            return;

        if (activityManager.ChangeActivity(ActivityType.Work))
        {
            ClosePanelActivity();
        }
    }
    public void ChanveActivityToSleep()
    {

    }
    public void ChangeActivityToEat()
    {
        if (gameData.currentActivityType == ActivityType.Eat)
            return;

        if (activityManager.ChangeActivity(ActivityType.Eat))
        {
            ClosePanelActivity();
        }
    }

    private void ClosePanelActivity()
    {
        Resume();
        gameData.panelActivity.SetActive(false);
    }
}
