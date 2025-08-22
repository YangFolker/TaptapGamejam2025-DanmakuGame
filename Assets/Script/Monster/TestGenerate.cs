using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGenerate : MonoBehaviour
{
    public MonsterPool pool;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pool.GetMonster(0,pool.StartPos);
        }
    }
}
