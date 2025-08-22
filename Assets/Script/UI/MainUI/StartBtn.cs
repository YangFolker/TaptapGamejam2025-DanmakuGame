using UnityEngine;
using UnityEngine.UI;
public class StartBtn : MonoBehaviour
{
    PanelMainUI panelMainUI;
    public Text textAsset;

    public void Init(PanelMainUI panelMain,string text = null)
    {
        GetComponent<Button>().onClick.AddListener(StartEvent);
        panelMainUI = panelMain;
        if (text != null)
        {
            textAsset.text = text;
        }

    }
    private void StartEvent()
    {
        Debug.Log("StartEvent");
        panelMainUI.LoadGame(1);
        //隐藏主界面
    }
}
