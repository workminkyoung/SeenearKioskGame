using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UC_FreeGuide : MonoBehaviour
{
    private Button _btnStart;

    public void Init()
    {
        _btnStart = GetComponentInChildren<Button>();
        _btnStart.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void Show(bool state)
    {
        gameObject.SetActive(state);
    }
}
