using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public bool succeed;
    void Start()
    {
        succeed = false;
    }
    public void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.CompareTag("Player"))
        {
            //Debug.Log("2222");
            succeed = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
