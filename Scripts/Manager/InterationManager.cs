using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InterationManager : MonoBehaviour
{
    public static InterationManager instance;
    
    public TMP_Text countdownText;
    public GameObject menuPanel;

    private bool isPaused = false;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    // 일시정지
    public void PauseGame()
    {
        menuPanel.gameObject.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
    }

    // 게임 재개
    public void ResumeGame()
    {
        menuPanel.gameObject.SetActive(false);

        countdownText.gameObject.SetActive(true);

        StartCoroutine(ResumeAfterDelay());
    }

    // 3초 카운트 후 시작
    IEnumerator ResumeAfterDelay()
    {
        for (int countdown = 3; countdown > 0; countdown--) // 1초마다 숫자가 줄어 듦
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSecondsRealtime(1f);
        }

        countdownText.gameObject.SetActive(false);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GameExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
