using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallinspect : MonoBehaviour
{
    // Start is called before the first frame update
    public bool changeshu = false;
    public GameObject player;
    void Start()
    {
        
    }
    //在人物离开水墙时，更换为初始属性
    public void OnTriggerExit2D(Collider2D coll)
    {

        if (coll.gameObject.CompareTag("Player"))
        {
            //Debug.Log("2222");
            changeshu = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(changeshu==true)
        {
            ColorSwap_HeroKnight colorSwap = player.GetComponent<ColorSwap_HeroKnight>();
            colorSwap.RestoreOriginalColors();
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
            changeshu=false;
        }
    }
}
