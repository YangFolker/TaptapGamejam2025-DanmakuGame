using UnityEngine;

public class Bullet : MonoBehaviour
{
    //1= 红色 2=蓝色
    public int bulletType = 0; // 子弹类型
    public float speed = 10f;  // 子弹的速度
    public float lifetime = 5f;  // 子弹的生命周期（秒数）

    private Vector3 direction;  // 子弹的运动方向

    public float explosionRadius = 0.5f; //子弹爆炸半径
    public int explosionDamage = 10;

    public float explosionDuration = 2f;  // 爆炸持续时间
    public GameObject explosionPrefab;   // 爆炸预制体（圆形）

    public bool parentType;


    // 初始化子弹的方向和速度
    public void Initialize(Vector3 direction, float speed, float lifetime, bool parentType)
    {
        this.direction = direction.normalized;  // 确保方向是单位向量
        this.speed = speed;
        this.lifetime = lifetime;
        this.parentType = parentType; // 设置子弹的父类型

        // 销毁子弹
        Destroy(gameObject, lifetime);  // 生命周期结束后销毁子弹
    }

    void Update()
    {
        // 子弹的匀速直线运动
        transform.Translate(direction * speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查子弹与其他物体的碰撞
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            if (collision.gameObject.GetComponent<Bullet>().bulletType != this.bulletType)
            {
                Destroy(gameObject);  // 销毁子弹
                Destroy(collision.gameObject);  // 销毁碰撞的子弹
                Explode();
            }

        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            // 确保碰撞的是怪物
            Monster monster = collision.gameObject.GetComponent<Monster>();
            if (monster != null)
            {
                // 判断子弹和怪物的类型是否一致
                if (monster.monsterType == bulletType)
                {
                    // 如果类型一致，则触发怪物死亡
                    monster.Die(parentType);
                    Destroy(gameObject);  // 销毁子弹
                }
            }
            else
            {
                Debug.LogWarning("Monster component not found on the collided object.");
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // 确保碰撞的是玩家
            BulletShooter bulletShooter = collision.gameObject.GetComponent<BulletShooter>();
            if (bulletShooter != null)
            {
                PlayerHealth.instance?.TakeDamage(10, bulletShooter.IsMainPlayer);
                GameManger.instance.SetMoney(GameManger.instance.AttactPlayerRewait * bulletShooter.bulletCount);
                Destroy(gameObject);  // 销毁子弹
            }
            else
            {
                Debug.LogWarning("BulletShooter component not found on the collided player.");
            }
        }
    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
    //     {
    //         Explode();
    //         Destroy(gameObject);  // 销毁子弹
    //     }
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))  // 确保碰撞的是怪物
    //     {
    //         Monster monster = collision.gameObject.GetComponent<Monster>();
    //         if (monster != null)
    //         {
    //             // 判断子弹和怪物的类型是否一致
    //             if (monster.monsterId == bulletType)
    //             {
    //                 // 如果类型一致，则触发怪物死亡
    //                 monster.Die();
    //                 Destroy(gameObject);  // 销毁子弹
    //             }
    //         }

    //     }
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    //     {
    //         PlayerHealth.instance?.TakeDamage(10,collision.gameObject.GetComponent<BulletShooter>().IsMainPlayer);
    //         Destroy(gameObject);  // 销毁子弹
    //     }
    // }
    // void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
    //     {
    //         Explode();
    //         Destroy(gameObject);  // 销毁子弹
    //     }
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))  // 确保碰撞的是怪物
    //     {
    //         Monster monster = collision.gameObject.GetComponent<Monster>();
    //         if (monster != null)
    //         {
    //             // 判断子弹和怪物的类型是否一致
    //             if (monster.monsterId == bulletType)
    //             {
    //                 // 如果类型一致，则触发怪物死亡
    //                 monster.Die();
    //                 Destroy(gameObject);  // 销毁子弹
    //             }
    //         }

    //     }
    //     if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    //     {
    //         PlayerHealth.instance.TakeDamage(10,collision.gameObject.GetComponent<BulletShooter>().IsMainPlayer);
    //         Destroy(gameObject);  // 销毁子弹
    //     }
    // }
    void Explode()
    {
        TriggerExplosion(transform.position);
        // 找到爆炸范围内的所有怪物
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D hitCollider in hitColliders)
        {
            Debug.Log(hitCollider.name);
        }

        foreach (Collider2D hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Monster"))
            {
                Monster monster = hitCollider.GetComponent<Monster>();
                if (monster != null)
                {
                    // 对怪物造成伤害
                    monster.TakeDamage(2, parentType);
                }
            }
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                BulletShooter bulletShooter = hitCollider.GetComponent<BulletShooter>();
                PlayerHealth.instance.TakeDamage(10, bulletShooter.IsMainPlayer);
            }

        }
    }

    public void TriggerExplosion(Vector3 position)
    {
        // 创建爆炸效果
        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);

        // 设置爆炸的大小（可选）
        explosion.transform.localScale = new Vector3(explosionRadius, explosionRadius, explosionRadius);  // 示例，设置为 2 倍大小

        // // 在 explosionDuration 秒后销毁爆炸效果
        // Destroy(explosion, explosionDuration);
    }
}
