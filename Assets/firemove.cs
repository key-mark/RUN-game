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
            Debug.Log("死亡");
            uicontrol ui = player.GetComponent<uicontrol>();
            ui.money--;
            ui.SetMoney();
            HeroKnight hurt = player.GetComponent<HeroKnight>();
            hurt.playerhurt();
            death = false ;
        }
        if (isRun == true)
        {
            //玩家靠近绝对值距离小于4时火焰下落
            if (Mathf.Abs(player.transform.position.x - transform.position.x) < 4)
            {
                rb.gravityScale = 2; // 让物体受重力影响掉落
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
