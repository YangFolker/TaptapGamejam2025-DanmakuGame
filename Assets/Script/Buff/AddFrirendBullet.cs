using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFrirendBullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("执行了");
        //Debug.Log("Add Frirend Bullet");
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bool self = collision.gameObject.GetComponent<BulletShooter>().IsMainPlayer;
            BuffManager.instance.AddBulletNum(!self, 1);
            Destroy(gameObject);
        }
    }
}
