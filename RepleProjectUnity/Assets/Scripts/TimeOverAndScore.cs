using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOverAndScore : MonoBehaviour
{
    public GameObject[] Audience_L;
    public GameObject[] Audience_R;
    public RectTransform GameOverImage; // GameOver �̹����� ���� ����
    public float moveDistance = 5.0f;
    public float moveDuration = 2.0f;
    public float gameOverMoveDuration = 2.0f; // GameOver �̹����� �̵� �ӵ� ������ ���� ����

    private float delayBetweenMoves = 10.0f;

    public Text scoreText; // ������ ǥ���� Text ������Ʈ
    public RectTransform newScoreImage; // newScore �̹����� ���� ����
    private int currentScore;
    private int highScore;

    void Start()
    {
        newScoreImage.gameObject.SetActive(false);
        // ���� �ְ� ������ �ε�
        highScore = PlayerPrefs.GetInt("HighScore");
        UpdateScoreText(); // ���� Text ������Ʈ
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
        // ���� �߰� �� ������Ʈ
        currentScore += points;
        UpdateScoreText();
    }

    void CheckForNewHighScore()
    {
        Debug.Log("Checking for new high score: CurrentScore = " + currentScore + ", HighScore = " + highScore);
        if (currentScore > highScore)
        {
            Debug.Log("New high score achieved!");
            newScoreImage.gameObject.SetActive(true); // �ű�� �̹��� Ȱ��ȭ
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
        yield return new WaitForSeconds(3); // 3�� ���� ��ٸ�
        newScoreImage.gameObject.SetActive(false); // �ű�� �̹��� ����
        StartCoroutine(MoveGameOverImage()); // ���� GameOver �̹��� ������
    }

    void UpdateScoreText()
    {
        // ���� Text ������Ʈ
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
