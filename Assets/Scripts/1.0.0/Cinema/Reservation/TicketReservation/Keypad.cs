using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Keypad : MonoBehaviour
{
    public TMP_InputField input;
    private void OnEnable()
    {
        input.text = null;
    }

    public void KeyInput()
    {
        if(EventSystem.current.currentSelectedGameObject.name == "ReInput")
        {
            input.text = null;
        } 
        else if(EventSystem.current.currentSelectedGameObject.name == "Delete")
        {
            if (input.text.Length > 0)
            {
                input.text = input.text.Substring(0, input.text.Length - 1);
            }
        }
        else
        {
            if(input.text.Length > 7)
            {
                return;
            }

            input.text += EventSystem.current.currentSelectedGameObject.name;
        }
    }
}
