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
    EasyFunc easyFunc;

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

    [SerializeField] private Slider sliderFatigue;
    [SerializeField] private Image fillImageFatigue;
    [SerializeField] private TextMeshProUGUI fatigueText;

    [SerializeField] private Color safeFatigueColor;
    [SerializeField] private Color dagerFatigueColor;

    [Header("시간")]
    public bool pause;
    public DateTime currentDate; // 날짜

    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TextMeshProUGUI timeText;

    public int speedLevel;
    [SerializeField] private TextMeshProUGUI speedText;

    private int timeRatio; // 게임 속 속도가 현실보다 몇 배 빠르게 흘러가는지 ( 기본적으로 1초가 1분 )

    [Header("활동")]
    public ActivityType currentActivityType;
    public GameObject panelActivity;

    private void Awake()
    {
        easyFunc = GetComponent<EasyFunc>();
    }

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

        ChangeValues();
    }
    private void ChangeValues()
    {
        ChangeTime();
        ChangeFatigue();
    }

    private void ChangeTime()
    {
        currentDate = currentDate.AddMinutes(Time.deltaTime * speedLevel * timeRatio);

        SetDate();
        speedText.text = "x" + speedLevel;
    }
    private void ChangeFatigue()
    {
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

        fillImageFatigue.color = fatigue >= 80 ? dagerFatigueColor : safeFatigueColor; // 피로도가 80 넘으면 빨간색 아니면 노란색
    }

    private void SetDate()
    {
        string curDateText = currentDate.Year + "년" +
                             currentDate.Month + "월" + currentDate.Day + "일";
        dateText.text = curDateText;

        string curTimeText = currentDate.Hour + " : " + currentDate.Minute;
        timeText.text = curTimeText;
    }

    public void LoadData()
    {

    }
    public void SaveData()
    {

    }
}