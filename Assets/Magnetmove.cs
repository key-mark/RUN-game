using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Magnetmove : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsFollow;
    public GameObject player;
    //�ҵ�Ѫ����
    public Canvas magnetHP;
    //�ҵ�HPͼƬ
    public Image HpPhoto;
    //HP��ֵ
    public float Hp;
    void Start()
    {
        Hp = 1;
        IsFollow = false;

    }
    public void OnTriggerEnter2D(Collider2D coll)
    {
        //Debug.Log("3333");
        if (coll.gameObject.CompareTag("coin"))
        {
            //Debug.Log("1212");
            coll.GetComponent<heartmove>().IsRun = true;
        }
        if (coll.gameObject.CompareTag("Player"))
        {
            //Debug.Log("2222");
            IsFollow = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (IsFollow == true)
        {
            transform.position = new Vector3(player.transform.position.x - 0.5f, player.transform.position.y + 1.5f, transform.position.z);
            magnetHP.gameObject.SetActive(true);
            Hp = Hp - 0.0005f;
            if (Hp < 0)
            {
                Hp = 0;
                Destroy(gameObject);     //���ٴ���
                //Destroy(magnetHP);      //����Ѫ��
                magnetHP.gameObject.SetActive(false);
            }

            // ���� UI Ѫ�����
            float maxHpWidth = 120f;  // ����Ѫ����Ѫʱ����� 200
            HpPhoto.rectTransform.sizeDelta = new Vector2(Hp * maxHpWidth, HpPhoto.rectTransform.sizeDelta.y);
        }
    }
}
