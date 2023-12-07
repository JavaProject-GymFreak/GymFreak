using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private GameData gameData;

    [SerializeField]
    private Blank[] blanks;

    [SerializeField]
    private TMP_Text happy, muscle, fat;

    private FoodSO selected_food;

    public void SetUI(List<InventoryItem> items, int index)
    {
        blanks[0].blankObject.SetActive(index != 0);
        blanks[2].blankObject.SetActive(index < items.Count-1);

        selected_food = items[index].item;

        blanks[1].image.sprite = items[index].item.ItemImage;
        blanks[1].amount.text = items[index].quantity + "";
        if (blanks[0].blankObject.activeSelf)
        {
            blanks[0].image.sprite = items[index - 1].item.ItemImage;
            blanks[0].amount.text = items[index - 1].quantity + "";
        }
        if (blanks[2].blankObject.activeSelf)
        {
            blanks[2].image.sprite = items[index + 1].item.ItemImage;
            blanks[2].amount.text = items[index + 1].quantity + "";
        }

        happy.color = items[index].item.HappinessIncrease > 0 ? Color.green : items[index].item.HappinessIncrease == 0 ? Color.grey : Color.red;
        muscle.color = items[index].item.MuscleIncrease > 0 ? Color.green : items[index].item.MuscleIncrease == 0 ? Color.grey : Color.red;
        fat.color = items[index].item.FatIncrease > 0 ? Color.green : items[index].item.FatIncrease == 0 ? Color.grey : Color.red;
        happy.text = "" + (items[index].item.HappinessIncrease >= 0 ? "+" + items[index].item.HappinessIncrease : items[index].item.HappinessIncrease);
        muscle.text = "" + (items[index].item.MuscleIncrease >= 0 ? "+" + items[index].item.MuscleIncrease : items[index].item.MuscleIncrease);
        fat.text = "" + (items[index].item.FatIncrease >= 0 ? "+" + items[index].item.FatIncrease : items[index].item.FatIncrease);
    }

    public void EatFood()
    {
        gameData.totalHappy += selected_food.HappinessIncrease;
        gameData.totalMuscle += selected_food.MuscleIncrease;
        gameData.totalFat += selected_food.FatIncrease;
    }
}

[System.Serializable]
public struct Blank
{
    public GameObject blankObject;
    public Image image;
    public TextMeshProUGUI amount;
}
