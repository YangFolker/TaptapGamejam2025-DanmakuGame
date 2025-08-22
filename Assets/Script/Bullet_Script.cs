using UnityEngine;

public class Bullet : MonoBehaviour
{
    //1= 红色 2=蓝色
    public int bulletType = 0; // 子弹类型
    public float speed = 10f;  // 子弹的速度
    public float lifetime = 5f;  // 子弹的生命周期（秒数）

    private Vector3 direction;  // 子弹的运动方向

    // 初始化子弹的方向和速度
    public void Initialize(Vector3 direction, float speed, float lifetime)
    {
        this.direction = direction.normalized;  // 确保方向是单位向量
        this.speed = speed;
        this.lifetime = lifetime;

        // 销毁子弹
        Destroy(gameObject, lifetime);  // 生命周期结束后销毁子弹
    }

    void Update()
    {
        // 子弹的匀速直线运动
        transform.Translate(direction * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Explode();
            Destroy(gameObject);  // 销毁子弹
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))  // 确保碰撞的是怪物
        {
            Monster monster = collision.gameObject.GetComponent<Monster>();
            if (monster != null)
            {
                // 判断子弹和怪物的类型是否一致
                if (monster.monsterId == bulletType)
                {
                    // 如果类型一致，则触发怪物死亡
                    monster.Die();
                    Destroy(gameObject);  // 销毁子弹
                }
            }

        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerHealth.instance.TakeDamage(10,collision.gameObject.GetComponent<BulletShooter>().IsMainPlayer);
            Destroy(gameObject);  // 销毁子弹
        }
    }
    void Explode()
    {
        // 子弹爆炸的逻辑
    }
}
