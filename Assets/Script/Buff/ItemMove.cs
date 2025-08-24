using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class ItemMove : MonoBehaviour
{
    private int a;
    void Start()
    {
        a = Random.Range(0, 2);
    }
    void Update()
    {
        if (a == 0)
        {
            transform.Translate(Vector3.up * Time.deltaTime);
        }
        else if (a == 1)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
    }

}
