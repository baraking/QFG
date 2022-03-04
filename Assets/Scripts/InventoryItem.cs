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
        StartCoroutine(UI_Manager.instance.SetMessageOnMessageBoard(item.itemDescription));
    }

    public void OnSelectItem()
    {
        MouseController.instance.curItem = this;
        MouseController.instance.SetCurHeroAction((int)MouseController.HeroAction.UseItem);
    }
}
