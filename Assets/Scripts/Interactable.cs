using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Queue<string>))]
public class Interactable : MonoBehaviour
{
    [SerializeField] public List<string> lookAt;
    [SerializeField] public List<string> grab;
    [SerializeField] public DialougeTree talkTo;

    public float lookAtDistance;
    public float grabDistance;
    public float talkToDistance;

    public List<Item> holdsItems;
    public bool shouldDisappearAfterItemsAreTaken;

    //response to Item
    //response to Spell
    //condition to getItems

    public void Start()
    {
        if (lookAtDistance < 0)
        {
            lookAtDistance = Constants.INTERACTION_DISTANCE_LOOK_AT;
        }

        if (grabDistance < 0)
        {
            grabDistance = Constants.INTERACTION_DISTANCE_GRAB;
        }

        if (talkToDistance < 0)
        {
            talkToDistance = Constants.INTERACTION_DISTANCE_TALK_TO;
        }
    }

    public List<string> LookAtThis()
    {
        if (lookAt != null && lookAt.Count > 0)
        {
            return lookAt;
        }
        else
        {
            List<string> ans = new List<string>();
            ans.Add(Constants.DEFAULT_LOOK_AT_MESSAGE);
            return ans;
        }
    }

    public List<string> GrabThis()
    {
        if (grab != null && grab.Count > 0)
        {
            for (int i = holdsItems.Count; i > 0; i--)
            {
                print(holdsItems[i-1].itemName);
                Inventory.instance.inventory.Add(holdsItems[i-1]);
                holdsItems.RemoveAt(i-1);
            }
            Invoke("OnTakenAllItems", Constants.MESSAGE_BOARD_WAIT_TIME);
            return grab;
        }
        else
        {
            List<string> ans = new List<string>();
            ans.Add(Constants.DEFAULT_GRAB_MESSAGE);
            return ans;
        }
    }

    public DialougeTree TalkToThis()
    {
        if (talkTo != null && talkTo.dialougeContent.Count> 0)
        {
            return talkTo;
        }
        else
        {
            return null;
        }
    }

    public void OnTakenAllItems()
    {
        if (holdsItems.Count < 1 && shouldDisappearAfterItemsAreTaken)
        {
            Destroy(gameObject);
        }
    }

}
