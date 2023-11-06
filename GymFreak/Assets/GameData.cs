using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

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
    public int fatigue; // �Ƿε�

    [Header("�ð�")]
    public bool pause;
    public DateTime currentDate; // ��¥

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