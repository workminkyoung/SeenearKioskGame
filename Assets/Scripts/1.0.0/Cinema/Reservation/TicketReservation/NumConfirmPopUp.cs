using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumConfirmPopUp : MonoBehaviour
{
    public TMP_InputField input;
    public TMP_Text txt;
    private void OnEnable()
    {
        txt.text = input.text;
    }

}
