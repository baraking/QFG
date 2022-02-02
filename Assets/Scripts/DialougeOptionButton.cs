using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialougeOptionButton : MonoBehaviour
{
    DialougeTree myDialougeTree;
    public TMP_Text myDisplayedText;

    public void Init(DialougeTree newDialougeTree)
    {
        myDialougeTree = newDialougeTree;
        myDisplayedText.text = myDialougeTree.dialgoueTitle;
    }
}
