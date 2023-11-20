using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOverAndScore : MonoBehaviour
{
    public GameObject[] Audience_L;
    public GameObject[] Audience_R;
    public RectTransform GameOverImage; // GameOver 이미지에 대한 참조
    public float moveDistance = 5.0f;
    public float moveDuration = 2.0f;
    public float gameOverMoveDuration = 2.0f; // GameOver 이미지의 이동 속도 조절을 위한 변수

    private float delayBetweenMoves = 10.0f;

    public Text scoreText; // 점수를 표시할 Text 컴포넌트
    public RectTransform newScoreImage; // newScore 이미지에 대한 참조
    private int currentScore;
    private int highScore;

    void Start()
    {
        newScoreImage.gameObject.SetActive(false);
        // 이전 최고 점수를 로드
        highScore = PlayerPrefs.GetInt("HighScore");
        UpdateScoreText(); // 점수 Text 업데이트
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

        CheckForNewHighScore();
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

    IEnumerator MoveGameOverImage()
    {
        Vector2 startPosition = GameOverImage.anchoredPosition;
        Vector2 endPosition = new Vector2(0, 0);

        float elapsedTime = 0;
        while (elapsedTime < gameOverMoveDuration)
        {
            GameOverImage.anchoredPosition = Vector2.Lerp(startPosition, endPosition, (elapsedTime / gameOverMoveDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        GameOverImage.anchoredPosition = endPosition;
    }

    public void AddScore(int points)
    {
        // 점수 추가 및 업데이트
        currentScore += points;
        UpdateScoreText();
    }

    void CheckForNewHighScore()
    {
        Debug.Log("Checking for new high score: CurrentScore = " + currentScore + ", HighScore = " + highScore);
        if (currentScore > highScore)
        {
            Debug.Log("New high score achieved!");
            newScoreImage.gameObject.SetActive(true); // 신기록 이미지 활성화
            PlayerPrefs.SetInt("HighScore", currentScore);
            StartCoroutine(ShowNewHighScoreThenGameOver());
        }
        else
        {
            Debug.Log("No new high score.");
            StartCoroutine(MoveGameOverImage());
        }
    }

    IEnumerator ShowNewHighScoreThenGameOver()
    {
        yield return new WaitForSeconds(3); // 3초 동안 기다림
        newScoreImage.gameObject.SetActive(false); // 신기록 이미지 숨김
        StartCoroutine(MoveGameOverImage()); // 이제 GameOver 이미지 움직임
    }

    void UpdateScoreText()
    {
        // 점수 Text 업데이트
        scoreText.text = currentScore.ToString();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddScore(10);
        }
    }
}
