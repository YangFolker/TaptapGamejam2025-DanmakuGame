using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelMainUI : MonoBehaviour
{
    public StartBtn startBtn;
    public ExitBtn exitBtn;
    public string StartText;
    public string ExitText;

    void Awake()
    {
        startBtn?.Init(this, StartText);
        exitBtn?.Init(this, ExitText);
    }
    public void LoadGame()
    {
        Debug.LogError("没有加载场景");
    }
    public void LoadGame(int sceneIndex = 0)
    {
        Hide();
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadGame(string sceneName)
    {
        Hide();
        SceneManager.LoadScene(sceneName);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
