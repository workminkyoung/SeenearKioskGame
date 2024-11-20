using KioskApp.Tutorial;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnboardingManager : MonoBehaviour
{
    [SerializeField]
    private CONTENT_TYPE _contentType = CONTENT_TYPE.ONBOARDING;
    private List<UP_PageBase> _pages = new List<UP_PageBase>();
    [SerializeField]
    private int _curPage = 0;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _pages.AddRange(GetComponentsInChildren<UP_PageBase>());

        for (int i = 0; i < _pages.Count; i++)
        {
            _pages[i].SetContentType(_contentType);
            _pages[i].ChangePage = ChangePage;
            _pages[i].NextPage = NextPage;
            _pages[i].PrePage = PrePage;
            _pages[i].Init();
        }
        ChangePage(ONBOARDING.START);
    }

    public void ChangePage<T>(T tutoPage) where T : Enum
    {
        for (int i = 0; i < _pages.Count; i++)
        {
            if (Convert.ToInt32(tutoPage) == i)
            {
                _pages[i].EnablePage(true);
                Debug.Log(_pages[i].gameObject.name + "Enabled");
            }
            else
            {
                _pages[i].EnablePage(false);
                Debug.Log(_pages[i].gameObject.name + "Disabled");
            }
        }

        _curPage = Convert.ToInt32(tutoPage);
    }

    public void NextPage()
    {
        if (_curPage < _pages.Count - 1)
        {
            _curPage++;
            ChangePage((ONBOARDING)_curPage);
        }
    }

    public void PrePage()
    {
        if (_curPage > 0)
        {
            _curPage--;
            ChangePage((ONBOARDING)_curPage);
        }
    }
}
