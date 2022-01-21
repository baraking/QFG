using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{

    public static UI_Manager instance;
    public MessageBoard messageBoard;

    public bool isMessageBoardOpen;

    bool isInCooldown;

    public void Awake()
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

    public void OpenMessageBoard()
    {
        messageBoard.gameObject.SetActive(true);
        isMessageBoardOpen = true;
    }

    public void CloseMessageBoard()
    {
        messageBoard.gameObject.SetActive(false);
        isMessageBoardOpen = false;
    }

    public IEnumerator SetMessageOnMessageBoard(List<string> newText)
    {
        foreach(string line in newText)
        {
            Debug.Log(line);
        }

        CloseMessageBoard();
        yield return new WaitForSecondsRealtime(Constants.conversationWaitTime);
        OpenMessageBoard();
        messageBoard.SetNewText(newText);
        yield return new WaitForSecondsRealtime(Constants.conversationWaitTime);
    }

    public IEnumerator ContinueMessageOnMessageBoard()
    {
        if (!isInCooldown)
        {
            isInCooldown = true;
            yield return new WaitForSecondsRealtime(Constants.conversationWaitTime);
            if (messageBoard.IsLastSentenceInText())
            {
                messageBoard.ResetText();
                CloseMessageBoard();
            }
            else
            {
                messageBoard.ContinueText();
            }
            isInCooldown = false;
        }
    }
}
