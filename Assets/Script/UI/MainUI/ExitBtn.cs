using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitBtn : MonoBehaviour
{
    PanelMainUI panelMainUI;
    public Text textAsset;

    public void Init(PanelMainUI panelMain, string text = null)
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
        // 在编辑器中停止播放
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 在实际的构建版本中退出
        Application.Quit();
#endif
    }
}
