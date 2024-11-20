using KioskApp.Tutorial;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UC_TutoAlert : MonoBehaviour
{
    [SerializeField]
    private List<Image> _hands = new List<Image>();
    [SerializeField]
    private Image _alert;
    private TextMeshProUGUI _alertText;
    private CanvasGroup _canvasGroup;

    public void Init()
    {
        _alertText = GetComponentInChildren<TextMeshProUGUI>();
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    public void Show(bool state)
    {
        gameObject.SetActive(state);
    }

    public void SetAlertText(string text)
    {
        if(text.Length <= 0)
        {
            _alert.gameObject.SetActive(false);
        }
        else
        {
            _alert.gameObject.SetActive(true);
            _alertText.text = text.Replace("\\n", "\n");
        }
    }

    public void SetActivateAlert(bool state)
    {
        _alert.gameObject.SetActive(state);
    }

    public void SetActivateHand(POINT_SIZE size, RectTransform rect, int posType = 0)
    {
        for (int i = 0; i < _hands.Count; i++)
        {
            if(i == (int)size)
            {
                _hands[i].gameObject.SetActive(true);
                _hands[i].rectTransform.position = rect.position;
            }
            else
            {
                _hands[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetActivateHand(bool state)
    {
        for (int i = 0; i < _hands.Count; i++)
        {
            _hands[i].gameObject.SetActive(state);
        }
    }

    public void SetAlertBoxSize(POINT_SIZE size)
    {
        switch (size)
        {
            case POINT_SIZE.S:
                _alert.rectTransform.sizeDelta = new Vector2(960, 200);
                _alert.rectTransform.anchoredPosition = new Vector2(0, 159);
                break;
            case POINT_SIZE.M:
                _alert.rectTransform.sizeDelta = new Vector2(960, 271);
                _alert.rectTransform.anchoredPosition = new Vector2(0, 207);
                break;
            case POINT_SIZE.L:
                break;
            default:
                break;
        }
    }
}
