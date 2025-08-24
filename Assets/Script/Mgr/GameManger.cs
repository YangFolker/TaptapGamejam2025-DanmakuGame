using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{

    public int maxHealth = 100;  // 最大生命值
    public int currentHealth;   // 上方当前生命值
    public int currentHealth_down;//下方当前生命值
    public int MainNum = 0;
    

    public int AttactPlayerRewait = 3;
    public int KillEnemy = 2;

    public GameObject FinllayUI; //游戏结束UI
    public static GameManger instance;

    //分数记录
    public int playerScore = 0;  // 玩家的分数
    public int money = 0;//玩家经济

    void Awake()
    {
        // 确保单例初始化
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);  // 如果已有实例，销毁当前对象
        }
        money = 50;
    }
    void Start()
    {
        MainNum = 1000;
    }
    public void GameOver()
    {
        if (FinllayUI != null)
        {
            FinllayUI.SetActive(true);  // 激活结算面板
        }
        else
        {
            Debug.LogError("结算面板未设置");
        }
        // 游戏结束时暂停游戏时间
        // 设置游戏时间为 0，暂停游戏
        Debug.Log("分数" + playerScore);

        // 激活游戏结束的面板
        
        Time.timeScale = 0f;
    }
    public void SetMoney(int num)
    {
        money += num;
    }

    //     // // 更新结算页面上的分数（如果有）
    //     if (scoreText != null)
    //     {
    //         scoreText.text = "Score: " + playerScore.ToString();  // 更新分数显示
    //     }
    //     SavePlayerProgress();  // 假设有一个保存进度的方法
    // }
    // private void SavePlayerProgress()
    // {
    //     // 假设我们将分数保存到 PlayerPrefs 中（持久化存储）
    //     PlayerPrefs.SetInt("LastScore", playerScore);
    //     PlayerPrefs.Save();
    //     Debug.Log("Player progress saved.");
    // }

}
