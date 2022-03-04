using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellHolder : MonoBehaviour
{
    public Spell spell;
    public Image myIcon;

    public void Init(Spell newSpell)
    {
        spell = newSpell;
        myIcon.sprite = spell.sprite;
    }

    public void OnClick()
    {
        MouseController.HeroAction curAction = MouseController.instance.curHeroAction;
        if (curAction == MouseController.HeroAction.LookAt)
        {
            OnInspectSpell();
        }
        else if (curAction == MouseController.HeroAction.Grab)
        {
            OnSelectSpell();
        }
    }

    public void OnInspectSpell()
    {
        StartCoroutine(UI_Manager.instance.SetMessageOnMessageBoard(spell.spellDescription));
    }

    public void OnSelectSpell()
    {
        MouseController.instance.curSpell = this;
        MouseController.instance.curItem = null;

        MouseController.instance.SetCurHeroAction((int)MouseController.HeroAction.UseItem);
    }
}
