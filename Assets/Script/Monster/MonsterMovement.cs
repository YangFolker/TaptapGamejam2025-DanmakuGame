using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    //实际移动速度
    public float speed;
    //实际移动方向
    public Vector3 dir;
    public bool isMove = false;
    private float y_min = 0;
    private float y_max = 0;
    private Vector3 vertical_offset = new Vector3(0, 0, 0);
    float timer = 0f;
    void Awake(){
        y_min = MonsterPool.instance.yMin;
        y_max = MonsterPool.instance.yMax;

    }
    void Update()
    {
        // 持续沿着指定方向移动
        if (isMove)
        {
            Movement();
        }
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            vertical_offset = new Vector3(0, UnityEngine.Random.Range(-0.4f, 0.4f), 0);
            timer = 0f;
        }
        
    }
    public void Movement()
    {
        transform.Translate(dir * speed * Time.deltaTime);
        if(transform.position.y>y_min && transform.position.y<y_max){
            transform.Translate(vertical_offset * speed * Time.deltaTime);
        }else{
            transform.Translate(-1*vertical_offset * speed * Time.deltaTime);
        }
        
    }
}
