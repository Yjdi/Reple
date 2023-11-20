using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardArranger : MonoBehaviour
{
    public GameObject[] cardObjects; // ī�� ������Ʈ�� ���� �迭
    private Vector3[] initialPositions; // ī���� �ʱ� ��ġ�� ������ �迭

    void Start()
    {
        SaveInitialPositions();
        ShuffleCards();
        ArrangeCards();
    }

    void SaveInitialPositions()
    {
        initialPositions = new Vector3[cardObjects.Length];
        for (int i = 0; i < cardObjects.Length; i++)
        {
            initialPositions[i] = cardObjects[i].transform.position;
        }
    }

    void ShuffleCards()
    {
        // ī�� ������ �����ϰ� ����
        for (int i = 0; i < cardObjects.Length; i++)
        {
            GameObject temp = cardObjects[i];
            int randomIndex = Random.Range(0, cardObjects.Length);
            cardObjects[i] = cardObjects[randomIndex];
            cardObjects[randomIndex] = temp;
        }
    }

    void ArrangeCards()
    {
        // ���� ī�带 ���� ��ġ�� ��ġ
        for (int i = 0; i < cardObjects.Length; i++)
        {
            cardObjects[i].transform.position = initialPositions[i];
        }
    }
}
