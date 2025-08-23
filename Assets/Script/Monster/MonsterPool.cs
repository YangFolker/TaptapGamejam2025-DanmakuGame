using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class MonsterPool : MonoBehaviour
{
    public static MonsterPool instance;  // 单例实例
    public int initialPoolSize = 10;  // 初始池大小
    public Transform monsterMax;
    public Transform monsterMin;
    public float yMax = 0; // 怪物初始位置的最大值
    public float yMin = 0; // 怪物初始位置的最小值
    private int monsterNumMax = 100; // 怪物数量上限
    private int curMonsterNum = 0; // 当前怪物数量

    public Vector3 StartPos = Vector3.zero; // 怪物初始位置
    public GameObject[] monsterPrefabs;  // 不同类型的怪物预制体

    private float timer = 0f;  // 计时器
    public float generateInterval = 1f;  // 生成怪物的时间间隔（秒）

    public bool isGenerate; // 是否生成怪物

    // 使用字典存储每种怪物类型的池
    public Dictionary<int, Queue<GameObject>> staticPool = new Dictionary<int, Queue<GameObject>>();  // 不活动的怪物池
    public Dictionary<int, Queue<GameObject>> activatePool = new Dictionary<int, Queue<GameObject>>();  // 活动的怪物池
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
        yMax = monsterMax.position.y;
        yMin = monsterMin.position.y;
    }
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
        isGenerate = true;

    }
    void Update()
    {
        if (isGenerate)
        {
            timer += Time.deltaTime;  // 增加计时器

            if (timer >= generateInterval)  // 当计时器达到设定的间隔时
            {
                RandomGenerate();  // 生成怪物
                timer = 0f;  // 重置计时器
                generateInterval = Random.Range(1f, 2f);  // 重新生成间隔
            }
        }
    }

    public void RandomGenerate()
    {
        int a = Random.Range(0, 10);  // 随机生成怪物数量
        if (curMonsterNum >= monsterNumMax)
        {
            return;  // 如果已达到最大怪物数量，返回
        }
        
        for (int i = 0; i < a; i++)
        {
            GetMonster(Random.Range(0, monsterPrefabs.Length));  // 获取怪物
            curMonsterNum += 1;  // 更新当前怪物数量
            if (curMonsterNum >= monsterNumMax)
            {
                break;  // 达到最大怪物数量时跳出循环
            }
        }
    }
    /// <summary>
    /// 对外使用
    /// </summary>
    /// <param name="monsterId">需要的怪物类型</param>
    /// <param name="pos">生成位置</param>
    public void GetMonster(int monsterId)
    {
        if (staticPool.ContainsKey(monsterId))
        {
            GameObject monster = RealGetMonster(monsterId);
            Vector3 curPos = new Vector3(monsterMax.position.x, Random.Range(yMin, yMax), 0);
            monster.transform.position = curPos;
            monster.GetComponent<Monster>().Init();
        }
        else
        {
            Debug.Log("没有这种怪物");
        }

    }
    // 获取指定类型的怪物
    private GameObject RealGetMonster(int monsterId)
    {
        if (staticPool.ContainsKey(monsterId) && staticPool[monsterId].Count > 0)
        {
            // 从指定类型的池中取出一个怪物
            GameObject monster = staticPool[monsterId].Dequeue();
            monster.SetActive(true);  // 激活怪物
            activatePool[monsterId].Enqueue(monster);  // 将怪物放入活动池
            return monster;
        }
        else
        {
            // 如果没有可用的怪物，实例化新的怪物并返回
            GameObject newMonster = Instantiate(monsterPrefabs[monsterId]);
            activatePool[monsterId].Enqueue(newMonster);
            return newMonster;
        }
    }
    public void ReturnMonster(int monsterId, GameObject monster)
    {
        RealReturnMonster(monsterId, monster);
    }
    // 将怪物返回到池中
    private void RealReturnMonster(int monsterId, GameObject monster)
    {
        monster.SetActive(false);  // 禁用怪物对象
        activatePool[monsterId].Dequeue();  // 从活动池中移除
        staticPool[monsterId].Enqueue(monster);  // 将怪物放回不活动池
    }
    /// <summary>
    /// 应该用不上，放回所有已激活怪物
    /// </summary>
    public void DeactivateAllMonsters()
    {
        // 遍历所有怪物类型
        foreach (var key in activatePool.Keys)
        {
            Queue<GameObject> activeMonsters = activatePool[key];

            // 遍历每个已激活的怪物
            while (activeMonsters.Count > 0)
            {
                GameObject monster = activeMonsters.Dequeue();  // 获取活动池中的怪物
                monster.SetActive(false);  // 禁用怪物
                staticPool[key].Enqueue(monster);  // 将怪物返回到未激活池
            }
        }
    }

}

