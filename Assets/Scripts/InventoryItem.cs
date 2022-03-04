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

    public void OnClick()
    {
        MouseController.HeroAction curAction = MouseController.instance.curHeroAction;
        if (curAction == MouseController.HeroAction.LookAt)
        {
            OnInspectItem();
        }
        else if (curAction == MouseController.HeroAction.Grab)
        {
            OnSelectItem();
        }
    }

    public void OnInspectItem()
    {
        List<string> description = new List<string>();
        foreach(string str in item.itemDescription)
        {
            description.Add(str);
        }
        description[description.Count - 1] += QuanitiveDescription();

        StartCoroutine(UI_Manager.instance.SetMessageOnMessageBoard(description));
    }

    public void OnSelectItem()
    {
        MouseController.instance.curItem = this;
        MouseController.instance.SetCurHeroAction((int)MouseController.HeroAction.UseItem);
    }

    public string QuanitiveDescription()
    {
        return "You currntly have " + item.amount + " and it weighs " + (item.amount * item.weight);
    }
}
