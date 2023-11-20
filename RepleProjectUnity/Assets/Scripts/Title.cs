using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public RectTransform titleImage;
    public Button storyButton;
    public Button endlessModeButton;
    public Button endlessModeButton_lock;
    public Button preferencesButton;
    public RectTransform preferencesImage;
    public Button checkBox_backgroundMusic;
    public Button checkBox_backgroundMusic_checked;
    public Button checkBox_soundEffect;
    public Button checkBox_soundEffect_checked;
    public Button confirm;
    public float moveSpeed = 1f;

    private bool panel = false;
    private bool titleIsMoving = false;
    private bool preferencesIsMoving = false;
    private Vector2 targetPosition = new Vector2(0, 0);
    private Vector2 titleStartPosition;
    private Vector2 preferencesStartPosition;
    private float time;

    void Start()
    {
        titleStartPosition = titleImage.anchoredPosition;
        preferencesStartPosition = preferencesImage.anchoredPosition;
        storyButton.onClick.AddListener(ToggleStory);
        endlessModeButton.onClick.AddListener(ToggleEndlessMode);
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

    void Update()
    {
        if (titleIsMoving)
        {
            time += Time.unscaledDeltaTime * moveSpeed; // Time.timeScale이 0일 때도 작동하도록 Time.unscaledDeltaTime 사용
            titleImage.anchoredPosition = Vector2.Lerp(titleStartPosition, targetPosition, time);

            if (titleImage.anchoredPosition == targetPosition)
            {
                titleIsMoving = false;
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

    public void StartMoving()
    {
        if(panel == true)
        {
            titleIsMoving = true;
            time = 0;
            panel = false;
        }
        else if(panel == false)
        {
            titleImage.localPosition = titleStartPosition;
            titleIsMoving = false;
            panel = true;
        }
    }

    void ToggleStory()
    {
        SceneManager.LoadScene("Play");
    }

    void ToggleEndlessMode()
    {

    }
    void TogglePreferences()
    {
        titleImage.localPosition = titleStartPosition;
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
        titleIsMoving = true;
        time = 0;
    }
}
