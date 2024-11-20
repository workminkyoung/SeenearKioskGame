using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class mcHowToPayTuto : PageBase
{
    private List<Button> _listBtn = new List<Button>();
    
    private string json = "McTutoDataPay";
    private McTutoList _tutoList;
    private McTutoUI _tutoUI;
    private Action Interaction;
    private int _curNarr = 0;
    
    public override void Setting()
    {
        base.Setting();
        _listBtn.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Button>(transform));
        
        _listBtn[(int)eBtn.Card].onClick.AddListener(() => SwitchPage(ePage.PayCard));
        _listBtn[(int)eBtn.Mobile].onClick.AddListener(() => SwitchPage(ePage.PayMobileGift));
        _listBtn[(int)eBtn.Back].onClick.AddListener(() => PrePage());

        for (int i = 0; i < _listBtn.Count; i++)
        {
            _listBtn[i].enabled = false;
        }

        _listBtn[(int)eBtn.Card].enabled = true;
        
        _tutoList = DataLoader.LoadAndGetMcTutoData(json);
        _tutoUI = GetComponentInChildren<McTutoUI>();
        _tutoUI.Setting(NextNarration);
        Interaction = Interaction_Card;
    }

    public override void Show(bool state)
    {
        base.Show(state);
        if(state)
            NextNarration();
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

    void Interaction_Card()
    {
        _tutoUI.SetBgState(false);
    }

    enum eBtn
    {
        Card = 0,
        Mobile,
        Back
    }
}
