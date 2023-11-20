using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform originalParent;
    public Transform[] answerAreas; // answer �������� Transform
    private GameObject placeholder; // �巡�� �� �ӽ� ��ġ ǥ�� ��ü

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        originalParent = transform.parent;
        transform.SetParent(transform.root);

        if (placeholder != null)
        {
            Destroy(placeholder); // ���� placeholder�� ������ ����
        }
        placeholder = new GameObject("Placeholder");
        placeholder.transform.position = startPosition;
        placeholder.transform.SetParent(originalParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(originalParent); // ���� �θ�� ���ư����� ����
        Destroy(placeholder); // placeholder ����
        placeholder = null; // placeholder�� null�� ����

        // ���� ����� answer ���� ã��
        Transform closestAnswer = FindClosestAnswerArea();

        if (closestAnswer != null && closestAnswer.childCount == 0)
        {
            // ���� ����� answer ������ �ڽ��� ������ �� ��ġ�� ��ġ
            transform.position = closestAnswer.position;
            transform.SetParent(closestAnswer);
        }
        else if (closestAnswer != null && closestAnswer.childCount > 0)
        {
            // ���� ����� answer ������ �̹� ī�尡 ������ ��ü
            GameObject currentCard = closestAnswer.GetChild(0).gameObject;
            currentCard.transform.position = startPosition;
            currentCard.transform.SetParent(originalParent);

            transform.position = closestAnswer.position;
            transform.SetParent(closestAnswer);
        }
        else
        {
            // ����� answer ������ ������ ���� ��ġ��
            transform.position = startPosition;
        }
    }

    Transform FindClosestAnswerArea()
    {
        float closestDistance = float.MaxValue;
        Transform closestArea = null;

        foreach (var area in answerAreas)
        {
            float distance = Vector3.Distance(transform.position, area.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestArea = area;
            }
        }

        // ����� �� �ִ� �ִ� �Ÿ� ���� (��: 50)
        if (closestDistance <= 50f)
        {
            return closestArea;
        }

        return null;
    }
}
