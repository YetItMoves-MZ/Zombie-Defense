using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }
    int money;

    private void Awake()
    {
        Instance = this;
    }

    public void GainMoney(int amount)
    {
        money += amount;
    }

    public bool Purchase(int cost)
    {
        if (money < cost)
            return false;
        money -= cost;
        return true;
    }

    public int GetMoney()
    {
        return money;
    }
}
