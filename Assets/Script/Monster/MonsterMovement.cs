using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    //实际移动速度
    public float speed;
    //实际移动方向
    public Vector3 dir;
    public bool isMove = false;
    void Update()
    {
        // 持续沿着指定方向移动
        if (isMove)
        {
            Movement();
        }
        
    }
    public void Movement()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
