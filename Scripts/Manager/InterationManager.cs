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

    // �Ͻ�����
    public void PauseGame()
    {
        menuPanel.gameObject.SetActive(true);

        Time.timeScale = 0f;
        isPaused = true;
    }

    // ���� �簳
    public void ResumeGame()
    {
        menuPanel.gameObject.SetActive(false);

        countdownText.gameObject.SetActive(true);

        StartCoroutine(ResumeAfterDelay());
    }

    // 3�� ī��Ʈ �� ����
    IEnumerator ResumeAfterDelay()
    {
        for (int countdown = 3; countdown > 0; countdown--) // 1�ʸ��� ���ڰ� �پ� ��
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
