using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UC_PopupEnd : MonoBehaviour
{
    public Action OnPopupEnd;
    public Action OnPopupCancel;

    Button _btnCancel;
    private Coroutine _returnRoutine;

    public void Init()
    {
        _btnCancel = GetComponentInChildren<Button>();
        _btnCancel.onClick.AddListener(() =>
        {
            OnPopupCancel?.Invoke();
            Show(false);
        });
    }

    public void Show(bool state)
    {
        gameObject.SetActive(state);
    }

}
