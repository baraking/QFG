using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "ScriptableObjects/Spell")]
public class Spell : ScriptableObject
{
    public string spellName;
    public Sprite sprite;
    public List<string> spellDescription;
}
