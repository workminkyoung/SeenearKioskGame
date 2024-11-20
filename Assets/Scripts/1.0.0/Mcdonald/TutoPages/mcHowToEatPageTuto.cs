using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class mcHowToEatPageTuto : PageBase
{
    private string json = "McTutoDataHow";
    private McTutoList _tutoList;
    private McTutoUI _tutoUI;
    private Action Interaction;
    private int _curNarr = 0;

    private List<Button> _listBtn = new List<Button>();

    public override void Setting()
    {
        base.Setting();
        _tutoList = DataLoader.LoadAndGetMcTutoData(json);
        
        _tutoUI = GetComponentInChildren<McTutoUI>();
        _tutoUI.Setting(NextNarration);
        
        _listBtn.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Button>(transform));
        for (int i = 0; i < _listBtn.Count; i++)
        {
            _listBtn[i].onClick.AddListener((() => NextPage()));
            _listBtn[i].enabled = false;
        }
        
        Interaction = Interaction_Select;
    }

    public override void Show(bool state)
    {
        base.Show(state);
        if (state)
        {
            NextNarration();
        }
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

    void Interaction_Select()
    {
        _tutoUI.SetBgState(false);
        _listBtn[1].enabled = true;
    }
}
