using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;     // ��ʼ�ƶ��ٶ�
    public float upspeed = 5.0f;   // ��ʼ��Ծ����
    public float hight = 1.0f;     // ��Ծ���߶�
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
        // **��ɫ�Զ������ƶ�**
        transform.Translate(speed * Time.deltaTime, 0, 0);
        Debug.DrawRay(transform.position, Vector2.down * hight, Color.red);
        // **�ո����Ծ**
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, hight, LayerMask.GetMask("ground")))
            {
               // Debug.Log("1212");
                myRigidbody.AddForce(Vector2.up * upspeed, ForceMode2D.Impulse);
            }
        }

        // **���� A ����С�������ٶȺ���Ծ��**
        if (transform.localScale.x > 0.2f && Input.GetKey(KeyCode.A))
        {
            transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            speed += 0.05f;
            upspeed += 0.05f;
        }

        // **���� D ����󣬽����ٶȺ���Ծ���������ܶ����컨��**
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
