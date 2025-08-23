using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReStartBtn : MonoBehaviour
{
    public Sprite one;
    public Sprite two;
    public Image image;

    void OnEnable()
    {
        int a = Random.Range(1, 3);
        if (a == 1)
        {
            image.sprite = one;
        }
        else if (a == 2)
        {
            image.sprite = two;
        }

        // 确保重新加载场景时，按钮事件重新绑定
        Button restartButton = GetComponent<Button>();
        restartButton.onClick.RemoveAllListeners();  // 移除所有先前的监听器
        restartButton.onClick.AddListener(ReStartEvent);  // 重新绑定事件
    }

    private void ReStartEvent()
    {
        Debug.Log("ReStartEvent");

        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
