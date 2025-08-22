using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitBtn : MonoBehaviour
{
    PanelMainUI panelMainUI;
    public Text textAsset;

    public void Init(PanelMainUI panelMain,string text = null)
    {
        GetComponent<Button>().onClick.AddListener(ExitEvent);
        panelMainUI = panelMain;
        if (text != null)
        {
            textAsset.text = text;
        }
    }
    private void ExitEvent()
    {
        Debug.Log("ExitEvent");
        Application.Quit();
    }
}
