using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<Item> inventory = new List<Item>();
    public Item moneyBag;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (ReferenceEquals(moneyBag, null))
        {
            Debug.LogWarning("Missing Money Item");
        }
    }

    public int GetMoneyAmount()
    {
        return moneyBag.amount;
    }

    public void UpdateMoneyBy(int amount)
    {
        if (moneyBag.amount + amount >= 0)
        {
            moneyBag.amount += amount;
        }
        else
        {
            Debug.LogWarning("Negative amount money transaction was aborted");
        }
    }
}
