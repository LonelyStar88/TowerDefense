using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnClickEvent : MonoBehaviour
{
    [SerializeField]private SceneChange sceneManager;
    public void OnGameStart()
    {
        SceneChange.instance.Change("Game");
    }
    public void OnGameOption()
    {
        SceneChange.instance.Change("Option");
    }

    public void OnGameExit()
    {
       // Application.Quit();
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit(); // 어플리케이션 종료
    #endif
    }
    public void OnGameReStart()
    {
        SceneManager.LoadScene("Game");

    }
}
