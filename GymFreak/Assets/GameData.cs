using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

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
    public int fatigue; // 피로도

    [Header("시간")]
    public bool pause;
    public DateTime currentDate; // 날짜

    public TextMeshProUGUI dateText;
    public TextMeshProUGUI timeText;

    public int speedLevel;
    public TextMeshProUGUI speedText;

    private void Start()
    {
        speedLevel = 1;
        pause = false;

        currentDate = new DateTime(2023, 1, 1);
        SetDate();
    }

    public void Update()
    {
        if(!pause)
            currentDate = currentDate.AddMinutes(Time.deltaTime * speedLevel);
        SetDate();

        speedText.text = "x" + speedLevel;
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