using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updown : MonoBehaviour
{
    // Start is called before the first frame update
    public bool IsChange;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 0)
        {
            IsChange = false;
        }
        if (transform.position.y < -6)
        {
            IsChange = true;
        }
        if (IsChange == false)
        {
            //在世界坐标向下移动
            transform.Translate(0, -4 * Time.deltaTime, 0, Space.World);
        }
        else
        {
            //在世界坐标向上移动
            transform.Translate(0, 4 * Time.deltaTime, 0, Space.World);
        }
    }
}
