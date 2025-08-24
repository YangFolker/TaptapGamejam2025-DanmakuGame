using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelMainUI : MonoBehaviour
{
    public StartBtn startBtn;
    public ExitBtn exitBtn;
    public AudioSource audioSource;

    void Awake()
    {
        startBtn?.Init(this);
        exitBtn?.Init(this);
    }
    void Start()
    {
        audioSource?.Play();
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
