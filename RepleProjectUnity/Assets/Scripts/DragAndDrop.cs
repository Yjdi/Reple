using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Vector2 originalPosition;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / (rectTransform.parent as RectTransform).localScale.x;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = originalPosition;
    }

    public void OnDrop(PointerEventData eventData)
    {
        DragAndDrop draggedItem = eventData.pointerDrag.GetComponent<DragAndDrop>();
        if (draggedItem != null)
        {
            // �巡�׵� �����۰� ��ӵ� �������� ��ġ�� �����մϴ�.
            Vector2 tempPosition = draggedItem.rectTransform.anchoredPosition;
            draggedItem.rectTransform.anchoredPosition = rectTransform.anchoredPosition;
            rectTransform.anchoredPosition = tempPosition;
        }
    }
}
