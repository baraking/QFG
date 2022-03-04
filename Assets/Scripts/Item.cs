using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item",menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public float weight;
    public int amount;
    public GameObject inGameObject;
    public Sprite sprite;
    public List<string> itemDescription;

    public void OnClick()
    {
        if (MouseController.instance.curHeroAction == MouseController.HeroAction.LookAt)
        {
            OnInspectItem();
        }
    }

    public void OnInspectItem()
    {
        UI_Manager.instance.SetMessageOnMessageBoard(itemDescription);
    }
}
