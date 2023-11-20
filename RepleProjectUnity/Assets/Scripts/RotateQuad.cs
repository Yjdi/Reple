using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateQuad : MonoBehaviour
{
    float angle;
    Vector2 target, mouse;
    bool isDragging = false; // ���尡 �巡�׵ǰ� �ִ��� Ȯ���ϱ� ���� ����

    private void Start()
    {
        target = transform.position;
    }

    private void Update()
    {
        // ���� ���콺 ��ư�� ���ȴ��� Ȯ��
        if (Input.GetMouseButtonDown(0))
        {
            // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // ���콺�� ���� ���� �ִ��� Ȯ��
            if (GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                isDragging = true;
            }
        }

        // ���콺 ��ư�� �������� �巡�� �ߴ�
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // ���尡 �巡�׵ǰ� ������ ȸ��
        if (isDragging)
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
