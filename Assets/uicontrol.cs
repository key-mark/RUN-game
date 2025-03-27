using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class uicontrol : MonoBehaviour
{

    public TextMeshProUGUI MoneyText;
    public int money;
    void Start()
    {
        money = 5;
        SetMoney();
    }

    public void SetMoney()
    {
        MoneyText.text = money.ToString();
    }

}
