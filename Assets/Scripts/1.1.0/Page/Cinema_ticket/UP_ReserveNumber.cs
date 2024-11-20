using KioskApp.Cinema;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UP_ReserveNumber : UP_PageBase
{
    [SerializeField]
    private List<Toggle> _adultSelects = new List<Toggle>();
    [SerializeField]
    private List<Toggle> _teenSelects = new List<Toggle>();
    private int _adultNum = 0;
    private int _teenNum = 0;

    #region tuto
    private bool _selectAll = false;
    #endregion

    public override void Init()
    {
        base.Init();
    }

    public override void ResetPage()
    {
        base.ResetPage();
        _adultNum = 0;
        _teenNum = 0;
    }

    protected override void BindDelegate()
    {
        base.BindDelegate();
        for (int i = 0; i < _adultSelects.Count; i++)
        {
            int index = i;
            _adultSelects[i].onValueChanged.AddListener((state) =>
            {
                if(state)
                    _adultNum = index;
            });
            _teenSelects[i].onValueChanged.AddListener((state) =>
            {
                if(state)
                    _teenNum = index;
            });
        }
    }

    protected override void OnPageEnable()
    {
        base.OnPageEnable();
        _adultNum = 0;
        _teenNum = 0;
        Cinema_UserData.Instance._movie._adultNum = 0;
        Cinema_UserData.Instance._movie._teenNum = 0;

        Cinema_UserData.Instance._movie._selecSeats.Clear();
        Cinema_UserData.Instance._movie._selecSeats = new List<string>();

        for (int i = 0; i < _teenSelects.Count; i++)
        {
            _teenSelects[i].isOn = false;
            _adultSelects[i].isOn = false;
        }
    }

    protected override void OnPageDisable()
    {
        base.OnPageDisable();
        Cinema_UserData.Instance._movie._adultNum = _adultNum;
        Cinema_UserData.Instance._movie._teenNum = _teenNum;
    }

    protected override void TutoBindDelegate()
    {
        base.TutoBindDelegate();

        _adultSelects[1].onValueChanged.AddListener((state) =>
        {
            if (state)
            {
                if (_teenSelects[1].isOn)
                    _navigation.ActivateNextButton(true);
            }
            else
            {
                _navigation.ActivateNextButton(false);
            }
        });
        _teenSelects[1].onValueChanged.AddListener((state) =>
        {
            if (state)
            {
                if (_adultSelects[1].isOn)
                    _navigation.ActivateNextButton(true);
            }
            else
            {
                _navigation.ActivateNextButton(false);
            }
        });
    }

    protected override void FreeOnPageEnable()
    {
        base.FreeOnPageEnable();

        for (int i = 0; i < _adultSelects.Count; i++)
        {
            int index = i;
            _adultSelects[i].onValueChanged.AddListener((state) =>
            {
                _navigation.ActivateNextButton(CheckNum());
            });
            _teenSelects[i].onValueChanged.AddListener((state) =>
            {
                _navigation.ActivateNextButton(CheckNum());
            });
        }

    }

    protected override void RealBindDelegate()
    {
        base.RealBindDelegate();
        for (int i = 0; i < _adultSelects.Count; i++)
        {
            int index = i;
            _adultSelects[i].onValueChanged.AddListener((state) =>
            {
                _navigation.ActivateNextButton(CheckNum());
            });
            _teenSelects[i].onValueChanged.AddListener((state) =>
            {
                _navigation.ActivateNextButton(CheckNum());
            });
        }
    }

    private bool CheckNum()
    {
        if(_adultNum > 0 ||  _teenNum > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
