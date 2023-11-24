using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    GameData gameData;
    FoodStorage foodStorage;

    [SerializeField] private GameObject[] gameObjectByCharacter;
    [SerializeField] private GameObject[] backgrounds;

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

    public bool ChangeActivity(ActivityType activityType)
    {
        if (gameData.currentActivityType == activityType) return false;

        switch (activityType)
        {
            case ActivityType.Exercise:
                curCharacter.SetActive(true);

                for (int i = 0; i < curCharacter.transform.childCount; i++)
                {
                    GameObject o = curCharacter.transform.GetChild(i).gameObject;
                    if (o == curExcercisingCharacter)
                    {
                        o.SetActive(true);
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
                break;

            case ActivityType.Eat:
                curCharacter.SetActive(false);
                ActiveBackgroundByNum(2);

                foodStorage.ShowFoods();
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

        curExcercisingCharacterAnim = curExcercisingCharacter.GetComponent<Animator>();
    }

    public void SetAnimSpeed(int speed)
    {
        if (gameData.pause)
            return;

        curExcercisingCharacterAnim.speed = speed;
    }
}
