using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class heartmove : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsRun;
    public GameObject player;
    public Image AllCoin;  // 直接声明 Image 组件
    private Vector3 UIcoin;
    //金币向目标点移动
    private void Start()
    {
        IsRun = false;
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("找不到带有 'Player' 标签的物体，请检查是否正确设置了标签！");
            }
        }
    }

    public void CoinMove()
    {
        // UI 坐标转换成世界坐标
        UIcoin = Camera.main.ScreenToWorldPoint(AllCoin.transform.position);

        // 当前物体向目标点移动
        transform.position = Vector3.MoveTowards(transform.position, UIcoin + Vector3.forward, 25 * Time.deltaTime);

        // 计算两点之间的距离
        if ((transform.position - (UIcoin + Vector3.forward)).sqrMagnitude < 0.1f)
        {
            if (player != null)
            {
                uicontrol ui = player.GetComponent<uicontrol>();
                if (ui != null)
                {
                    ui.money++;
                    ui.SetMoney();
                }
                else
                {
                    Debug.LogError("player 物体上没有挂载 uicontrol 组件！");
                }
            }
            else
            {
                Debug.LogError("player 为空！");
            }

            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 2, Space.World);    //原地旋转

        if (IsRun == true)
        {
            CoinMove();
        }
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            IsRun = true;
        }
    }
}
