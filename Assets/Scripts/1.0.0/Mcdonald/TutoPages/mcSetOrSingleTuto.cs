using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class mcSetOrSingleTuto : PageBase
{
    private string json = "McTutoDataSetOrSingle";
    private McTutoList _tutoList;
    private McTutoUI _tutoUI;
    private Action Interaction;
    private int _curNarr = 0;

    private List<Button> _listBtn = new List<Button>();
    
    public override void Setting()
    {
        base.Setting();
        _listBtn.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Button>(transform));
        _listBtn[(int)eBtn.Set].onClick.AddListener(() => NextPage());
        _listBtn[(int)eBtn.Single].onClick.AddListener(() => SwitchPage(ePage.ShoppingBasketConfirm));

        for (int i = 0; i < _listBtn.Count; i++)
        {
            _listBtn[i].enabled = false;
        }
        
        _tutoList = DataLoader.LoadAndGetMcTutoData(json);
        _tutoUI = GetComponentInChildren<McTutoUI>();
        _tutoUI.Setting(NextNarration);
        Interaction = Interaction_SelectSet;
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

    void Interaction_SelectSet()
    {
        _tutoUI.SetBgState(false);
        _listBtn[(int)eBtn.Set].enabled = true;
    }

    enum eBtn
    {
        Set = 0,
        Single
    }
}
