using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UC_PopupPrint : MonoBehaviour
{
    Button _btnCancel;

    public void Init()
    {
        _btnCancel = GetComponentInChildren<Button>();
    }
}
