using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_Game : UP_PageBase
{
    public RectTransform _handler;

    private UC_GameGo _gameGo;
    private UC_GameCard _gameCard;
    private UC_GameFood _gameFood;

    private UC_Good _good;
    public Action _OnDownEnd;

    private const float _gap = 2214;

    public override void Init()
    {
        _gameGo = GetComponentInChildren<UC_GameGo>();
        _gameCard = GetComponentInChildren<UC_GameCard>();
        _gameFood = GetComponentInChildren<UC_GameFood>();
        _good = GetComponentInChildren<UC_Good>();
        base.Init();
        _gameGo.Init();
        _gameCard.Init();
        _gameFood.Init();

        _good.Init();
        _good.Show(false);
    }

    protected override void BindDelegate()
    {
        base.BindDelegate();
        _gameGo.OnEndGame = () =>
        {
            //show end page
            //set end return
            _good.Show(true);
            _good.SetEndAction(() =>
            {
                _good.Show(false);

                _OnDownEnd = null;
                _OnDownEnd = () =>
                {
                    _gameCard.Show(true);
                };
                StartGoDown();
            });
        };

        _gameCard.OnEndGame = () =>
        {
            _good.Show(true);
            _good.SetEndAction(() =>
            {
                _good.Show(false);

                _OnDownEnd = null;
                _OnDownEnd = () =>
                {
                    _gameFood.Show(true);
                };
                StartGoDown();
            });
        };

        _gameFood.OnEndGame = () =>
        {
            NextPage();
        };
    }

    protected override void OnPageEnable()
    {
        base.OnPageEnable();
        _good.Show(false);
        _handler.anchoredPosition = new Vector2(0, -1920);

        EnableGameGo();
    }

    public void StartGoDown()
    {
        StartCoroutine(GoDown());
    }

    IEnumerator GoDown()
    {
        float duration = 1.5f;
        float t = 0;

        float prePos = _handler.anchoredPosition.y;
        float targetPos = _handler.anchoredPosition.y + _gap;

        while(t < duration)
        {
            t += Time.deltaTime;
            float posY = UtilityExtensions.Remap(t, 0, duration, prePos, targetPos);
            _handler.anchoredPosition = new Vector2(_handler.anchoredPosition.x, posY);
            yield return null;
        }

        _handler.anchoredPosition = new Vector2(_handler.anchoredPosition.x, targetPos);
        _OnDownEnd?.Invoke();
    }

    void EnableGameGo()
    {
        _OnDownEnd = null;
        _OnDownEnd = () =>
        {
            _gameGo.Show(true);
        };
        StartGoDown();
    }
}
