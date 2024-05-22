using UnityEngine;

public class QuitButton : MonoBehaviour
{
    // 게임 종료
    //public void QuitGame()
    //{
    //    Application.Quit();
    //}
    public void GameExit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
