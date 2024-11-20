using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UC_Good : MonoBehaviour
{
    public Action OnTurnOff;
    public List<TextMeshProUGUI> _texts = new List<TextMeshProUGUI>();
    public Button _btn;

    public void Init()
    {
        _btn.onClick.AddListener(() =>
        {
            OnTurnOff?.Invoke();
        });
    }

    public void Show(bool state)
    {
        gameObject.SetActive(state);
        if (state)
        {
            _btn.gameObject.SetActive(false);
            for (int i = 0; i < _texts.Count; i++)
            {
                _texts[i].gameObject.SetActive(false);
            }

            StartCoroutine(ShowRoutine());
        }
    }

    IEnumerator ShowRoutine()
    {
        // show routine 
        yield return new WaitForSecondsRealtime(1f);
        _texts[0].gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        _texts[1].gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        _btn.gameObject.SetActive(true); 
    }

    public void SetEndAction(Action action)
    {
        OnTurnOff = null;
        OnTurnOff = action;

    }
}
