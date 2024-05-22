using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameManager;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] players { get; private set; }
    public ObjectPool ObjectPool { get; private set; }
    [SerializeField] private string playerTag = "Paddle";

    [Header("# UI")]
    // ����UI
    [SerializeField] private TMP_Text bestScoreTxt;
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject gameClear;
    public Slider bgmSlider;
    public Slider sfxSlider;

    [Header("# GameOver")]
    [SerializeField] private TMP_Text bestScoreTxt_GO;
    [SerializeField] private TMP_Text currentScoreTxt_GO;

    [Header("# GameClear")]
    [SerializeField] private TMP_Text clearBest;
    [SerializeField] private TMP_Text clearScore;

    [Header("# Pause")]
    [SerializeField] private TMP_Text p_score;
    [SerializeField] private TMP_Text p_best;
    [SerializeField] private GameObject pauseMenu;


    public int ballCount;
    private GameObject[] balls;

    public int blockCnt = 0;

    private int bestScore = 0;
    public int currentScore = 0;

    public static class sceneVariable
    {
        public static int level = 1;  // ����
        public static int openStage = 1;  // �رݵ� ��������
    }


    //Ŭ���������� ���� �������� ���� �ڵ� 
    //public void OpenStage()
    //{
    //    if (sceneVariable.level == 1 && sceneVariable.openStage == 1)
    //    {
    //        sceneVariable.openStage = 2;
    //    }
    //    else if (sceneVariable.level == 2 && sceneVariable.openStage == 2)
    //    {
    //        sceneVariable.openStage = 3;
    //    }  // ���� �������� Ŭ���� �� ���� �������� ����
    //}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        SoundManager.instance.ChangeBgm(BgmType.Stage);

        players = GameObject.FindGameObjectsWithTag(playerTag);
        ObjectPool = GetComponent<ObjectPool>();

        // ���� �ҷ�����
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }
        bestScore = PlayerPrefs.GetInt("BestScore");

        bestScoreTxt.text = $"�ְ� ���� : {bestScore}";
        scoreTxt.text = $"���� ���� : {currentScore}";
    }

    private void Update()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
        ballCount = balls.Length;

        bestScoreTxt.text = $"�ְ� ���� : {bestScore}";
        scoreTxt.text = $"���� ���� : {currentScore}";

        if (ballCount <= 0)
        {
            //���â ����
            LoseGame();
        }
    }

    public void CheckBlockCnt()
    {
        if (blockCnt == 0)
        {
            BlcokSpawnController.instance.SpawnBoss();
        }
    }

    public void PauseGame()
    {
        p_best.text = $"�ְ� ���� : {bestScore}";
        p_score.text = $"���� ���� : {currentScore}";

        pauseMenu.SetActive(true);
    }

    public void LoseGame()
    {
        gameOver.SetActive(true);
        SoundManager.instance.PlayeSfx("Lose");
        Time.timeScale = 0f;


        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            bestScore = currentScore;
        }
        
        bestScoreTxt_GO.text = $"�ְ� ���� : {bestScore}";
        currentScoreTxt_GO.text = $"���� ���� : {currentScore}";
    }

    public void ClearGame()
    {
        gameClear.SetActive(true);
        SoundManager.instance.PlayeSfx("Victory");
        Time.timeScale = 0f;

        if (currentScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
            bestScore = currentScore;
        }

        clearScore.text = $"���� ���� : {currentScore}";
        clearBest.text = $"�ְ� ���� : {bestScore}";

    }
}
