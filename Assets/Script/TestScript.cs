using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))] // 确保物体有碰撞器才能被射线检测到
public class ClickToMove : MonoBehaviour
{
    // 移动的距离，这里设置为1单位
    public float moveDistance = 1f;

    void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 创建一条从摄像机到鼠标点击位置的射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 射线检测，如果击中了物体
            if (Physics.Raycast(ray, out hit))
            {
                // 检查击中的是否是当前脚本所附加的物体
                if (hit.collider.gameObject == gameObject)
                {
                    // 沿X轴移动指定距离
                    Vector3 newPosition = transform.position;
                    newPosition.x += moveDistance;
                    transform.position = newPosition;

                    // 在控制台输出移动信息
                    Debug.Log($"{gameObject.name} 已沿X轴移动 {moveDistance} 单位");
                }
            }
        }
    }
}
