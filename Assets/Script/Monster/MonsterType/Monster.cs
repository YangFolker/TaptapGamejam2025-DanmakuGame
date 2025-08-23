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
    private Animator animator;
    void Awake()
    {
        monsterMovement = GetComponent<MonsterMovement>();
    }
    public void Init()
    {
        monsterMovement.speed = speed;
        monsterMovement.dir = direction;
        monsterMovement.isMove = true;
        animator = GetComponent<Animator>();
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
        monsterMovement.speed = 0;
        animator.SetTrigger("die_trigger");
        StartCoroutine(WaitAndDestroy());
        //更新怪物数量
        MonsterPool.instance.curMonsterNum -= 1;

        int a = Random.Range(0, 10);
        if (a == 2)
        {

        }
        GameManger.instance.playerScore += 10;
        GameManger.instance.money += 10;
        
    }

    private IEnumerator WaitAndDestroy()
    {
        // 获取当前动画状态信息
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // 等待动画播放完毕
        yield return new WaitForSeconds(stateInfo.length);
        MonsterPool.instance.ReturnMonster(monsterId,gameObject);
    }
}

