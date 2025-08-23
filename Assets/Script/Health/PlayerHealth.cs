using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;  // 单例实例
    public Image healthBar;      // 增加血量的血条
    public Image healthBar1;     // 减少血量的血条
    public RectTransform healthMarker;  // 增加血量的浮标
    public RectTransform healthMarker1;  // 减少血量的浮标

    private float timer = 0f;  // 计时器，用来控制每秒更新一次

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
        GameManger.instance.currentHealth_down = GameManger.instance.maxHealth/2;  // 初始化减少的血量

        // 初始化浮标位置
        float initialPositionY = Mathf.Lerp(-offect, offect, (float)GameManger.instance.currentHealth / GameManger.instance.maxHealth);  // 根据初始血量计算浮标位置
        healthMarker.anchoredPosition = new Vector2(healthMarker.anchoredPosition.x, initialPositionY);  // 设置浮标初始位置

        initialPositionY = Mathf.Lerp(-offect, offect, (float)GameManger.instance.currentHealth_down / GameManger.instance.maxHealth);  // 设置减少血量的浮标初始位置
        healthMarker1.anchoredPosition = new Vector2(healthMarker1.anchoredPosition.x, initialPositionY);  // 设置浮标初始位置
    }
    void Update()
    {
        // 每秒更新一次血量
        timer += Time.deltaTime;
        if (timer >= 1f)  // 每秒触发一次
        {
            timer = 0f;

            // 如果 currentHealth 大于 50，每秒减少 1
            if (GameManger.instance.currentHealth > 50)
            {
                GameManger.instance.currentHealth -= 1;  // 通过掉血来减少
            }

            // 如果 currentHealth_down 小于 50，每秒增加 1
            if (GameManger.instance.currentHealth_down < 50)
            {
                GameManger.instance.currentHealth_down += 1;  // 通过回血来增加
            }
        }
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
            // 回血
            GameManger.instance.currentHealth += amount;
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
        else
        {
            // 掉血
            GameManger.instance.currentHealth_down -= amount;
            GameManger.instance.currentHealth_down = Mathf.Clamp(GameManger.instance.currentHealth_down, 0, GameManger.instance.maxHealth);  // 确保掉血量不小于0，也不大于最大血量

            // 计算血条填充量（比例）
            float targetFillAmount = (float)GameManger.instance.currentHealth_down / GameManger.instance.maxHealth;

            // 使用 DOTween 平滑过渡血条变化
            healthBar1.DOFillAmount(targetFillAmount, 0.5f);

            // 根据血量百分比平滑浮标移动
            float targetPositionY = Mathf.Lerp(-offect, offect, targetFillAmount);  // 这里 -50f 和 50f 是浮标位置的上下范围，你可以调整

            // 确保浮标的动画每次都重新启动
            healthMarker1.DOKill();  // 先停止之前的动画，避免重叠
            healthMarker1.DOAnchorPosY(targetPositionY, 0.5f);  // 使用 DOTween 平滑过渡浮标位置
        }

        // 游戏结束判断
        if (GameManger.instance.currentHealth >= GameManger.instance.maxHealth || GameManger.instance.currentHealth_down <= 0)
        {
            GameManger.instance.GameOver();
        }
    }
}
