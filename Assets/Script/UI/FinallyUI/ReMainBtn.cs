using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ReMainBtn : MonoBehaviour
{

    public void Init(PanelFinallyUI panelMain,string text = null)
    {
        GetComponent<Button>().onClick.AddListener(ReMainEvent);

    }
    private void ReMainEvent()
    {
        Debug.Log("ReMainEvent");
        SceneManager.LoadScene(0);
        //重新加载游戏场景
        Time.timeScale = 1;
    }
}
