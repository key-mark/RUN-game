using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;     // 初始移动速度
    public float upspeed = 5.0f;   // 初始跳跃力度
    public float hight = 1.0f;     // 跳跃检测高度
    private Rigidbody2D myRigidbody;
    public TextMeshProUGUI MoneyText;
    public int money;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        money = 0;
        SetMoney();
    }

    public void SetMoney()
    {
        MoneyText.text=money.ToString();
    }

    void Update()
    {
        // **角色自动向右移动**
        transform.Translate(speed * Time.deltaTime, 0, 0);
        Debug.DrawRay(transform.position, Vector2.down * hight, Color.red);
        // **空格键跳跃**
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, hight, LayerMask.GetMask("ground")))
            {
               // Debug.Log("1212");
                myRigidbody.AddForce(Vector2.up * upspeed, ForceMode2D.Impulse);
            }
        }

        // **按下 A 键缩小，提升速度和跳跃力**
        if (transform.localScale.x > 0.2f && Input.GetKey(KeyCode.A))
        {
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            speed += 0.05f;
            upspeed += 0.05f;
        }

        // **按下 D 键变大，降低速度和跳跃力，但不能顶到天花板**
        if (transform.localScale.x <= 1.0f && Input.GetKey(KeyCode.D))
        {
            if (!Physics2D.Raycast(transform.position, Vector2.up, 0.5f * hight, LayerMask.GetMask("ceiling")))
            {
                transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
                speed -= 0.05f;
                upspeed -= 0.05f;
            }
        }
    }
}
