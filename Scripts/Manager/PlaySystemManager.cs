using UnityEngine;
using UnityEngine.UI;


public class PlaySystemManager : MonoBehaviour
{
    public static PlaySystemManager instance;

    public Button startButton;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PlaySinglePlayer()
    {
        PlayerPrefs.SetInt("PlayerMode", 1);
        ShowGameStartBtn();
    }

    public void PlayMultiplayer()
    {
        PlayerPrefs.SetInt("PlayerMode", 2);
        ShowGameStartBtn();
    }

    private void ShowGameStartBtn()
    {
        startButton.gameObject.SetActive(true);
    }
}