using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeOver : MonoBehaviour
{
    public GameObject[] Audience_L;
    public GameObject[] Audience_R;
    public float moveDistance = 5.0f;
    public float moveDuration = 2.0f;

    private float delayBetweenMoves = 10.0f;
    void Start()
    {
        StartCoroutine(SequentialMoveCoroutine());
    }
    IEnumerator SequentialMoveCoroutine()
    {
        yield return new WaitForSeconds(5f);
        yield return new WaitForSeconds(delayBetweenMoves);

        for (int i = 0; i < Audience_L.Length; i++)
        {
            StartCoroutine(MoveObject(Audience_L[i], Vector3.left * moveDistance, moveDuration));
            StartCoroutine(MoveObject(Audience_R[i], Vector3.right * moveDistance, moveDuration));

            if (i < Audience_L.Length - 1)
            {
                yield return new WaitForSeconds(delayBetweenMoves);
            }
        }
    }
    IEnumerator MoveObject(GameObject obj, Vector3 direction, float duration)
    {
        Vector3 startPosition = obj.transform.position;
        Vector3 endPosition = startPosition + direction;

        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            obj.transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / duration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = endPosition;
    }
}
