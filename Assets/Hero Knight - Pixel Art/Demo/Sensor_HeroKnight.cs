using UnityEngine;
using System.Collections;

public class Sensor_HeroKnight : MonoBehaviour {

    private int m_ColCount = 0;//// 记录当前与传感器碰撞的物体数量

    private float m_DisableTimer;// 传感器禁用时间（倒计时）

    private void OnEnable()
    {
        m_ColCount = 0;
    }

    public bool State()
    {
        if (m_DisableTimer > 0)
            return false;
        return m_ColCount > 0;//// 如果m_ColCount>0，说明有物体碰撞，返回true（检测到地面或墙壁）
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        m_ColCount++;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        m_ColCount--;
    }

    void Update()
    {
        m_DisableTimer -= Time.deltaTime;
    }

    //这个方法用于 手动禁用传感器，并在 duration 秒后恢复。
    //例如，HeroKnight 角色起跳时，地面传感器会被禁用一段时间，以防止在起跳瞬间被错误判定为“仍然接触地面”：
    public void Disable(float duration)
    {
        m_DisableTimer = duration;
    }
}
