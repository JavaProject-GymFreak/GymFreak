using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NestedScrollManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Scrollbar scrollbar;
    public Scrollbar scrollbarVer;

    const int SIZE = 5;
    float[] pos = new float[SIZE];
    float distance, targetPos;
    bool isDrag;

    private void Awake()
    {
        distance = 1f / (SIZE - 1);
        for (int i = 0; i < SIZE; i++) pos[i] = distance * i;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData) => isDrag = true;

    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;

        for(int i = 0; i < SIZE; i++)
        {
            if(scrollbar.value < pos[i] + distance * 0.5f && scrollbar.value > pos[i] - distance * 0.5f)
            {
                targetPos = pos[i];
                
            }
        }
    }

    private void Update()
    {
        if (!isDrag)
        {
            if (targetPos != pos[1])
            {
                scrollbarVer.value = Mathf.Lerp(scrollbarVer.value, 1, 0.1f);
            }
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetPos, 0.1f);
        }
    }
}
