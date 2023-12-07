using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Data;
using UnityEngine.Networking;

public class GameData : MonoBehaviour
{
    #region Values

    FoodStorage foodStorage;

    [Header("�÷��̾� ����")]
    public Character character;

    [Header("��ü ����")]
    public float currentMuscleMass; // ���� ��ݱٷ�
    public float currentFatMass; // ���� ü���淮
    public float happiness; // ���� �ູ��

    [Header("��")]
    public int money; // ��
    [SerializeField] private TextMeshProUGUI moneyText;

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

    [SerializeField] private int timeRatio; // ���� �� �ӵ��� ���Ǻ��� �� �� ������ �귯������ ( �⺻������ 1�ʰ� 1�� )

    [Header("Ȱ��")]
    public ActivityType currentActivityType;
    public GameObject panelActivity;

    [Header("���")]
    public float totalHappy;
    public float totalMuscle;
    public float totalFat;

    public float excerciseTime;

    #endregion

    private void Awake()
    {
        foodStorage = GetComponent<FoodStorage>();
    }

    private void Start()
    {
        speedLevel = 1;
        pause = false;

        currentDate = new DateTime(2023, 1, 1, 12, 0, 0);
        SetDate();

        fatigue = 0;
    }

    public void Update()
    {
        ChangeValues();
    }
    private void ChangeValues()
    {
        ChangeSpeed();
        ChangeMoney();

        if (pause)
            return;

        ChangeTime();
        ChangeFatigue();
    }
    private void ChangeSpeed()
    {
        speedText.text = "x" + speedLevel;
    }

    private void ChangeTime()
    {
        if(currentActivityType == ActivityType.Sleep)
        {
            currentDate = currentDate.AddMinutes(Time.deltaTime * timeRatio * 10);
        }
        else
        {
            currentDate = currentDate.AddMinutes(Time.deltaTime * speedLevel * timeRatio);

            if(currentActivityType == ActivityType.Exercise)
                excerciseTime += Time.deltaTime * speedLevel * timeRatio;
        }

        SetDate();
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
    private void ChangeMoney()
    {
        moneyText.text = money.ToString("N0");
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
        SendData sendData = new SendData();
        sendData.character = character;
        sendData.currentMuscleMass = currentMuscleMass;
        sendData.currentFatMass = currentFatMass;
        sendData.happiness = happiness;
        sendData.money = money;
        sendData.currentDate = currentDate;
        sendData.itemList = foodStorage.GetInventoryItemState();


        string json = JsonUtility.ToJson(sendData);
        Debug.Log(json);
        //StartCoroutine(Upload("http://URL", json));
    }

    IEnumerator Upload(string URL, string json)
    {
        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(URL, json))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }

        }
    }
}

public class SendData
{
    public Character character;

    public float currentMuscleMass; // ���� ��ݱٷ�
    public float currentFatMass; // ���� ü���淮
    public float happiness; // ���� �ູ��

    public int money; // ��


    public DateTime currentDate; // ��¥

    public List<InventoryItem> itemList;
}

public enum ActivityType
{
    Exercise,
    Rest,
    Work,
    Eat,
    Sleep,
}

public enum Character
{
    potato,
    ma,
    sim,
    jinam,
    hee,
}