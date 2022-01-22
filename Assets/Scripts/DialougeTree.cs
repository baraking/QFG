using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialougeTree", menuName = "ScriptableObjects/DialougeTree")]
public class DialougeTree : ScriptableObject
{
    public string dialgoueTitle;
    [SerializeField] public List<string> dialougeContent;

    [SerializeField] public List<DialougeTree> dialougeOptions;
    public DialougeTree parentNode;
}
