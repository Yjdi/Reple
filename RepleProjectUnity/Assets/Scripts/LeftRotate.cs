using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftRotate : MonoBehaviour
{
    public GameObject quad;
    public Button LeftButton;
    private float currentAngle = 0; // ���� ������ �����ϴ� ����

    private void Start()
    {
        currentAngle = transform.eulerAngles.z;
        LeftButton.onClick.AddListener(ToggleRotateLeft);
    }

    public void ToggleRotateLeft()
    {
        // �������� 60�� ȸ��
        currentAngle -= 60;
        quad.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
