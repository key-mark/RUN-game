using UnityEngine;
using System.Collections.Generic;
using System;

public class ColorSwap_HeroKnight : MonoBehaviour
{
    // 在编辑器中可访问的颜色数组
    [SerializeField] Color[] m_sourceColors; // 原始颜色数组
    [SerializeField] Color[] m_newColors;    // 替换后的颜色数组

    // 私有成员变量
    Texture2D m_colorSwapTex;   // 颜色替换纹理
    Color[] m_spriteColors;     // 精灵的颜色数组
    SpriteRenderer m_spriteRenderer; // 精灵渲染组件
    bool m_init = false;        // 是否已初始化
    private Dictionary<int, Color> originalColors = new Dictionary<int, Color>(); // 存储原始颜色


    // 初始化
    void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>(); // 获取 SpriteRenderer 组件
        InitColorSwapTex(); // 初始化颜色替换纹理

        SwapDemoColors(); // 执行颜色替换

    }

    // 当在编辑器中修改 m_sourceColors 或 m_newColors 时调用（仅在运行模式下可实时更改）
    //OnValidate() 是 Unity 编辑器 提供的一个特殊回调方法，它的作用是在 脚本的 Inspector 面板中的变量发生更改时自动执行，无需运行游戏。
    private void OnValidate()
    {
        if (m_init)
        {
            SwapDemoColors(); // 重新应用颜色替换
        }
    }

    // 使用原始颜色的红色通道值（0-255）作为索引，在颜色交换纹理中放置新的颜色
    public void SwapDemoColors()
    {
        for (int i = 0; i < m_sourceColors.Length && i < m_newColors.Length; i++)
        {
            SwapColor((int)(m_sourceColors[i].r * 255.0f), m_newColors[i]); // 替换颜色
        }
        if (m_colorSwapTex)
            m_colorSwapTex.Apply(); // 应用颜色替换
    }

    // 通过整数值创建颜色（带有透明度）
    public static Color ColorFromInt(int c, float alpha = 1.0f)
    {
        int r = (c >> 16) & 0x000000FF; // 提取红色通道
        int g = (c >> 8) & 0x000000FF;  // 提取绿色通道
        int b = c & 0x000000FF;         // 提取蓝色通道

        Color ret = ColorFromIntRGB(r, g, b);
        ret.a = alpha; // 设置透明度

        return ret;
    }

    // 通过 RGB 数值创建颜色
    public static Color ColorFromIntRGB(int r, int g, int b)
    {
        return new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, 1.0f);
    }

    // 初始化颜色替换纹理
    public void InitColorSwapTex()
    {
        Texture2D colorSwapTex = new Texture2D(256, 1, TextureFormat.RGBA32, false, false);
        colorSwapTex.filterMode = FilterMode.Point; // 设置纹理过滤模式为点采样（像素风格）

        // 初始化纹理的所有像素为透明
        for (int i = 0; i < colorSwapTex.width; ++i)
            colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));

        colorSwapTex.Apply(); // 应用更改

        m_spriteRenderer.material.SetTexture("_SwapTex", colorSwapTex); // 赋值给精灵的材质

        m_spriteColors = new Color[colorSwapTex.width]; // 存储精灵颜色
        m_colorSwapTex = colorSwapTex;
        m_init = true; // 标记初始化完成
    }

    // 替换指定索引处的颜色
    public void SwapColor(int index, Color color)
    {
        if (index >= 0 && index < 256)
        {
            m_spriteColors[index] = color;
            m_colorSwapTex.SetPixel(index, 0, color);
        }
    }

    // 批量替换颜色
    public void SwapColors(List<int> indexes, List<Color> colors)
    {
        for (int i = 0; i < indexes.Count; ++i)
        {
            m_spriteColors[indexes[i]] = colors[i];
            m_colorSwapTex.SetPixel(indexes[i], 0, colors[i]);
        }
        m_colorSwapTex.Apply(); // 应用颜色更改
    }

    // 清除指定索引的颜色
    public void ClearColor(int index)
    {
        Color c = new Color(0.0f, 0.0f, 0.0f, 0.0f); // 透明颜色
        m_spriteColors[index] = c;
        m_colorSwapTex.SetPixel(index, 0, c);
    }

    // 暂时将所有精灵颜色替换为指定颜色
    public void SwapAllSpritesColorsTemporarily(Color color)
    {
        for (int i = 0; i < m_colorSwapTex.width; ++i)
            m_colorSwapTex.SetPixel(i, 0, color);
        m_colorSwapTex.Apply();
    }

    // 恢复所有精灵的原始颜色
    public void ResetAllSpritesColors()
    {
        for (int i = 0; i < m_colorSwapTex.width; ++i)
            m_colorSwapTex.SetPixel(i, 0, m_spriteColors[i]);
        m_colorSwapTex.Apply();
    }

    // 清除所有精灵颜色，使其变为透明
    public void ClearAllSpritesColors()
    {
        for (int i = 0; i < m_colorSwapTex.width; ++i)
        {
            m_colorSwapTex.SetPixel(i, 0, new Color(0.0f, 0.0f, 0.0f, 0.0f));
            m_spriteColors[i] = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        m_colorSwapTex.Apply();
    }

    public void SwapToLanceColor()
    {
        Color lanceBlue = new Color(0.1f, 0.1f, 0.44f, 1.0f);
        List<int> colorIndexes = new List<int>();
        List<Color> colors = new List<Color>();

        for (int i = 0; i < 256; i++)
        {
            if (!originalColors.ContainsKey(i)) // 只存一次原始颜色
            {
                originalColors[i] = m_colorSwapTex.GetPixel(i, 0);
            }

            colorIndexes.Add(i);
            colors.Add(lanceBlue);
        }

        SwapColors(colorIndexes, colors);
        Debug.Log("已成功替换颜色为兰斯蓝！");
    }

    public void RestoreOriginalColors()
    {
        if (originalColors.Count == 0)
        {
            Debug.LogWarning("没有存储的原始颜色，无法恢复！");
            return;
        }

        List<int> colorIndexes = new List<int>();
        List<Color> colors = new List<Color>();

        foreach (var pair in originalColors)
        {
            colorIndexes.Add(pair.Key);
            colors.Add(pair.Value);
        }

        SwapColors(colorIndexes, colors);
        originalColors.Clear(); // 清空原始颜色，防止重复调用问题

        Debug.Log("颜色已恢复！");
    }

}
