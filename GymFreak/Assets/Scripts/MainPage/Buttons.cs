using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    GameData gameData;

    private void Awake()
    {
        gameData = GetComponent<GameData>();
    }

    public void ChangeSpeed()
    {
        gameData.speedLevel *= 2;
        if(gameData.speedLevel > 4)
        {
            gameData.speedLevel = 1;
        }
    }

    public void Pause()
    {
        gameData.pause = true;
    }
    public void Resume()
    {
        gameData.pause = false;
    }

    public void ChangeActivityToExercise()
    {
        if (gameData.currentActivityType == ActivityType.Exercise)
            return;

        gameData.currentActivityType = ActivityType.Exercise;
        ClosePanelActivity();
    }
    public void ChangeActivityToRest()
    {
        if (gameData.currentActivityType == ActivityType.Rest)
            return;

        gameData.currentActivityType = ActivityType.Rest;
        ClosePanelActivity();
    }
    public void ChangeActivityToWork()
    {
        if (gameData.currentActivityType == ActivityType.Work)
            return;

        gameData.currentActivityType = ActivityType.Work;
        ClosePanelActivity();
    }

    private void ClosePanelActivity()
    {
        gameData.panelActivity.SetActive(false);
    }
}