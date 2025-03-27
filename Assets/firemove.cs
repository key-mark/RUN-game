using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firemove : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isRun;
    public bool death;
    public GameObject player;
    Rigidbody2D rb;
    private void Start()
    {
        isRun = true;
        death = false;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(death==true)
        {
            Debug.Log("����");
            uicontrol ui = player.GetComponent<uicontrol>();
            ui.money--;
            ui.SetMoney();
            HeroKnight hurt = player.GetComponent<HeroKnight>();
            hurt.playerhurt();
            death = false ;
        }
        if (isRun == true)
        {
            //��ҿ�������ֵ����С��4ʱ��������
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < 4)
            {
                rb.gravityScale = 2; // ������������Ӱ�����
            }
        }
        if(isRun == false) 
        {
            //Debug.Log("1212");
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("ground"))
        {
            isRun = false;
        }
        if (coll.gameObject.CompareTag("Player"))
        {
            death = true;
        }
    }

}
