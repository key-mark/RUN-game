using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watercollider : MonoBehaviour
{
    // Start is called before the first frame update
    public bool magicchange;
    public GameObject wall;
    public GameObject player;
    private void Start()
    {
        magicchange = false;
    }
    public void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.CompareTag("Player"))
        {
            //Debug.Log("2222");
            magicchange = true;
        }
    }
    // Update is called once per frame

    private void Update()
    {
        if (magicchange)
        {
            wall.GetComponent<BoxCollider2D>().isTrigger = true;
            ColorSwap_HeroKnight colorSwap = player.GetComponent<ColorSwap_HeroKnight>();
            colorSwap.SwapToLanceColor();
            magicchange = false;
        }
    }
}
