using UnityEngine;

public class MoveOnKeyPress : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 5f;

    [Header("控制键")]
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;

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

        transform.Translate(movement);
    }
}