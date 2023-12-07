using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class SleepPanel : MonoBehaviour
{
    #region 변수

    [SerializeField]
    private ActivityManager activityManager;

    [SerializeField]
    private GameData gameData;

    [SerializeField]
    private Button wakeUpButton;

    [SerializeField]
    private TMP_Text M, F;

    #endregion

    public void OpenPanel(int sleepTime)
    {
        StartCoroutine(SleepCoroutine(sleepTime));
    }
    private IEnumerator SleepCoroutine(int sleepTime)
    {
        DateTime wakeUpTime = gameData.currentDate.AddHours(sleepTime);

        transform.GetChild(0).GetComponent<TMP_Text>().text = "기상시간 : " +
            gameData.currentDate.AddHours(sleepTime).ToString("yyyy년 MM월 dd일 HH:mm");

        activityManager.ChangeActivity(ActivityType.Sleep);
        gameData.panelActivity.SetActive(false);

        while (gameData.currentDate < wakeUpTime)
        {
            yield return null;
            
        }
        gameData.pause = true;
        wakeUpButton.gameObject.SetActive(true);
    }

    public void WakeUp()
    {
        activityManager.ChangeActivity(ActivityType.Rest);
        wakeUpButton.gameObject.SetActive(false);
        ChangeStats();

        gameData.SaveData();
    }

    private void ChangeStats()
    {
        int excerciseTime = (int)gameData.excerciseTime / 60;

        float muscleInc = gameData.totalMuscle;
        if (gameData.totalMuscle == 0f)
            muscleInc = -0.01f;
        else
        {
            switch (excerciseTime)
            {
                case 0:
                    muscleInc *= 0;
                    break;
                case 1:
                    muscleInc *= 0.5f;
                    break;
                case 2:
                    muscleInc *= 8f;
                    break;

                default:
                    muscleInc *= 1f;
                    break;
            }
        }
        M.color = gameData.currentMuscleMass < (gameData.currentMuscleMass + muscleInc) ? Color.green : Color.red;

        M.text = gameData.currentMuscleMass + " -> " + (gameData.currentMuscleMass + muscleInc);
        gameData.totalMuscle = 0;
        gameData.currentMuscleMass += muscleInc;


        float fatInc = gameData.totalFat;
        if (fatInc >= 0f)
        {
            switch (excerciseTime)
            {
                case 0:
                    fatInc *= 1;
                    break;
                case 1:
                    fatInc *= 0.8f;
                    break;
                case 2:
                    fatInc *= 0.4f;
                    break;

                default:
                    fatInc *= 0.1f;
                    break;
            }
        }

        F.color = gameData.currentFatMass < (gameData.currentFatMass + fatInc) ? Color.red : Color.green;
        F.text = gameData.currentFatMass + " -> " + (gameData.currentFatMass + fatInc);
        gameData.totalFat = 0;
        gameData.currentFatMass += fatInc;
    }
}
