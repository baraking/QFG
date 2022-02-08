using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{

    public static UI_Manager instance;

    public MessageBoard messageBoard;
    public DialougeOptions dialougeOptions;
    public GameObject dialougeOptionPrefab;

    public DialougeTree curDialougeTree;

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

    public bool IsUIManagerActive()
    {
        return (isMessageBoardOpen || isDialougeOptionsOpen);
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
        foreach (string line in newText)
        {
            Debug.Log(line);
        }

        if (isDialougeOptionsOpen)
        {
            CloseDialougeOptions();
            StartCoroutine(UIManagerWait());
        }
        
        CloseMessageBoard();
        yield return new WaitForSecondsRealtime(Constants.MESSAGE_BOARD_WAIT_TIME);
        OpenMessageBoard();
        messageBoard.SetNewText(newText);
        yield return new WaitForSecondsRealtime(Constants.MESSAGE_BOARD_WAIT_TIME);
    }

    public IEnumerator SetMessageOnMessageBoard(DialougeTree dialougeTree)
    {
        curDialougeTree = dialougeTree;

        messageBoard.SetNewText(dialougeTree.dialougeContent);

        foreach (string line in dialougeTree.dialougeContent)
        {
            Debug.Log(line);
        }

        if (isDialougeOptionsOpen)
        {
            CloseDialougeOptions();
            StartCoroutine(UIManagerWait());
        }

        CloseMessageBoard();

        //yield return new WaitForSecondsRealtime(Constants.MESSAGE_BOARD_WAIT_TIME);

        OpenMessageBoard();

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
                if (curDialougeTree != null)
                {
                    CloseMessageBoard();
                    OpenDialougeOptions();

                    foreach (DialougeTree dialougeTree in curDialougeTree.dialougeOptions)
                    {
                        GameObject newDialougeTreeOption = Instantiate(dialougeOptionPrefab);
                        newDialougeTreeOption.transform.SetParent(dialougeOptions.dialougeOptionsHolder.transform);
                        newDialougeTreeOption.GetComponent<DialougeOptionButton>().Init(dialougeTree);
                    }

                    if (ReferenceEquals(curDialougeTree.parentNode , null))
                    {
                        //add Exit
                        print("Add Exit");
                    }
                    else
                    {
                        //add Return
                        print("Add Return");
                    }
                }
                //if does
                //open dialougeOptions
                //for each dialougeTree in the list add dialgoueTitle's text as each button
                //fill with all options
                //if has parent add return, else add exit
            }
            else
            {
                messageBoard.ContinueText();
            }
            isInCooldown = false;
        }

    }

    public IEnumerator UIManagerWait()
    {
        if (!isInCooldown)
        {
            isInCooldown = true;
            yield return new WaitForSecondsRealtime(Constants.MESSAGE_BOARD_WAIT_TIME);
        }
        isInCooldown = false;
    }
}
