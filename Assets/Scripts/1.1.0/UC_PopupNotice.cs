using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UC_PopupNotice : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;

    public void Init()
    {
        
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void Show(bool state)
    {
        gameObject.SetActive(state);
    }
}
