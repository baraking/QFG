using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialougeOptionButton : MonoBehaviour
{
    public DialougeTree myDialougeTree;
    public TMP_Text myDisplayedText;

    public void Init(DialougeTree newDialougeTree)
    {
        myDialougeTree = newDialougeTree;
        myDisplayedText.text = myDialougeTree.dialgoueTitle;
    }

    public void SetOriginDialougeTree(DialougeTree newDialougeTree)
    {
        myDialougeTree = newDialougeTree;
    }

    public void TreeButton()
    {
        StartCoroutine(UI_Manager.instance.SetMessageOnMessageBoard(myDialougeTree));
    }

    public void ReturnButton()
    {
        UI_Manager.instance.curDialougeTree = myDialougeTree.parentNode;
        UI_Manager.instance.OpenDialougeOptionDirectly();
    }

    public void ExitButton()
    {
        UI_Manager.instance.CloseAndResetAllUIElements();
    }
}
