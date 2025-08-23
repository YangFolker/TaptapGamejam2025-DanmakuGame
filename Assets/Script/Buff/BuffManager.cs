using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public BulletShooter Mainplayer;
    public BulletShooter BulletShooter;
    public static BuffManager instance;
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
    /// <summary>
    /// 怪物buff
    /// </summary>
    /// <param name="id"></param>
    /// <param name="num"></param>
    private void AddBulletNum(int id, int num)
    {

    }///负数为减
    public void AddBulletNum(bool type, int num)
    {
        if (type && Mainplayer.bulletCount + num > 0)
        {
            Mainplayer.bulletCount += num;
        }
        else if(!type && BulletShooter.bulletCount + num > 0)
        {
            BulletShooter.bulletCount += num;
        }
    }
}
