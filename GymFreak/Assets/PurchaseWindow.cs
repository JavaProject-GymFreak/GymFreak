using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseWindow : MonoBehaviour
{
    #region ����

    [SerializeField]
    private GameData gameData;

    [SerializeField]
    private FoodStorage foodStorage;

    [SerializeField]
    private FoodShopWindow foodShopWindow; //�̰� �ϱػ���

    [SerializeField]
    private Slider slider_amount;

    [SerializeField]
    private TMP_Text text_amount, text_moneyChange, text_quantity;

    [SerializeField]
    private Image image_food;

    private FoodSO foodSO;

    #endregion

    //�� ������ ������ ������ �� �ǽð����� �ٲ�� �����
    //�� ������ �� �� ������(��ư ��Ȱ��ȭ)
    //�����ϸ� ���� �޽���?

    private void OnEnable()
    {
        slider_amount.onValueChanged.AddListener(UpdateText);
    }

    private void OnDisable()
    {
        foodShopWindow.OpenFoodShopWindow(); // �ϱػ�ƾƾ�~~
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
