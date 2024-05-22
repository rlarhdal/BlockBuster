using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public TextMeshProUGUI lockedTxt;
    public GameObject lockedPanel;

    public void LoadLevelScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelectScene");
    }

    public void LoadGameScene(int num)
    {
        BallInstantiate();

        Time.timeScale = 1f;
        GameManager.sceneVariable.level = num;
        SceneManager.LoadScene("GameScene");
    }

    public void RestartScene()
    {
        BallInstantiate();

        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void LoadMainScene()
    {
        GameObject soundManager = GameObject.Find("SoundManager");

        if (soundManager != null) Destroy(soundManager);

        Time.timeScale = 1f;
        SceneManager.LoadScene("IntroScene");
    }

    public void BallInstantiate()
    {
        if (SceneManager.GetActiveScene().name != "GameScene") return;

        GameObject ball = GameObject.Find("Ball");
        GameObject paddle = GameObject.Find("Paddle1");

        // ���� ������ 0�� �� ���ο� ���� ����
        if (ball == null)
        {
            Instantiate(ballPrefab, paddle.transform.position, Quaternion.identity);
        }
    }
    //public void GoMain()
    //{
    //    if (CompareTag("Easy"))
    //    {
    //        GameManager.sceneVariable.level = 1;
    //        SceneManager.LoadScene("DH");
    //    }
    //    else if (CompareTag("Normal"))
    //    {
    //        if (GameManager.sceneVariable.openStage < 2)
    //        {
    //            lockedTxt.text = "Easy Ŭ���� ��\n�رݵ˴ϴ�!";
    //            lockedPanel.SetActive(true);
    //            Invoke("DestroyLockedPanel", 1.5f);
    //        }
    //        else
    //        {
    //            GameManager.sceneVariable.level = 2;
    //            SceneManager.LoadScene("DH");
    //        }
    //    }
    //    else if (CompareTag("Hard"))
    //    {
    //        if (GameManager.sceneVariable.openStage < 3)
    //        {
    //            lockedTxt.text = "Normal Ŭ���� ��\n�رݵ˴ϴ�!";
    //            lockedPanel.SetActive(true);
    //            Invoke("DestroyLockedPanel", 1.5f);
    //        }
    //        else
    //        {
    //            GameManager.sceneVariable.level = 3;
    //            SceneManager.LoadScene("DH");
    //        }
    //    }
    //}

    void DestroyLockedPanel()
    {
        lockedPanel.SetActive(false);
    }


}