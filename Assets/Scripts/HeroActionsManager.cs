using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroActionsManager : MonoBehaviour
{
    public static HeroActionsManager instance;

    public bool isCasting;

    private void Awake()
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
}
