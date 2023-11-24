using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FoodShopWindow : MonoBehaviour
{
    #region ����

    [SerializeField]
    private GameData gameData;

    [SerializeField]
    private FoodStorage foodStorage;

    [SerializeField]
    private PurchaseWindow purchaseWindow;

    [SerializeField]
    private TMP_Text current_money;

    [SerializeField]
    private GameObject scrollView_content;

    [SerializeField]
    private GameObject foodTemplate;

    #endregion

    private void OnEnable()
    {
        OpenFoodShopWindow();
    }

    public void OpenFoodShopWindow()
    {
        gameObject.SetActive(true);

        scrollView_content.transform.DetachChildren();

        current_money.text = "�� : " + gameData.money;

        Transform g;
        foreach (var food in foodStorage.GetInventoryItemState())
        {
            g = Instantiate(foodTemplate, scrollView_content.transform).transform;
            g.GetChild(0).GetComponent<Image>().sprite = food.item.ItemImage;
            g.GetChild(1).GetComponent<TMP_Text>().text = "���� : " + food.item.Price;
            g.GetChild(2).GetComponent<TMP_Text>().text = food.item.Name;
            g.GetChild(3).GetComponent<TMP_Text>().text = food.item.Description;
            g.GetChild(4).GetComponent<TMP_Text>().text = "�������� : " + food.quantity;
            g.GetChild(5).GetComponent<Button>().onClick.AddListener(() => purchaseWindow.OpenWindow(food.item));
        }
    }
}