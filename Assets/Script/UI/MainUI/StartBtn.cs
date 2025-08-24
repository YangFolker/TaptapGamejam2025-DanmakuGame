using UnityEngine;
using UnityEngine.UI;
public class StartBtn : MonoBehaviour
{
    PanelMainUI panelMainUI;

    public void Init(PanelMainUI panelMain,string text = null)
    {
        GetComponent<Button>().onClick.AddListener(StartEvent);
        panelMainUI = panelMain;

    }
    private void StartEvent()
    {
        Debug.Log("StartEvent");
        panelMainUI.LoadGame(1);
        //隐藏主界面
    }
}
