using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Object/Food")]
public class FoodSO : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; set; }

    public int ID => GetInstanceID();

    [field: SerializeField]
    [field: TextArea]
    public string Description { get; set; }

    [field: SerializeField]
    public Sprite ItemImage { get; set; }

    [field: SerializeField]
    public int Price { get; set; }

    [field: SerializeField]
    public float HappinessIncrease { get; set; } // 최대 100 한번 먹을때마다 맛없는거 대충 -10 맛있는거 40

    [field: SerializeField]
    public float MuscleIncrease { get; set; } // 한달에 3 하루에 0.03

    [field: SerializeField]
    public float FatIncrease { get; set; } // 맛있는거 0.1  맛없는거 -0.05
}
