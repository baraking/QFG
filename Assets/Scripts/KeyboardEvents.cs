using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardEvents : MonoBehaviour
{
    private void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("i")))
        {
            UI_Manager.instance.ToggleInventory();
        }
    }
}
