using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class heartmove : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsRun;
    public GameObject player;
    public Image AllCoin;  // ֱ������ Image ���
    private Vector3 UIcoin;
    //�����Ŀ����ƶ�
    private void Start()
    {
        IsRun = false;
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player == null)
            {
                Debug.LogError("�Ҳ������� 'Player' ��ǩ�����壬�����Ƿ���ȷ�����˱�ǩ��");
            }
        }
    }

    public void CoinMove()
    {
        // UI ����ת������������
        UIcoin = Camera.main.ScreenToWorldPoint(AllCoin.transform.position);

        // ��ǰ������Ŀ����ƶ�
        transform.position = Vector3.MoveTowards(transform.position, UIcoin + Vector3.forward, 25 * Time.deltaTime);

        // ��������֮��ľ���
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
                    Debug.LogError("player ������û�й��� uicontrol �����");
                }
            }
            else
            {
                Debug.LogError("player Ϊ�գ�");
            }

            Destroy(gameObject);
        }
    }

    void Update()
    {
        transform.Rotate(Vector3.up * 2, Space.World);    //ԭ����ת

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
