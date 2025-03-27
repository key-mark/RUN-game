using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inspector : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject treasure;
    public Canvas succeed;
    public Canvas failure;
    public int count;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        uicontrol count = player.GetComponent<uicontrol>();
        HeroKnight hr = player.GetComponent<HeroKnight>();
        if(count.money<= 0)
        {
            failure.gameObject.SetActive(true);
            hr.playerdead();
        }
        Treasure trea = treasure.GetComponent<Treasure>();
        if(trea.succeed == true)
        {
            succeed.gameObject.SetActive(true);
        }
    }
}
