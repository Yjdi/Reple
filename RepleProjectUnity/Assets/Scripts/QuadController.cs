using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadController : MonoBehaviour
{
    public static QuadController draggedItem;
    public static QuadController overlappedItem; // ���� ���콺 �Ʒ� �ִ� Quad
    public Vector3 originalPosition;

    private void OnMouseDown()
    {
        draggedItem = this; // �� �ڵ带 �߰�
        originalPosition = transform.position; // ���� ��ġ ����
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
        GetComponent<BoxCollider2D>().enabled = true; // ����� �̵�
    }

    void Update()
    {
        if (draggedItem == this)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = -Camera.main.transform.position.z;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = worldPos;

            // Raycasting�� ����Ͽ� �巡�� ���� Quad �Ʒ��� Quad ����
            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("Hit: " + hit.collider.gameObject.name);
                QuadController quadBelow = hit.collider.GetComponent<QuadController>();
                if (quadBelow && quadBelow != this) // �ڱ� �ڽ��� �ƴ� ��츸 overlappedItem���� ����
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
