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
    public int monsterType;//判断怪物属于蓝色阵营还是红色阵营，默认1红2蓝
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
        GameManger.instance.playerScore += 10;
        MonsterPool.instance.ReturnMonster(monsterId,gameObject);
    }
}

