using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    

public class FoodStorage : MonoBehaviour
{
    GameData gameData;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private UIInventoryPage inventoryUI;

    int currentFoodIndex;

    private void Awake()
    {
        gameData = GetComponent<GameData>();
    }

    public List<InventoryItem> GetInventoryItemState()
    {
        return inventoryData.GetCurrentInventoryState();
    }
    public List<InventoryItem> GetInventoryItemStateOnlyAvailable()
    {
        List<InventoryItem> inventoryItems = inventoryData.GetCurrentInventoryState();
        for(int i =0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].quantity == 0)
            {
                inventoryItems.RemoveAt(i);
            }
        }
        return inventoryItems;
    }

    public int GetItemCount()
    {
        return inventoryData.GetCurrentInventoryState().Count;
    }

    public int GetFoodQuantityByFoodSO(FoodSO foodSO)
    {
        return inventoryData.GetQuantityByFoodSO(foodSO);
    }

    public int AvailableFoodQuantity(FoodSO foodSO)
    {
        int maxAvailableQuantity = 
            gameData.money / inventoryData.GetPriceByFoodSO(foodSO);
        return maxAvailableQuantity;
    }

    public bool CanBuy(FoodSO foodSO)
    {
        return gameData.money >= foodSO.Price && inventoryData.CanBuy(foodSO, 1);
    }

    public bool BuyFood(FoodSO foodSO, int amount)
    {
        if(gameData.money >= foodSO.Price * amount && inventoryData.CanBuy(foodSO, amount))
        {
            gameData.money -= foodSO.Price * amount;
            inventoryData.BuyFoodByFoodSO(foodSO, amount);
            return true;
        }
        return false;
    }

    public void ShowFoods()
    {
        currentFoodIndex = 0;
        inventoryUI.SetUI(GetInventoryItemStateOnlyAvailable(), 0);
    }

    public void NextFood()
    {
        if (currentFoodIndex >= inventoryData.GetCurrentInventoryState().Count-1)
            return;

        inventoryUI.SetUI(GetInventoryItemStateOnlyAvailable(), ++currentFoodIndex);
    }

    public void PrevFood()
    {
        if (currentFoodIndex == 0)
            return;

        inventoryUI.SetUI(GetInventoryItemStateOnlyAvailable(), --currentFoodIndex);
    }
}
