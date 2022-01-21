using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageBoard : MonoBehaviour
{
    public TMP_Text currentTextDisplayer;

    List<string> currentText;
    int currentTextIndex;

    public void SetNewText(List<string> newText)
    {
        currentText = newText;
        currentTextIndex = 0;
        SetDisplayedText(newText[currentTextIndex]);
    }

    public void ResetText()
    {
        currentText = null;
        currentTextIndex = -1;
    }

    public void ContinueText()
    {
        currentTextIndex++;
        SetDisplayedText(currentText[currentTextIndex]);
    }

    public void SetDisplayedText(string newText)
    {
        currentTextDisplayer.text = newText;
    }

    public bool IsLastSentenceInText()
    {
        return (currentTextIndex + 1 >= currentText.Count);
    }
}
