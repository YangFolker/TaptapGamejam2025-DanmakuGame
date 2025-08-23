using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
             Monster monster = collision.gameObject.GetComponent<Monster>();
            if (monster != null)
            {
                switch (monster.monsterType)
                {
                    case 0:
                        Debug.LogError("怪物生成错误");
                        break;
                    case 1:
                        PlayerHealth.instance.TakeDamage(10, true);
                        break;
                    case 2:
                        PlayerHealth.instance.TakeDamage(10, false);
                        break;
                }
                MonsterPool.instance.ReturnMonster(monster.monsterId,collision.gameObject);
            }
        }
    }
}
