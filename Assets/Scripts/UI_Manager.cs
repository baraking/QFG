using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{

    public static UI_Manager instance;

    public MessageBoard messageBoard;
    public DialougeOptions dialougeOptions;
    public GameObject dialougeOptionPrefab;
    public GameObject returnDialougeOptionPrefab;
    public GameObject exitDialougeOptionPrefab;

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

    public void CloseAndResetAllUIElements()
    {
        messageBoard.ResetText();
        CloseMessageBoard();
        dialougeOptions.ResetOptions();
        CloseDialougeOptions();
        curDialougeTree = null;
    }

    public IEnumerator SetMessageOnMessageBoard(List<string> newText)
    {
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

        if(!ReferenceEquals(curDialougeTree, null))
        {
            messageBoard.SetNewText(curDialougeTree.dialougeContent);
        }


        if (isDialougeOptionsOpen)
        {
            CloseDialougeOptions();
            StartCoroutine(UIManagerWait());
        }

        CloseMessageBoard();

        //yield return new WaitForSecondsRealtime(Constants.MESSAGE_BOARD_WAIT_TIME);

        OpenMessageBoard();
        if (ReferenceEquals(dialougeTree, null))
        {
            List<string> defaultTalkTo = new List<string>();
            defaultTalkTo.Add(Constants.DEFAULT_TALK_TO_MESSAGE);
            messageBoard.SetNewText(defaultTalkTo);
        }
        yield return new WaitForSecondsRealtime(Constants.MESSAGE_BOARD_WAIT_TIME);
    }

    public void OpenDialougeOptionDirectly()
    {
        CloseMessageBoard();
        OpenDialougeOptions();

        dialougeOptions.ResetOptions();

        foreach (DialougeTree dialougeTree in curDialougeTree.dialougeOptions)
        {
            GameObject newDialougeTreeOption = Instantiate(dialougeOptionPrefab);
            newDialougeTreeOption.transform.SetParent(dialougeOptions.dialougeOptionsHolder.transform);
            newDialougeTreeOption.GetComponent<DialougeOptionButton>().Init(dialougeTree);
        }

        if (ReferenceEquals(curDialougeTree.parentNode, null))
        {
            GameObject newDialougeTreeOption = Instantiate(exitDialougeOptionPrefab);
            newDialougeTreeOption.transform.SetParent(dialougeOptions.dialougeOptionsHolder.transform);
        }
        else
        {
            GameObject newDialougeTreeOption = Instantiate(returnDialougeOptionPrefab);
            newDialougeTreeOption.transform.SetParent(dialougeOptions.dialougeOptionsHolder.transform);
            newDialougeTreeOption.GetComponent<DialougeOptionButton>().SetOriginDialougeTree(curDialougeTree);
        }
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
                    OpenDialougeOptionDirectly();
                }
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
