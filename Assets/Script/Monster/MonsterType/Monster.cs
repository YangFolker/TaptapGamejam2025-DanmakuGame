using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int maxHealth = 100;   // 最大生命值
    private int currentHealth;    // 当前生命值
    public float speed;
    public Vector3 direction;
    public int monsterId;
    public MonsterMovement monsterMovement;
    void Awake()
    {
        monsterMovement = GetComponent<MonsterMovement>();
    }
    public void Init()
    {
        monsterMovement.speed = speed;
        monsterMovement.dir = direction;
        monsterMovement.isMove = true;
    }

    void OnEnable()
    {
        // 每次激活时重置生命值
        currentHealth = maxHealth;
    }

    /// <summary>
    /// 受到伤害（外部调用接口）
    /// </summary>
    /// <param name="damage">伤害数值</param>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} 受到了 {damage} 点伤害，当前血量：{currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 怪物死亡处理
    /// </summary>
    public void Die()
    {
        Debug.Log($"{gameObject.name} 死亡！");

        // 这里你可以播放死亡动画，或者掉落物品
        // 暂时直接禁用怪物，交还对象池
        MonsterPool.Instance.ReturnMonster(monsterId,gameObject);

        // 如果你有对象池管理类，可以在这里调用 ReturnMonster()
        // 比如：
        // MonsterSpawner spawner = FindObjectOfType<MonsterSpawner>();
        // spawner.ReturnMonsterToPool(gameObject);
    }
}

