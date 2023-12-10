using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ActivityManager : MonoBehaviour
{
    GameData gameData;
    FoodStorage foodStorage;

    [SerializeField] private GameObject[] gameObjectByCharacter;
    [SerializeField] private GameObject[] backgrounds;

    [SerializeField] private GameObject panel_paint;
    [SerializeField] private GameObject wakeUpButton;

    private GameObject curCharacter;
    private GameObject curExcercisingCharacter;

    private Animator curExcercisingCharacterAnim;

    public GameObject CurCharacter => curCharacter;
    public GameObject CurExcercisingCharacter => curExcercisingCharacter;


    private static int eCount;

    private void Awake()
    {
        gameData = GetComponent<GameData>();
        foodStorage = GetComponent<FoodStorage>();

        curCharacter = gameObjectByCharacter[(int)gameData.character];
        SetCurExcercisingCharacter();
    }
    private IEnumerator FaintCoroutine()
    {
        DateTime wakeUpTime = gameData.currentDate.AddHours(16);

        panel_paint.transform.GetChild(0).GetComponent<TMP_Text>().text = "기상시간 : " +
            gameData.currentDate.AddHours(16).ToString("yyyy년 MM월 dd일 HH:mm");

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
        ChangeActivity(ActivityType.Rest);
        wakeUpButton.gameObject.SetActive(false);
    }

    public bool ChangeActivity(ActivityType activityType)
    {
        if (gameData.currentActivityType == activityType) return false;

        switch (activityType)
        {
            case ActivityType.Faint:

                StartCoroutine(FaintCoroutine());
                curCharacter.SetActive(false);
                ActiveBackgroundByNum(3);
                panel_paint.SetActive(true);
                gameData.pause = false;
                break;
            case ActivityType.Exercise:
                curCharacter.SetActive(true);

                for (int i = 0; i < curCharacter.transform.childCount; i++)
                {
                    GameObject o = curCharacter.transform.GetChild(i).gameObject;
                    if (o == curExcercisingCharacter)
                    {
                        o.SetActive(true);
                        int num = (int)gameData.currentMuscleMass / 10;
                        num--;
                        for (int j = 0; j < 3; j++)
                        {
                            o.transform.GetChild(j).gameObject.SetActive(j== num);
                        }
                    }
                    else
                    {
                        o.SetActive(false);
                    }
                }

                ActiveBackgroundByNum(0);
                break;

            case ActivityType.Rest:

                curCharacter.SetActive(true);
                curExcercisingCharacter.SetActive(false);

                SetCurExcercisingCharacter();

                curCharacter.transform.GetChild(3).gameObject.SetActive(true);

                ActiveBackgroundByNum(1);
                break;

            case ActivityType.Work:
                curCharacter.SetActive(false);
                ActiveBackgroundByNum(2);
                break;

            case ActivityType.Sleep:
                curCharacter.SetActive(false);
                ActiveBackgroundByNum(3);

                gameData.pause = false;
                break;

            default:
                break;
        }

        gameData.currentActivityType = activityType;
        return true;
    }

    private void ActiveBackgroundByNum(int num)
    {
        for(int i= 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].SetActive(i == num);
        }
    }

    public void SetCurExcercisingCharacter()
    {
        if (eCount >= 3) eCount = 0;

        curExcercisingCharacter = curCharacter.transform.GetChild(eCount++).gameObject;
        int num = (int)gameData.currentMuscleMass / 10;
        num--;

        curExcercisingCharacterAnim = curExcercisingCharacter.transform.GetChild(num).GetComponent<Animator>();
    }

    public void SetAnimSpeed(float speed)
    {
        if (gameData.pause)
            return;

        curExcercisingCharacterAnim.speed = speed;
    }
}
