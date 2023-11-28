using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Object/Inventory")]
public class InventorySO : ScriptableObject
{
    [SerializeField]
    private List<InventoryItem> inventoryItems;
    
    public List<InventoryItem> GetCurrentInventoryState()
    {
        return inventoryItems;
    }
    
    public void BuyFoodByFoodSO(FoodSO foodSO, int amount)
    {
        for(int i = 0; i < inventoryItems.Count; i++)
        {
            if(inventoryItems[i].item.ID == foodSO.ID)
            {
                inventoryItems[i].ChangeQuantity(inventoryItems[i].quantity + amount);
            }
        }
    }
    public int GetQuantityByFoodSO(FoodSO foodSO)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].item.ID == foodSO.ID)
            {
                return inventoryItems[i].quantity;
            }
        }
        return -1;
    }

    public int GetPriceByFoodSO(FoodSO foodSO)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].item.ID == foodSO.ID)
            {
                return inventoryItems[i].item.Price;
            }
        }
        return -1;
    }

    public bool CanBuy(FoodSO foodSO, int amount)
    {
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].quantity + amount > 99 && 
                foodSO.ID == inventoryItems[i].item.ID)
            {
                return false;
            }
        }
        return true;
    }
}

[Serializable]
public class InventoryItem
{
    public int quantity;
    public FoodSO item;

    public bool IsEmpty => quantity == 0;

    public void ChangeQuantity(int nquantity)
    {
        quantity = nquantity;
    }
}
