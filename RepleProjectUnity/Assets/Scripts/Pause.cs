using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public Button pauseButton;
    public RectTransform pauseImage;
    public Button goBackButton;
    public Button preferencesButton;
    public RectTransform preferencesImage;
    public Button checkBox_backgroundMusic;
    public Button checkBox_backgroundMusic_checked;
    public Button checkBox_soundEffect;
    public Button checkBox_soundEffect_checked;
    public Button confirm;
    public float moveSpeed = 1f;

    private bool pauseIsMoving = false;
    private bool preferencesIsMoving = false;
    private Vector2 targetPosition = new Vector2(0, 0);
    private Vector2 pauseStartPosition;
    private Vector2 preferencesStartPosition;
    private float time;

    void Start()
    {
        pauseStartPosition = pauseImage.anchoredPosition;
        preferencesStartPosition = preferencesImage.anchoredPosition;

        pauseButton.onClick.AddListener(TogglePause);
        goBackButton.onClick.AddListener(ToggleGoBack);
        preferencesButton.onClick.AddListener(TogglePreferences);

        checkBox_backgroundMusic.gameObject.SetActive(true);
        checkBox_soundEffect.gameObject.SetActive(true);
        checkBox_backgroundMusic_checked.gameObject.SetActive(false);
        checkBox_soundEffect_checked.gameObject.SetActive(false);
        // 체크박스 클릭 이벤트 초기화
        checkBox_backgroundMusic.onClick.AddListener(() => ToggleBackgroundMusic(true));
        checkBox_backgroundMusic_checked.onClick.AddListener(() => ToggleBackgroundMusic(false));
        checkBox_soundEffect.onClick.AddListener(() => ToggleSoundEffect(true));
        checkBox_soundEffect_checked.onClick.AddListener(() => ToggleSoundEffect(false));
        confirm.onClick.AddListener(ToggleConfirm);
    }

    void TogglePause()
    {
         // 게임 중지 및 이미지 이동 시작
         Time.timeScale = 0;
         pauseIsMoving = true;
         time = 0;
    }

    void ToggleGoBack()
    {
        Time.timeScale = 1;
        pauseIsMoving = false;
        pauseImage.localPosition = pauseStartPosition;
    }

    void TogglePreferences()
    {
        pauseImage.localPosition = pauseStartPosition;
        preferencesIsMoving = true;
        time = 0;
    }

    void ToggleBackgroundMusic(bool isMusic)
    {
        if (isMusic == true)
        {
            checkBox_backgroundMusic.gameObject.SetActive(false);
            checkBox_backgroundMusic_checked.gameObject.SetActive(true);
        }
        else if (isMusic == false)
        {
            checkBox_backgroundMusic.gameObject.SetActive(true);
            checkBox_backgroundMusic_checked.gameObject.SetActive(false);
        }
    }

    void ToggleSoundEffect(bool isEffect)
    {
        if (isEffect == true)
        {
            checkBox_soundEffect.gameObject.SetActive(false);
            checkBox_soundEffect_checked.gameObject.SetActive(true);
        }
        else if (isEffect == false)
        {
            checkBox_soundEffect.gameObject.SetActive(true);
            checkBox_soundEffect_checked.gameObject.SetActive(false);
        }
    }

    void ToggleConfirm()
    {
        preferencesImage.localPosition = preferencesStartPosition;
        pauseIsMoving = true;
        time = 0;
    }

    void Update()
    {
        if (pauseIsMoving)
        {
            time += Time.unscaledDeltaTime * moveSpeed; // Time.timeScale이 0일 때도 작동하도록 Time.unscaledDeltaTime 사용
            pauseImage.anchoredPosition = Vector2.Lerp(pauseStartPosition, targetPosition, time);

            if (pauseImage.anchoredPosition == targetPosition)
            {
                pauseIsMoving = false;
            }
        }
        else if (preferencesIsMoving)
        {
            time += Time.unscaledDeltaTime * moveSpeed; // Time.timeScale이 0일 때도 작동하도록 Time.unscaledDeltaTime 사용
            preferencesImage.anchoredPosition = Vector2.Lerp(preferencesStartPosition, targetPosition, time);

            if (preferencesImage.anchoredPosition == targetPosition)
            {
                preferencesIsMoving = false;
            }
        }
    }
}
