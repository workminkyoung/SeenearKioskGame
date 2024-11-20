using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class mcComplete1Tuto : PageBase
{
    private string json = "McTutoDataComplete";
    private McTutoList _tutoList;
    private McTutoUI _tutoUI;
    private Action Interaction;
    private int _curNarr = 0;

    public override void Setting()
    {
        base.Setting();
        
        _tutoList = DataLoader.LoadAndGetMcTutoData(json);
        _tutoUI = GetComponentInChildren<McTutoUI>();
        _tutoUI.Setting(NextNarration);
        Interaction = Interaction_Next;
    }

    public override void Show(bool state)
    {
        base.Show(state);
        if (state)
        {
            //Invoke("Delay", 5f);
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

    void Interaction_Next()
    {
        _tutoUI.SetBgState(false);
        Invoke("Delay", 5f);
    }

    void Delay()
    {
        NextPage();
    }
}
