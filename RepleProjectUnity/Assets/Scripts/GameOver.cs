using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public RectTransform gameOver;
    public Button mainPageButton;
    public Button retryButton;

    private void Start()
    {
        mainPageButton.onClick.AddListener(ToggleMainPage);
        retryButton.onClick.AddListener(ToggleRetry);
    }

    void ToggleMainPage()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Title");
    }
    void ToggleRetry()
    {
        PlayerPrefs.SetInt("CurrentScore", 0);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Play");
    }
}
