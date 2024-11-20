using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

public class TextBox : MonoBehaviour
{
    private RectTransform _rect;
    private TextMeshProUGUI _text;
    private Button _nextBtn;
    private Action NextOption = null;
    private const float charSec = 0.1f;

    public void Setting(Action Next)
    {
        _rect = GetComponent<RectTransform>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
        _nextBtn = GetComponentInChildren<Button>();
        _nextBtn.onClick.AddListener(() =>
        {
            NextOption?.Invoke();
            Next();
        });
    }

    public void SetBtnAction(Action option)
    {
        NextOption = option;
    }

    public void SetBtnState(bool state)
    {
        _nextBtn.gameObject.SetActive(state);
        _nextBtn.enabled = state;
    }

    public void SetTextInfo(Vector2 size, Vector2 pos, string text)
    {
        if (text == "null")
        {
            SetState(false);
            return;
        }
        else if(!gameObject.activeSelf)
        {
            SetState(true);
        }
        _rect.anchoredPosition = pos;
        _rect.sizeDelta = size;
        _text.text = text;
        DoText(_text, text.Length * charSec);
    }
    
    public void DoText(TextMeshProUGUI text, float duration)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x, 0f, text.text.Length, duration).SetEase(Ease.Linear);
    }

    public void SetState(bool state)
    {
        gameObject.SetActive(state);
    }
}
