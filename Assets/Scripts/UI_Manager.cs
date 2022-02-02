using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{

    public static UI_Manager instance;

    public MessageBoard messageBoard;
    public DialougeOptions dialougeOptions;
    public GameObject dialougeOptionPrefab;

    public bool isMessageBoardOpen;
    public bool isDialougeOptionsOpen;

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

    public void OpenDialougeOptions()
    {
        dialougeOptions.gameObject.SetActive(true);
        isDialougeOptionsOpen = true;
    }

    public void CloseDialougeOptions()
    {
        dialougeOptions.gameObject.SetActive(false);
        isDialougeOptionsOpen = false;
    }

    public IEnumerator SetMessageOnMessageBoard(List<string> newText)
    {
        foreach(string line in newText)
        {
            Debug.Log(line);
        }

        CloseMessageBoard();
        yield return new WaitForSecondsRealtime(Constants.MESSAGE_BOARD_WAIT_TIME);
        OpenMessageBoard();
        messageBoard.SetNewText(newText);
        yield return new WaitForSecondsRealtime(Constants.MESSAGE_BOARD_WAIT_TIME);
    }

    public IEnumerator ContinueMessageOnMessageBoard()
    {
        if (!isInCooldown)
        {
            isInCooldown = true;
            yield return new WaitForSecondsRealtime(Constants.MESSAGE_BOARD_WAIT_TIME);
            if (messageBoard.IsLastSentenceInText())
            {
                messageBoard.ResetText();
                CloseMessageBoard();

                //check if has dialouge
                //if does
                //open dialougeOptions
                //for each dialougeTree in the list add dialgoueTitle's text as each button
                //fille with all options
                //if has parent add return, else add exit
            }
            else
            {
                messageBoard.ContinueText();
            }
            isInCooldown = false;
        }
    }
}
