using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsBook : MonoBehaviour
{
    public static SpellsBook instance;

    public List<Spell> spells = new List<Spell>();

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
}
