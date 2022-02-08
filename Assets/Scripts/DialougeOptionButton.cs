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

    public void TreeButton()
    {
        StartCoroutine(UI_Manager.instance.SetMessageOnMessageBoard(myDialougeTree));
    }

    public void ReturnButton()
    {

    }

    public void ExitButton()
    {

    }
}
