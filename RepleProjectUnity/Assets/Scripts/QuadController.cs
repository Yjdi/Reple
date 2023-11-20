using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadController : MonoBehaviour
{
    public static QuadController draggedItem;
    public static QuadController overlappedItem; // 현재 마우스 아래 있는 Quad
    public Vector3 originalPosition;

    private void OnMouseDown()
    {
        draggedItem = this; // 이 코드를 추가
        originalPosition = transform.position; // 원래 위치 저장
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnMouseUp()
    {
        if (draggedItem != null && overlappedItem != null)
        {
            Debug.Log($"Swapping {draggedItem.name} with {overlappedItem.name}");
            Swap(draggedItem, overlappedItem);
            draggedItem = null;
        }
        GetComponent<BoxCollider2D>().enabled = true; // 여기로 이동
    }

    void Update()
    {
        if (draggedItem == this)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = worldPos;

            // Raycasting을 사용하여 드래그 중인 Quad 아래의 Quad 감지
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
                QuadController quadBelow = hit.collider.GetComponent<QuadController>();
                if (quadBelow && quadBelow != this) // 자기 자신이 아닌 경우만 overlappedItem으로 설정
                {
                    overlappedItem = quadBelow;
                }
            }
        }
    }

    private void Swap(QuadController a, QuadController b)
    {
        Vector3 tempPosition = a.originalPosition;
        a.transform.position = b.originalPosition;
        b.transform.position = tempPosition;

        a.originalPosition = a.transform.position;
        b.originalPosition = b.transform.position;
    }
}
