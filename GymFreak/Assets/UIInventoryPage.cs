using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryPage : MonoBehaviour
{
    [SerializeField]
    private Blank[] blanks;

    public void SetUI(List<InventoryItem> items, int index)
    {
        blanks[0].blankObject.SetActive(index != 0);
        blanks[2].blankObject.SetActive(index < items.Count-1);

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
    }
}

[System.Serializable]
public struct Blank
{
    public GameObject blankObject;
    public Image image;
    public TextMeshProUGUI amount;
}
