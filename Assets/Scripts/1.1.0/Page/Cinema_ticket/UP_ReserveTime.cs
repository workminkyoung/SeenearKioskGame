using KioskApp.Cinema;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UP_ReserveTime : UP_PageBase
{
    [SerializeField]
    private RawImage _thumbnail;
    [SerializeField]
    private TextMeshProUGUI _title;
    private List<Button> _btns = new List<Button>();
    private List<TextMeshProUGUI> _btnTexts = new List<TextMeshProUGUI>();

    public override void Init()
    {
        _btns.AddRange(GetComponentsInChildren<Button>());
        base.Init();
        for (int i = 0; i < _btns.Count; i++)
        {
            _btnTexts.Add(_btns[i].GetComponentInChildren<TextMeshProUGUI>());
        }
    }

    protected override void BindDelegate()
    {
        base.BindDelegate();
        for (int i = 0; i < _btns.Count; i++)
        {
            int index = i;
            _btns[i].onClick.AddListener(() =>
            {
                Cinema_UserData.Instance._movie._time = _btnTexts[index].text;
            });
        }
    }

    protected override void OnPageEnable()
    {
        base.OnPageEnable();

        _thumbnail.texture = Cinema_UserData.Instance._movie._thumbnail;
        _title.text = Cinema_UserData.Instance._movie._name;
    }

    protected override void TutoBindDelegate()
    {
        base.TutoBindDelegate();

        _btns[1].onClick.AddListener(() =>
        {
            _navigation.ActivateNextButton(true);
        });
    }

    protected override void FreeBindDelegate() 
    {
        base.FreeBindDelegate();
        for (int i = 0; i < _btns.Count; i++)
        {
            _btns[i].onClick.AddListener(() =>
            {
                _navigation.ActivateNextButton(true);
            });
        }
    }

    protected override void RealBindDelegate()
    {
        base.RealBindDelegate();
        for (int i = 0; i < _btns.Count; i++)
        {
            _btns[i].onClick.AddListener(() =>
            {
                _navigation.ActivateNextButton(true);
            });
        }
    }

}
