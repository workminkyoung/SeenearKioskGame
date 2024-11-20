using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mcHomePageTuto : PageBase
{
    public string json = "McTutoDataHome";
    
    private McTutoList _tutoList;
    private SideBar _sideBar;
    private McTutoUI _tutoUI;
    private List<Button> _listBtn = new List<Button>();
    private Action Interaction;

    private int _curNarr = 0;
    
    public override void Setting()
    {
        base.Setting();
        _tutoList = DataLoader.LoadAndGetMcTutoData(json);
        _sideBar = GetComponentInChildren<SideBar>();
        
        _tutoUI = GetComponentInChildren<McTutoUI>();
        _tutoUI.Setting(NextNarration);
        
        _listBtn.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Button>(transform));
        _listBtn[(int)eBtn.Next].onClick.AddListener(() => NextPage());
        _listBtn[(int)eBtn.FakeSidebar].onClick.AddListener(() =>
        {
            _sideBar.Show();
            _tutoUI.SetLightState(false);
        });
        
        Interaction = Interaction_OnSidebar;
        for (int i = 0; i < _listBtn.Count; i++)
        {
            _listBtn[i].enabled = false;
        }
    }

    public override void Show(bool state)
    {
        base.Show(state);
        _sideBar.gameObject.SetActive(state);
    }

    void NextNarration()
    {
        if (_tutoList.items[_curNarr].directaction)
            Interaction();
        
        if (_tutoList.items[_curNarr].btnaction)
            _tutoUI.OnExplainBtn(Interaction);
        else
            _tutoUI.OnExplainBtn(null);
        
        _tutoUI.SetExplain(_tutoList.items[_curNarr]);
        _curNarr++;
    }

    void Interaction_OnSidebar()
    {
        _listBtn[(int)eBtn.FakeSidebar].enabled = true;
        _sideBar.OnComplete = () =>
        {
            NextNarration();
            _listBtn[(int)eBtn.FakeSidebar].enabled = false;
        };
        Interaction = Interaction_OffSidebar;
    }
    void Interaction_OffSidebar()
    {
        _sideBar.Hide();
        Interaction = Interaction_Order;
    }

    void Interaction_Order()
    {
        _tutoUI.SetBgState(false);
        _listBtn[(int)eBtn.Next].enabled = true;
    }

    enum eBtn
    {
        Next = 0,
        FakeSidebar
    }
}
