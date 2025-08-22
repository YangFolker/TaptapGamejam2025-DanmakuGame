using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [Header("MainPlayer")]
    public bool IsMainPlayer = true;

    [Header("移动设置")]
    public float moveSpeed = 5f;

    [Header("控制键")]
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode fireKey = KeyCode.Space;

    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public int bulletCount = 1;
    public Vector3 direction = Vector3.forward;
    public float bulletSpeed = 10f;

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(leftKey))
        {
            movement.x -= moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(rightKey))
        {
            movement.x += moveSpeed * Time.deltaTime;
        }
        if (Input.GetKeyDown(fireKey))
        {
            FireBullets();
        }

        transform.Translate(movement);
    }

    void FireBullets()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction.normalized * bulletSpeed;
            }
        }
    }
}