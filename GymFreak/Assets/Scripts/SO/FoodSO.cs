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
    public float HappinessIncrease { get; set; } // �ִ� 100 �ѹ� ���������� �����°� ���� -10 ���ִ°� 40

    [field: SerializeField]
    public float MuscleIncrease { get; set; } // �Ѵ޿� 3 �Ϸ翡 0.03

    [field: SerializeField]
    public float FatIncrease { get; set; } // ���ִ°� 0.1  �����°� -0.05
}
