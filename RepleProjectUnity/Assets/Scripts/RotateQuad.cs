using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateQuad : MonoBehaviour
{
    float angle;
    Vector2 target, mouse;
    bool isDragging = false; // 쿼드가 드래그되고 있는지 확인하기 위한 변수

    private void Start()
    {
        target = transform.position;
    }

    private void Update()
    {
        // 왼쪽 마우스 버튼이 눌렸는지 확인
        if (Input.GetMouseButtonDown(0))
        {
            // 마우스 위치를 월드 좌표로 변환
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // 마우스가 쿼드 위에 있는지 확인
            if (GetComponent<Collider2D>().OverlapPoint(mousePos))
            {
                isDragging = true;
            }
        }

        // 마우스 버튼이 떼어지면 드래깅 중단
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // 쿼드가 드래그되고 있으면 회전
        if (isDragging)
        {
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angle = Mathf.Atan2(mouse.y - target.y, mouse.x - target.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
