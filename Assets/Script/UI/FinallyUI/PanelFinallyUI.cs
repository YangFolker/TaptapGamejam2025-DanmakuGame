using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelFinallyUI : MonoBehaviour
{
    public ReStartBtn reStartBtn;
    public ReMainBtn reMainBtn;

    void Awake()
    {
        reMainBtn?.Init(this);
        this.gameObject.SetActive(false);
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
