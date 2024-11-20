using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KioskApp.Cinema;
using UnityEditor;

public class UP_ReserveMovie : UP_PageBase
{
    private List<Button> _btns = new List<Button>();
    private List<UC_MovieSelectable> _movieSelectables = new List<UC_MovieSelectable>();

    public override void Init()
    {
        _btns.AddRange(GetComponentsInChildren<Button>());
        _movieSelectables.AddRange(GetComponentsInChildren<UC_MovieSelectable>());
        base.Init();

        for (int i = 0; i < _movieSelectables.Count; i++)
        {
            _movieSelectables[i].Init();
        }
    }

    protected override void BindDelegate()
    {
        for (int i = 0; i < _btns.Count; i++)
        {
            int index = i;
            _btns[i].onClick.AddListener(() =>
            {
                Cinema_UserData.Instance._movie = _movieSelectables[index].Movie;
            });
        }
        base.BindDelegate();
    }

    protected override void OnPageEnable()
    {
        base.OnPageEnable();
        _navigation.ActivateNavigation(true);
    }

    #region Tuto
    protected override void TutoBindDelegate()
    {
        base.TutoBindDelegate();
        _btns[0].onClick.AddListener(() =>
        {
            _navigation.ActivateNextButton(true);
        });
    }

    protected override void TutoOnPageEnable()
    {
        base.TutoOnPageEnable();
    }
    #endregion

    protected override void FreeBindDelegate()
    {
        base.FreeBindDelegate();

        for (int i = 0; i < _btns.Count; i++)
        {
            int index = i;
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
            int index = i;
            _btns[i].onClick.AddListener(() =>
            {
                _navigation.ActivateNextButton(true);
            });
        }
    }
}
