using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightRotate : MonoBehaviour
{
    public GameObject quad;
    public Button RightButton;
    private float currentAngle = 0; // ���� ������ �����ϴ� ����

    private void Start()
    {
        currentAngle = transform.eulerAngles.z;
        RightButton.onClick.AddListener(ToggleRotateRight);
    }

    public void ToggleRotateRight()
    {
        // ���������� 60�� ȸ��
        currentAngle += 60;
        quad.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
}
