using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using KioskApp.Tutorial;

[RequireComponent(typeof(CanvasGroup))]
public class UC_Navigation : MonoBehaviour
{
    public Action NextPage, PrePage;
    public Action<Enum> ChangePage;
    private List<Button> _btns = new List<Button>();
    private CanvasGroup _canvasGroup;
    private RectTransform _container;
    
    //캔버스 그룹 연결해서 알파껏켯 만들고 델리게이트 연결하기
    public void Init()
    {
        _btns.AddRange(GetComponentsInChildren<Button>());
        _canvasGroup = GetComponent<CanvasGroup>();
        _container = _btns[0].transform.parent.GetComponent<RectTransform>();

        BindDelegate();
    }

    public void SetPosition(float x, float y)
    {
        _container.anchoredPosition = new Vector2(x, y);
    }

    private void BindDelegate()
    {
        _btns[(int)BUTTON.HOME].onClick.AddListener(() => ChangePage(CINEMA_TICKET.SELECT_BOOK));
        _btns[(int)BUTTON.PRE].onClick.AddListener(() => PrePage());
        _btns[(int)BUTTON.NEXT].onClick.AddListener(() => NextPage());
    }

    public void SetNextAction(Action action)
    {
        _btns[(int)BUTTON.NEXT].onClick.RemoveAllListeners();
        _btns[(int)BUTTON.NEXT].onClick.AddListener(() =>
        {
            action();
        });
    }

    public void ResetNextAction()
    {
        _btns[(int)BUTTON.NEXT].onClick.RemoveAllListeners();
        _btns[(int)BUTTON.NEXT].onClick.AddListener(() => NextPage());
    }

    public void ActivateHomeButton(bool state)
    {
        _btns[(int)BUTTON.HOME].enabled = state;
    }

    public void ActivatePreButton(bool state)
    {
        _btns[(int)BUTTON.PRE].enabled = state;
    }

    public void ActivateNextButton(bool state)
    {
        _btns[(int)BUTTON.NEXT].enabled = state;
    }

    public void ActivateNavigation(bool state)
    {
        _canvasGroup.alpha = state ? 1 : 0;
        _canvasGroup.interactable = state;
    }
    
    private enum BUTTON
    {
        HOME = 0,
        PRE,
        NEXT
    }
}
