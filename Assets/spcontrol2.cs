using System.Collections;
using UnityEngine;

public class spcontrol2 : MonoBehaviour
{
    public float downDistance = -1f;  // 尖刺升起的高度
    public float speed = 2f;         // 上升速度
    public float delayTime = 2f;     // 多久触发一次
    private Vector3 startPos;
    private bool isUp = false;
    public bool death = false;
    public GameObject player;
    void Start()
    {
        startPos = transform.position;
        StartCoroutine(AutoTrigger());
        
    }
    void Update()
    {
        if (death == true)
        {
            Debug.Log("死亡");
            uicontrol ui = player.GetComponent<uicontrol>();
            ui.money--;
            ui.SetMoney();
            HeroKnight hurt = player.GetComponent<HeroKnight>();
            hurt.playerhurt();
            death = false;
        }
    }
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            death = true;
        }
    }
    IEnumerator AutoTrigger()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayTime);
            if (!isUp)
            {
                yield return MoveSpike(startPos + Vector3.up * downDistance);
                isUp = true;
            }
            else
            {
                yield return MoveSpike(startPos);
                isUp = false;
            }
        }
    }

    IEnumerator MoveSpike(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }
    }
}
