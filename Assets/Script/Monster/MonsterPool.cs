using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public class MonsterPool : MonoBehaviour
{
     public static MonsterPool Instance;  // 单例实例
    public int initialPoolSize = 10;  // 初始池大小
    public Vector3 StartPos = Vector3.zero; // 怪物初始位置
    public GameObject[] monsterPrefabs;  // 不同类型的怪物预制体

    // 使用字典存储每种怪物类型的池
    public Dictionary<int, Queue<GameObject>> staticPool = new Dictionary<int, Queue<GameObject>>();  // 不活动的怪物池
    public Dictionary<int, Queue<GameObject>> activatePool = new Dictionary<int, Queue<GameObject>>();  // 活动的怪物池

    void Start()
    {
        // 初始化怪物池，每种怪物类型都有自己的池
        for (int i = 0; i < monsterPrefabs.Length; i++)
        {
            int id = monsterPrefabs[i].GetComponent<Monster>().monsterId;
            staticPool[id] = new Queue<GameObject>();
            activatePool[id] = new Queue<GameObject>();

            // 初始化每种怪物类型的池
            for (int j = 0; j < initialPoolSize; j++)
            {
                GameObject monster = Instantiate(monsterPrefabs[i]);
                monster.SetActive(false);  // 初始时怪物不激活
                staticPool[id].Enqueue(monster);  // 放入不活动池
            }
        }
    }
    /// <summary>
    /// 对外使用
    /// </summary>
    /// <param name="monsterType">需要的怪物类型</param>
    /// <param name="pos">生成位置</param>
    public void GetMonster(int monsterType, Vector3 pos)
    {
        if (staticPool.ContainsKey(monsterType))
        {
            GameObject monster = RealGetMonster(monsterType);
            monster.transform.position = pos;
            monster.GetComponent<Monster>().Init();
        }
        else
        {
            Debug.Log("没有这种怪物");
        }

    }
    // 获取指定类型的怪物
    private GameObject RealGetMonster(int monsterType)
    {
        if (staticPool.ContainsKey(monsterType) && staticPool[monsterType].Count > 0)
        {
            // 从指定类型的池中取出一个怪物
            GameObject monster = staticPool[monsterType].Dequeue();
            monster.SetActive(true);  // 激活怪物
            activatePool[monsterType].Enqueue(monster);  // 将怪物放入活动池
            return monster;
        }
        else
        {
            // 如果没有可用的怪物，实例化新的怪物并返回
            GameObject newMonster = Instantiate(monsterPrefabs[monsterType]);
            activatePool[monsterType].Enqueue(newMonster);
            return newMonster;
        }
    }
    public void ReturnMonster(int monsterType, GameObject monster)
    {
        RealReturnMonster(monsterType, monster);
    }
    // 将怪物返回到池中
    private void RealReturnMonster(int monsterType, GameObject monster)
    {
        monster.SetActive(false);  // 禁用怪物对象
        activatePool[monsterType].Dequeue();  // 从活动池中移除
        staticPool[monsterType].Enqueue(monster);  // 将怪物放回不活动池
    }
}

