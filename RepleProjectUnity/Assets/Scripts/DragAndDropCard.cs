using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private Transform originalParent;
    public Transform[] answerAreas; // answer 영역들의 Transform
    private GameObject placeholder; // 드래그 중 임시 위치 표시 객체

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        originalParent = transform.parent;
        transform.SetParent(transform.root);

        if (placeholder != null)
        {
            Destroy(placeholder); // 기존 placeholder가 있으면 삭제
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
        transform.SetParent(originalParent); // 원래 부모로 돌아가도록 설정
        Destroy(placeholder); // placeholder 삭제
        placeholder = null; // placeholder를 null로 설정

        // 가장 가까운 answer 영역 찾기
        Transform closestAnswer = FindClosestAnswerArea();

        if (closestAnswer != null && closestAnswer.childCount == 0)
        {
            // 가장 가까운 answer 영역에 자식이 없으면 그 위치에 배치
            transform.position = closestAnswer.position;
            transform.SetParent(closestAnswer);
        }
        else if (closestAnswer != null && closestAnswer.childCount > 0)
        {
            // 가장 가까운 answer 영역에 이미 카드가 있으면 교체
            GameObject currentCard = closestAnswer.GetChild(0).gameObject;
            currentCard.transform.position = startPosition;
            currentCard.transform.SetParent(originalParent);

            transform.position = closestAnswer.position;
            transform.SetParent(closestAnswer);
        }
        else
        {
            // 가까운 answer 영역이 없으면 원래 위치로
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

        // 드롭할 수 있는 최대 거리 설정 (예: 50)
        if (closestDistance <= 50f)
        {
            return closestArea;
        }

        return null;
    }
}
