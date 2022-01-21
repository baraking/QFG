using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Queue<string>))]
public class Interactable : MonoBehaviour
{
    [SerializeField] public List<string> lookAt;
    [SerializeField] public List<string> grab;
    [SerializeField] public List<string> talkTo;

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
            return grab;
        }
        else
        {
            List<string> ans = new List<string>();
            ans.Add(Constants.DEFAULT_GRAB_MESSAGE);
            return ans;
        }
    }

    public List<string> TalkToThis()
    {
        if (talkTo != null && talkTo.Count>0)
        {
            return talkTo;
        }
        else
        {
            List<string> ans = new List<string>();
            ans.Add(Constants.DEFAULT_TALK_TO_MESSAGE);
            return ans;
        }
    }

}
