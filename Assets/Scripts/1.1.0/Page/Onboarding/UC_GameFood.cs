using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class UC_GameFood : MonoBehaviour
{
    public Action OnEndGame;
    public Image _imgPullDown, _imgPullUp;
    public GameObject _guide;
    public Button _btn;
    public GameObject _mark;

    private Scrollbar _scrollbar;


    public void Init()
    {
        _scrollbar = GetComponentInChildren<Scrollbar>();

        _btn.onClick.AddListener(() =>
        {
            _mark.SetActive(true);
            StartCoroutine(Next());
        });

        _imgPullDown.gameObject.SetActive(true);
        _imgPullUp.gameObject.SetActive(false);
        _guide.SetActive(true);
        _mark.SetActive(false);
    }

    public void Show(bool state)
    {
        if (state)
        {
            _scrollbar.onValueChanged.RemoveAllListeners();
            _scrollbar.onValueChanged.AddListener((value) =>
            {
                _imgPullDown.gameObject.SetActive(false);
                _imgPullUp.gameObject.SetActive(true);
                _guide.SetActive(false);
            });
            _imgPullDown.gameObject.SetActive(true);
            _imgPullUp.gameObject.SetActive(false);
            _guide.SetActive(true);
            _mark.SetActive(false);
        }
    }

    IEnumerator Next()
    {
        yield return new WaitForSeconds(0.5f);
        OnEndGame?.Invoke();
    }
}
