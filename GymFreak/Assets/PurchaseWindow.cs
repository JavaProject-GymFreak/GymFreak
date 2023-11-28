using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseWindow : MonoBehaviour
{
    #region 변수

    [SerializeField]
    private GameData gameData;

    [SerializeField]
    private FoodStorage foodStorage;

    [SerializeField]
    private FoodShopWindow foodShopWindow; //이거 하극상임

    [SerializeField]
    private Slider slider_amount;

    [SerializeField]
    private TMP_Text text_amount, text_moneyChange, text_quantity;

    [SerializeField]
    private Image image_food;

    private FoodSO foodSO;

    #endregion

    //값 조절할 때마다 보유량 돈 실시간으로 바뀌게 만들기
    //돈 부족할 때 안 열리게(버튼 비활성화)
    //가능하면 에러 메시지?

    private void OnEnable()
    {
        slider_amount.onValueChanged.AddListener(UpdateText);
    }

    private void OnDisable()
    {
        foodShopWindow.OpenFoodShopWindow(); // 하극사아아앙~~
    }

    void UpdateText(float value)
    {
        int intValue = (int)value;

        text_amount.text = intValue + "";
        text_moneyChange.text = gameData.money + " -> " + (gameData.money - (foodSO.Price * intValue));
        text_quantity.text = foodStorage.GetFoodQuantityByFoodSO(foodSO) + " -> " + (foodStorage.GetFoodQuantityByFoodSO(foodSO) + intValue);
    }

    public void OpenWindow(FoodSO foodSO)
    {
        gameObject.SetActive(true);
        this.foodSO = foodSO;
        image_food.sprite = foodSO.ItemImage;

        slider_amount.maxValue = foodStorage.AvailableFoodQuantity(foodSO);
        slider_amount.value = 0;

        text_amount.text = (int)slider_amount.value + "";
        text_moneyChange.text = gameData.money + " -> " + (gameData.money - (foodSO.Price * (int)slider_amount.value));
        text_quantity.text = foodStorage.GetFoodQuantityByFoodSO(foodSO) + " -> " + (foodStorage.GetFoodQuantityByFoodSO(foodSO) + (int)slider_amount.value);
        text_amount.text = (int)slider_amount.value + "";
    }

    public void PurchaseFood()
    {
        if (foodStorage.BuyFood(foodSO, (int)slider_amount.value))
        {
            gameObject.SetActive(false);
        }
    }
}
