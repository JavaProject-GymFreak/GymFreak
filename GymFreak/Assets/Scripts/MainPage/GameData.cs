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

    [SerializeField] private Slider sliderFatigue;
    [SerializeField] private Image fillImageFatigue;
    [SerializeField] private TextMeshProUGUI fatigueText;

    [SerializeField] private Color safeFatigueColor;
    [SerializeField] private Color dagerFatigueColor;

    [Header("�ð�")]
    public bool pause;
    public DateTime currentDate; // ��¥

    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TextMeshProUGUI timeText;

    public int speedLevel;
    [SerializeField] private TextMeshProUGUI speedText;

    private int timeRatio; // ���� �� �ӵ��� ���Ǻ��� �� �� ������ �귯������ ( �⺻������ 1�ʰ� 1�� )

    [Header("Ȱ��")]
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

        fillImageFatigue.color = fatigue >= 80 ? dagerFatigueColor : safeFatigueColor; // �Ƿε��� 80 ������ ������ �ƴϸ� �����
    }

    private void SetDate()
    {
        string curDateText = currentDate.Year + "��" +
                             currentDate.Month + "��" + currentDate.Day + "��";
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