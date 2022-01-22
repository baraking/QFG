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
        if (talkTo != null && talkTo.dialougeContent.Count> 0)
        {
            return talkTo.dialougeContent;
        }
        else
        {
            List<string> ans = new List<string>();
            ans.Add(Constants.DEFAULT_TALK_TO_MESSAGE);
            return ans;
        }
    }

}
