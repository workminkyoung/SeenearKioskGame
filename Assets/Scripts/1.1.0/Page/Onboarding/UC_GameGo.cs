using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UC_GameGo : MonoBehaviour
{
    public Action OnEndGame;
    private List<Button> _btns = new List<Button>();
    int _index = 0;
    const int _max = 3;

    public void Init()
    {
        _btns.AddRange(GetComponentsInChildren<Button>());

        foreach (Button btn in _btns)
        {
            btn.onClick.AddListener(() =>
            {
                _index++;
                btn.enabled = false;
                btn.image.color = Color.white;

                if(_index >= _max)
                {
                    StartCoroutine(Next());
                }
            });
        }

        foreach (Button btn in _btns)
        {
            btn.image.color = Color.clear;
        }
    }

    public void Show(bool state)
    {
        if (state)
        {
            foreach (Button btn in _btns)
            {
                btn.image.color = Color.clear;
            }
            _index = 0;
        }
    }

    IEnumerator Next()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        OnEndGame?.Invoke();
    }
}
