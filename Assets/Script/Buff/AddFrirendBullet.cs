using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFrirendBullet : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //Debug.Log("Add Frirend Bullet");
            bool self = collision.gameObject.GetComponent<BulletShooter>().IsMainPlayer;
            if (self)
            {
                BuffManager.instance.AddBulletNum(!self, 1);
            }
            else
            {
                BuffManager.instance.AddBulletNum(self, 1);
            }
        }
    }
}
