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
    [Header("�÷��̾� ����")]
    public string playerName; // �̸�
    public int goalBig3; // ��ǥ 3�� �

    [Header("��ü ����")]
    public float currentMuscleMass; // ���� ��ݱٷ�
    public float currentFatMass; // ���� ü���淮

    [Header("��")]
    public int money; // ��

    [Header("�Ƿε�")]
    public float fatigue; // �Ƿε�
    public Slider sliderFatigue;
    public Image fillImageFatigue;
    public TextMeshProUGUI fatigueText;

    [Header("�ð�")]
    public bool pause;
    public DateTime currentDate; // ��¥

    public TextMeshProUGUI dateText;
    public TextMeshProUGUI timeText;

    public int speedLevel;
    public TextMeshProUGUI speedText;

    [Header("Ȱ��")]
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
        dateText.text = currentDate.Year + "��" +
                   currentDate.Month + "��" + currentDate.Day + "��";
        timeText.text = currentDate.Hour + " : " + currentDate.Minute;
    }
    public void LoadData()
    {

    }

    public void SaveData()
    {

    }
}