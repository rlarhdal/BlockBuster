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
    // 점수UI
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
        public static int level = 1;  // 레벨
        public static int openStage = 1;  // 해금된 스테이지
    }


    //클리어했을때 다음 스테이지 오픈 코드 
    //public void OpenStage()
    //{
    //    if (sceneVariable.level == 1 && sceneVariable.openStage == 1)
    //    {
    //        sceneVariable.openStage = 2;
    //    }
    //    else if (sceneVariable.level == 2 && sceneVariable.openStage == 2)
    //    {
    //        sceneVariable.openStage = 3;
    //    }  // 이전 스테이지 클리어 시 다음 스테이지 오픈
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

        // 점수 불러오기
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }
        bestScore = PlayerPrefs.GetInt("BestScore");

        bestScoreTxt.text = $"최고 점수 : {bestScore}";
        scoreTxt.text = $"현재 점수 : {currentScore}";
    }

    private void Update()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
        ballCount = balls.Length;

        bestScoreTxt.text = $"최고 점수 : {bestScore}";
        scoreTxt.text = $"현재 점수 : {currentScore}";

        if (ballCount <= 0)
        {
            //결과창 띄우기
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
        p_best.text = $"최고 점수 : {bestScore}";
        p_score.text = $"현재 점수 : {currentScore}";

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
        
        bestScoreTxt_GO.text = $"최고 점수 : {bestScore}";
        currentScoreTxt_GO.text = $"이전 점수 : {currentScore}";
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

        clearScore.text = $"현재 점수 : {currentScore}";
        clearBest.text = $"최고 점수 : {bestScore}";

    }
}
