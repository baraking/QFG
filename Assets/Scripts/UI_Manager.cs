using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{

    public static UI_Manager instance;

    #region messages
    [Header("Messages")]
    public MessageBoard messageBoard;
    public DialougeOptions dialougeOptions;
    public GameObject dialougeOptionPrefab;
    public GameObject returnDialougeOptionPrefab;
    public GameObject exitDialougeOptionPrefab;

    public DialougeTree curDialougeTree;

    public bool isMessageBoardOpen;
    public bool isDialougeOptionsOpen;

    bool isInCooldown;
    #endregion

    #region inventory
    [Header("Inventory")]
    public GameObject inventoryWindow;
    public GameObject inventoryItemPrefab;
    public GameObject inventoryGroupHolder;
    public bool isInventoryOpen;
    #endregion

    #region spells
    [Header("Spells")]
    public GameObject spellsBookWindow;
    public GameObject spellIconPrefab;
    public GameObject spellGroupHolder;
    public bool isSpellsBookOpen;
    #endregion

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
        print("Got message:");
        print(newText[0]);

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

        if (ReferenceEquals(curDialougeTree.dialougeOptions, null) || curDialougeTree.dialougeOptions.Count < 1) 
        {
            if (ReferenceEquals(curDialougeTree.parentNode, null))
            {
                CloseAndResetAllUIElements();
                return;
            }
            curDialougeTree = curDialougeTree.parentNode;
        }

        foreach (DialougeTree dialougeTree in curDialougeTree.dialougeOptions)
        {
            GameObject newDialougeTreeOption = Instantiate(dialougeOptionPrefab);
            newDialougeTreeOption.transform.SetParent(dialougeOptions.dialougeOptionsHolder.transform);
            newDialougeTreeOption.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            newDialougeTreeOption.GetComponent<DialougeOptionButton>().Init(dialougeTree);
        }

        if (ReferenceEquals(curDialougeTree.parentNode, null))
        {
            GameObject newDialougeTreeOption = Instantiate(exitDialougeOptionPrefab);
            newDialougeTreeOption.transform.SetParent(dialougeOptions.dialougeOptionsHolder.transform);
            newDialougeTreeOption.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        else
        {
            GameObject newDialougeTreeOption = Instantiate(returnDialougeOptionPrefab);
            newDialougeTreeOption.transform.SetParent(dialougeOptions.dialougeOptionsHolder.transform);
            newDialougeTreeOption.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
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

    public void ToggleInventory()
    {
        if (isInventoryOpen)
        {
            CloseInventory();
        }
        else
        {
            OpenInventory();
        }
    }

    public void OpenInventory()
    {
        isInventoryOpen = true;
        inventoryWindow.SetActive(true);
        MouseController.instance.SetCurHeroAction((int)MouseController.HeroAction.Grab);
        SetupInventory();
    }

    public void CloseInventory()
    {
        isInventoryOpen = false;
        inventoryWindow.SetActive(false);
        ResetInventory();
    }

    public void SetupInventory()
    {
        foreach (Item item in Inventory.instance.inventory)
        {
            GameObject newInventoryItem = Instantiate(inventoryItemPrefab);
            newInventoryItem.transform.SetParent(inventoryGroupHolder.transform);
            newInventoryItem.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            newInventoryItem.GetComponent<InventoryItem>().Init(item);
        }
    }

    public void ResetInventory()
    {
        foreach (Transform child in inventoryGroupHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void ToggleSpellsBook()
    {
        if (isSpellsBookOpen)
        {
            CloseSpellsBook();
        }
        else
        {
            OpenSpellsBook();
        }
    }

    public void OpenSpellsBook()
    {
        isSpellsBookOpen = true;
        spellsBookWindow.SetActive(true);
        MouseController.instance.SetCurHeroAction((int)MouseController.HeroAction.Grab);
        SetupSpellsBook();
    }

    public void CloseSpellsBook()
    {
        isSpellsBookOpen = false;
        spellsBookWindow.SetActive(false);
        ResetSpellsBook();
    }

    public void SetupSpellsBook()
    {
        foreach (Spell spell in SpellsBook.instance.spells)
        {
            GameObject newSpell = Instantiate(spellIconPrefab);
            newSpell.transform.SetParent(spellGroupHolder.transform);
            newSpell.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            newSpell.GetComponent<SpellHolder>().Init(spell);
        }
    }

    public void ResetSpellsBook()
    {
        foreach (Transform child in spellGroupHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void InspectItem(Item item)
    {
        StartCoroutine(SetMessageOnMessageBoard(item.itemDescription));
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
