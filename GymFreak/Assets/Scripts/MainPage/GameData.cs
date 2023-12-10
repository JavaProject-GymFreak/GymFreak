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
    ActivityManager activityManager;

    [Header("플레이어 정보")]
    public Character character;
    [SerializeField] private TextMeshProUGUI dday_text;

    [Header("신체 정보")]
    public float currentMuscleMass; // 현재 골격근량
    public float currentFatMass; // 현재 체지방량
    public float happiness; // 현재 행복도
    [SerializeField] private TextMeshProUGUI currentMuscleMassText;
    [SerializeField] private TextMeshProUGUI currentFatMassText;
    [SerializeField] private TextMeshProUGUI happinessText;

    [Header("돈")]
    public int money; // 돈
    [SerializeField] private TextMeshProUGUI moneyText;

    [Header("피로도")]
    public float fatigue_cur;
    public float fatigue_max;

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

    public float speedLevel;
    [SerializeField] private TextMeshProUGUI speedText;

    [SerializeField] private int timeRatio; // 게임 속 속도가 현실보다 몇 배 빠르게 흘러가는지 ( 기본적으로 1초가 1분 )

    [Header("활동")]
    public ActivityType currentActivityType;
    public GameObject panelActivity;

    public DateTime lastEatTime;
    [SerializeField] private Button eatButton;

    [Header("기록")]
    public float totalHappy;
    public float totalMuscle;
    public float totalFat;

    public float excerciseTime;

    #endregion

    private void Awake()
    {
        activityManager = GetComponent<ActivityManager>();
        foodStorage = GetComponent<FoodStorage>();
    }

    private void Start()
    {
        speedLevel = 1;
        pause = false;

        currentDate = new DateTime(2023, 1, 1, 0, 0, 0);
        lastEatTime = new DateTime(2022, 1, 1, 0, 0, 0);
        SetDate();

        fatigue_cur = 0;
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

        ChangeStats();
        ChangeTime();
        ChangeFatigue();
        ChangeFoodTime();
    }
    private void ChangeFoodTime()
    {
        TimeSpan t = currentDate - lastEatTime;
        eatButton.interactable = t.TotalHours > 3;
    }
    private void ChangeSpeed()
    {
        speedText.text = "x" + speedLevel;
    }
    private void ChangeStats()
    {
        currentMuscleMassText.text = currentMuscleMass + "kg";
        currentFatMassText.text = currentFatMass + "kg";
        //happinessText.text = happiness + "%";
    }
    private void ChangeTime()
    {
        
        if (currentActivityType == ActivityType.Sleep || currentActivityType == ActivityType.Faint)
        {
            currentDate = currentDate.AddMinutes(Time.deltaTime * timeRatio * 15);
        }
        else
        {
            currentDate = currentDate.AddMinutes(Time.deltaTime * speedLevel * timeRatio);

            if(currentActivityType == ActivityType.Exercise)
                excerciseTime += Time.deltaTime * speedLevel * timeRatio;
        }

        SetDate();
    }
    float timer, timerr;
    private void ChangeFatigue()
    {
        
        if (currentActivityType == ActivityType.Exercise)
        {
            timer += Time.deltaTime;
            if (timer >= 4 - speedLevel)// 3 -> 1 , 2 ->2 , 1 -> 3
            {
                timer = 0f;
                fatigue_cur += UnityEngine.Random.Range(3f ,5f);
                if(fatigue_cur >= fatigue_max)
                {
                    activityManager.ChangeActivity(ActivityType.Faint);
                }
            }
        }
        else if (currentActivityType == ActivityType.Rest)
        {
            timer += Time.deltaTime;
            if (timer >= 4 - speedLevel)// 3 -> 1 , 2 ->2 , 1 -> 3
            {
                timer = 0f;
                fatigue_cur -= UnityEngine.Random.Range(3f, 5f);
            }
        }
        else if (currentActivityType == ActivityType.Faint || currentActivityType == ActivityType.Sleep)
        {
            fatigue_cur -= Time.deltaTime * 3;
        }

        fatigue_cur = Mathf.Max(fatigue_cur, 0f);

        fatigueText.text = Mathf.FloorToInt(fatigue_cur) + "/" + Mathf.FloorToInt(fatigue_max);
        sliderFatigue.value = Mathf.Lerp(sliderFatigue.value, fatigue_cur / fatigue_max, 0.2f);

        fillImageFatigue.color = fatigue_cur / fatigue_max >= 0.8f ? dagerFatigueColor : safeFatigueColor; // 피로도가 80 넘으면 빨간색 아니면 노란색
    }
    private void ChangeMoney()
    {
        if (currentActivityType == ActivityType.Work)
        {
            timerr += Time.deltaTime * speedLevel;
            
            if(timerr >= 12)
            {
                timerr = 0;

                money += 10000;
            }
        }
        moneyText.text = money.ToString("N0");
    }

    private void SetDate()
    {
        string curDateText = currentDate.Month + "월" + currentDate.Day + "일";
        dateText.text = curDateText;

        string curTimeText = currentDate.Hour + " : " + currentDate.Minute;
        timeText.text = curTimeText;

        DateTime specificDate = new DateTime(2023, 1, 1, 0, 0, 0);
        TimeSpan diff = currentDate - specificDate;
        dday_text.text = "D - " + (diff.Days +1);
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
        sendData.fatigue_cur = fatigue_cur;
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

    public float currentMuscleMass; // 현재 골격근량
    public float currentFatMass; // 현재 체지방량
    public float happiness; // 현재 행복도

    public int money; // 돈

    public float fatigue_cur; // 피로도

    public DateTime currentDate; // 날짜

    public List<InventoryItem> itemList;
}

public enum ActivityType
{
    Exercise,
    Rest,
    Work,
    Eat,
    Sleep,
    Faint,
}

public enum Character
{
    potato,
    ma,
    sim,
    jinam,
    hee,
}