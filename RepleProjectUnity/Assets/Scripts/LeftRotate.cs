using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftRotate : MonoBehaviour
{
    public GameObject quad;
    public Button LeftButton;
    private float currentAngle = 0; // 현재 각도를 저장하는 변수

    private void Start()
    {
        currentAngle = transform.eulerAngles.z;
        LeftButton.onClick.AddListener(ToggleRotateLeft);
    }

    public void ToggleRotateLeft()
    {
        // 왼쪽으로 60도 회전
        currentAngle -= 60;
        quad.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
