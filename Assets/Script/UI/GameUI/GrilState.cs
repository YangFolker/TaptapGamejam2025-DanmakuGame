using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class GrilState : MonoBehaviour
{
    public Sprite normal;
    public Sprite happy;
    public Sprite sad;

    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        SwitchState();
    }
    public void SwitchState()
    {
        if (GameManger.instance.currentHealth >= 80 || GameManger.instance.currentHealth_down <= 20)
        {
            anim.SetTrigger("SetSad");
        }
        if (GameManger.instance.currentHealth <= 60 && GameManger.instance.currentHealth_down >= 40)
        {
            anim.SetTrigger("SetNormal");
        }
    }
}
