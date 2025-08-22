using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHealth : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            PlayerHealth.instance.TakeDamage(10, true);  // 回血 10 点
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayerHealth.instance.TakeDamage(10, false);  // 掉血 10 点
        }
    }
}
