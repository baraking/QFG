using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemIcon : MonoBehaviour
{
    public Item item;
    public Sprite sprite;

    private void Awake()
    {
        sprite = item.sprite;
    }
}
