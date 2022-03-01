using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public Item item;
    public Image myIcon;

    public void Init(Item newItem)
    {
        item = newItem;
        myIcon.sprite = item.sprite;
    }
}
