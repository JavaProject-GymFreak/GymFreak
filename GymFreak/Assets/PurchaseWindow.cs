using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PurchaseWindow : MonoBehaviour
{
    #region ����

    [SerializeField]
    private FoodStorage foodStorage;

    [SerializeField]
    private FoodShopWindow foodShopWindow; //�̰� �ϱػ���

    [SerializeField]
    private Slider slider_amount;

    [SerializeField]
    private TMP_Text text_amount;

    [SerializeField]
    private Image image_food;

    private FoodSO foodSO;

    #endregion

    //�� ������ ������ ������ �� �ǽð����� �ٲ�� �����
    //�� ������ �� �� ������(��ư ��Ȱ��ȭ)
    //�����ϸ� ���� �޽���?


    private void OnEnable()
    {
        text_amount.text = (int)slider_amount.value + "";
        slider_amount.onValueChanged.AddListener(UpdateText);
    }

    private void OnDisable()
    {
        foodShopWindow.OpenFoodShopWindow(); // �ϱػ�ƾƾ�~~
    }

    void UpdateText(float val)
    {
        text_amount.text = (int)val + "";
    }

    public void OpenWindow(FoodSO foodSO)
    {
        gameObject.SetActive(true);
        this.foodSO = foodSO;
        image_food.sprite = foodSO.ItemImage;

        slider_amount.maxValue = foodStorage.AvailableFoodQuantity(foodSO);
        slider_amount.value = 0;
    }

    public void PurchaseFood()
    {
        if (foodStorage.BuyFood(foodSO, (int)slider_amount.value))
        {
            gameObject.SetActive(false);
        }
    }
}
