using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{

    public int maxHealth = 100;  // 最大生命值
    public int currentHealth;   // 上方当前生命值
    public int currentHealth_down;//下方当前生命值
    public int MainNum = 0;
    public static GameManger instance;

    //分数记录
    public GameObject gameOverPanel;  // 游戏结束时显示的UI面板
    public UnityEngine.UI.Text scoreText;  // 显示分数的文本
    public int playerScore = 100;  // 假设这是玩家的分数
    void Awake()
    {
        // 确保单例初始化
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 保证该实例在场景切换时不被销毁
        }
        else
        {
            Destroy(gameObject);  // 如果已有实例，销毁当前对象
        }
    }
    void Start()
    {
        MainNum = 1000;
    }
    public void GameOver()
    {
        // 游戏结束逻辑
        //游戏时间暂停
        //弹出结算页面
        // 游戏结束时暂停游戏时间
        Time.timeScale = 0f;  // 设置游戏时间为 0，暂停游戏

        // 激活游戏结束的面板
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);  // 激活结算面板
        }

        // 更新结算页面上的分数（如果有）
        if (scoreText != null)
        {
            scoreText.text = "Score: " + playerScore.ToString();  // 更新分数显示
        }

        // 可以在这里添加其他结束游戏时需要的操作，比如保存玩家的分数等
        SavePlayerProgress();  // 假设有一个保存进度的方法

        // 如果需要退出游戏时处理，使用以下代码
        // ExitGame();  // 退出游戏的方法，取决于平台

        Debug.Log("Game Over!");  // 在控制台显示游戏结束
    }
    private void SavePlayerProgress()
    {
        // 假设我们将分数保存到 PlayerPrefs 中（持久化存储）
        PlayerPrefs.SetInt("LastScore", playerScore);
        PlayerPrefs.Save();
        Debug.Log("Player progress saved.");
    }

}
