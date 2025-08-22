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
    public int bulletCount = 10;
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
	    float angleStep;
	    float startAngle;

	    if (bulletCount <= 10)
	    {
	        angleStep = 10f;
	        startAngle = -(angleStep * (bulletCount - 1) / 2f);
	    }
	    else
	    {
	        angleStep = 100f / (bulletCount - 1);
	        startAngle = -50f; // 100° 扇形的一半
	    }

	    for (int i = 0; i < bulletCount; i++)
	    {
	        float angle = startAngle + i * angleStep;

	        // 计算旋转后的方向（绕 Y 轴旋转）
	        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	        Vector3 bulletDir = rotation * direction.normalized;

	        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
	        Rigidbody rb = bullet.GetComponent<Rigidbody>();
	        if (rb != null)
	        {
	            rb.velocity = bulletDir * bulletSpeed;
	        }
	    }
	}
}