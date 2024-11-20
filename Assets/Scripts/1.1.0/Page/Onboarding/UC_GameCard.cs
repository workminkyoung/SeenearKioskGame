using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UC_GameCard : MonoBehaviour
{
    public Action OnEndGame;
    public Image _image;
    public Button _btn;
    public Sprite _before, _after;

    public void Init()
    {
        _btn.onClick.AddListener(() =>
        {
            _image.sprite = _after;
            StartCoroutine(Next());
        });
    }

    public void Show(bool state)
    {
        if(state)
        {
            _image.sprite = _before;

        }
    }

    IEnumerator Next()
    {
        yield return new WaitForSecondsRealtime(1);
        OnEndGame?.Invoke();
    }
}
