using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public enum ActivityType
{
    Exercise,
    Rest,
    Work,
    Eat,
}

public class GameData : MonoBehaviour
{
    [Header("플레이어 정보")]
    public string playerName; // 이름
    public int goalBig3; // 목표 3대 운동

    [Header("신체 정보")]
    public float currentMuscleMass; // 현재 골격근량
    public float currentFatMass; // 현재 체지방량

    [Header("돈")]
    public int money; // 돈

    [Header("피로도")]
    public float fatigue; // 피로도
    public Slider sliderFatigue;
    public Image fillImageFatigue;
    public TextMeshProUGUI fatigueText;

    [Header("시간")]
    public bool pause;
    public DateTime currentDate; // 날짜

    public TextMeshProUGUI dateText;
    public TextMeshProUGUI timeText;

    public int speedLevel;
    public TextMeshProUGUI speedText;

    [Header("활동")]
    public ActivityType currentActivityType;
    public GameObject panelActivity;

    private void Start()
    {
        speedLevel = 1;
        pause = false;

        currentDate = new DateTime(2023, 1, 1);
        SetDate();

        fatigue = 0;
    }

    public void Update()
    {
        if (pause)
            return;

        currentDate = currentDate.AddMinutes(Time.deltaTime * speedLevel * 4);

        SetDate();
        speedText.text = "x" + speedLevel;


        if (currentActivityType == ActivityType.Exercise)
        {
            fatigue += Time.deltaTime * 2.3f * speedLevel;
        }
        else if (currentActivityType == ActivityType.Rest)
        {
            fatigue += Time.deltaTime * 2.3f * speedLevel * -1;
        }

        fatigue = Mathf.Clamp(fatigue, 0f, 100f);

        fatigueText.text = Mathf.FloorToInt(fatigue) + "/100";
        sliderFatigue.value = fatigue / 100;

        Color color;
        ColorUtility.TryParseHtmlString(fatigue >= 80 ? "#FF3741" : "#FCFF58", out color);
        fillImageFatigue.color = color;
    }
    private void SetDate()
    {
        dateText.text = currentDate.Year + "년" +
                   currentDate.Month + "월" + currentDate.Day + "일";
        timeText.text = currentDate.Hour + " : " + currentDate.Minute;
    }
    public void LoadData()
    {

    }

    public void SaveData()
    {

    }
}