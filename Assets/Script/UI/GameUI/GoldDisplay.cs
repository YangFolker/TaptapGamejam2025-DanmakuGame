using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
// using TMPro; // 如果你用TMP就把 Text 换成 TextMeshProUGUI

public class GoldDisplay : MonoBehaviour
{
    public Text coinText; // 或 TextMeshProUGUI coinText;
    private int displayedCoins = 0;    // 屏幕上“正在显示”的值
    private int lastTargetCoins = 0;   // 上一次补间目标
    private Tween coinTween;

    void Start()
    {
        // 初始同步
        lastTargetCoins = displayedCoins = GameManger.instance.money;
        if (coinText) coinText.text = displayedCoins.ToString();
    }

    void Update()
    {
        int target = GameManger.instance.money;

        // 目标没变：不做任何事，避免反复重启
        if (target == lastTargetCoins) return;

        // 目标变了：从“当前显示值”平滑到新目标
        StartCoinsTween(displayedCoins, target, 0.5f);
        lastTargetCoins = target;
    }

    private void StartCoinsTween(int from, int to, float duration)
    {
        if (coinTween != null && coinTween.IsActive()) coinTween.Kill();

        coinTween = DOVirtual.Int(from, to, duration, v =>
        {
            displayedCoins = v;                 // 实时更新“显示值”
            if (coinText) coinText.text = v.ToString();
        })
        .SetEase(Ease.OutQuad)
        .SetUpdate(true)                        // 暂停界面 timeScale=0 时也能动；不需要可改为 false
        .OnComplete(() => displayedCoins = to); // 收尾对齐
    }
}
