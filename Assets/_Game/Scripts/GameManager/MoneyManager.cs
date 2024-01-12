using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public TMPro.TMP_Text Text;
    public int StartingMoney;
    int money;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        money = 0;
        GainMoney(StartingMoney);
    }

    public void GainMoney(int amount)
    {
        money += amount;
        ShowMoney();
    }

    public bool Purchase(int cost)
    {
        if (money < cost)
            return false;
        money -= cost;
        ShowMoney();
        return true;
    }

    public int GetMoney()
    {
        return money;
    }

    void ShowMoney()
    {
        Text.text = money + " $";
    }
}
