using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialougeOptions : MonoBehaviour
{
    public GameObject dialougeOptionsHolder;

    public void ResetOptions()
    {
        foreach (Transform child in dialougeOptionsHolder.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
