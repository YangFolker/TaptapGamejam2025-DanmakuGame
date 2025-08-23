using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;  // 单例实例
    public Image healthBar;      // 血条 Image（需要在 Inspector 中拖拽进去）
    public RectTransform healthMarker;  // 血条浮标（需要在 Inspector 中拖拽进去）

    public float offect;

    void Awake()
    {
        offect = healthBar.GetComponent<RectTransform>().rect.height / 2;
        // 确保单例初始化
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // 保证该实例在场景切换时不被销毁
        }
        else
        {
            Destroy(gameObject);  // 如果已有实例，销毁当前对象
        }
    }

    void Start()
    {
        GameManger.instance.currentHealth = GameManger.instance.maxHealth/2;  // 初始化当前血量

        // 初始化浮标位置
        float initialPositionY = Mathf.Lerp(-offect, offect, (float)GameManger.instance.currentHealth / GameManger.instance.maxHealth);  // 根据初始血量计算浮标位置
        healthMarker.anchoredPosition = new Vector2(healthMarker.anchoredPosition.x, initialPositionY);  // 设置浮标初始位置
    }

    /// <summary>
    /// 受到伤害或恢复血量（外部调用接口）
    /// </summary>
    /// <param name="amount">血量变化数值（正值为回血，负值为掉血）</param>
    /// <param name="isUp">是否为回血</param>
    public void TakeDamage(int amount, bool isUp)
    {
        if (isUp)
        {
            GameManger.instance.currentHealth += amount;  // 回血
        }
        else
        {
            GameManger.instance.currentHealth -= amount;  // 掉血
        }
        if (GameManger.instance.currentHealth <= 0 || GameManger.instance.currentHealth >= GameManger.instance.maxHealth)
        {
            GameManger.instance.GameOver();
        }

        GameManger.instance.currentHealth = Mathf.Clamp(GameManger.instance.currentHealth, 0, GameManger.instance.maxHealth);  // 确保血量不小于0，也不大于最大血量

        // 计算血条填充量（比例）
        float targetFillAmount = (float)GameManger.instance.currentHealth / GameManger.instance.maxHealth;

        // 使用 DOTween 平滑过渡血条变化
        healthBar.DOFillAmount(targetFillAmount, 0.5f);

        // 根据血量百分比平滑浮标移动
        float targetPositionY = Mathf.Lerp(-offect, offect, targetFillAmount);  // 这里 -50f 和 50f 是浮标位置的上下范围，你可以调整

        // 确保浮标的动画每次都重新启动
        healthMarker.DOKill();  // 先停止之前的动画，避免重叠
        healthMarker.DOAnchorPosY(targetPositionY, 0.5f);  // 使用 DOTween 平滑过渡浮标位置
    }
}
