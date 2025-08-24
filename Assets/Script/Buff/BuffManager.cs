using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public BulletShooter Mainplayer;
    public BulletShooter BulletShooter;
    public static BuffManager instance;
    public List<GameObject> buffs = new();
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
    }
///负数为减
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
