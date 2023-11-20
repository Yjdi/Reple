using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardArranger : MonoBehaviour
{
    public GameObject[] cardObjects; // 카드 오브젝트를 담을 배열
    private Vector3[] initialPositions; // 카드의 초기 위치를 저장할 배열

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
        // 카드 순서를 랜덤하게 섞기
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
        // 섞인 카드를 원래 위치에 배치
        for (int i = 0; i < cardObjects.Length; i++)
        {
            cardObjects[i].transform.position = initialPositions[i];
        }
    }
}
