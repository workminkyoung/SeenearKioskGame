using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mcMenuPushTuto : PageBase
{
    private string json = "McTutoDataPush";
    private McTutoList _tutoList;
    private McTutoUI _tutoUI;
    private Action Interaction;
    private int _curNarr = 0;
    
    private List<Button> _listBtn = new List<Button>();
    private McMenuDetail _itemCheese, _itemOreo;

    public override void Setting()
    {
        base.Setting();
        _tutoList = DataLoader.LoadAndGetMcTutoData(json);
        _listBtn.AddRange( UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Button>(transform));
        _listBtn[(int)eBtn.NotChoose].onClick.AddListener(() => NextPage());

        for (int i = 0; i < _listBtn.Count; i++)
        {
            _listBtn[i].enabled = false;
        }
        
        _tutoList = DataLoader.LoadAndGetMcTutoData(json);
        _tutoUI = GetComponentInChildren<McTutoUI>();
        _tutoUI.Setting(NextNarration);
        Interaction = Interaction_SelectBurger;
    }

    private void Start()
    {
        _itemCheese = McdonaldProperties.FindMenu(eMcTitleList.Side, "골든 모짜렐라 치즈스틱 4조각");
        _itemOreo = McdonaldProperties.FindMenu(eMcTitleList.Dessert, "오레오 맥플러리");
        
        _listBtn[(int)eBtn.itemCheese].onClick.AddListener(() =>
        {
            McdonaldManager.AddMenu(_itemCheese, true);
            NextPage();
        });
        _listBtn[(int)eBtn.itemOreo].onClick.AddListener(() =>
        {
            McdonaldManager.AddMenu(_itemOreo, true);
            NextPage();
        });
    }

    public override void Show(bool state)
    {
        base.Show(state);
        if(state)
            NextNarration();
    }

    void NextNarration()
    {
        _tutoUI.SetBgState(true);
        if (_tutoList.items[_curNarr].directaction)
            Interaction();
        
        if (_tutoList.items[_curNarr].btnaction)
            _tutoUI.OnExplainBtn(Interaction);
        else
            _tutoUI.OnExplainBtn(null);
        
        _tutoUI.SetExplain(_tutoList.items[_curNarr]);
        _curNarr++;
    }

    void Interaction_SelectBurger()
    {
        _tutoUI.SetBgState(false);
        _listBtn[(int)eBtn.NotChoose].enabled = true;
    }


    enum eBtn
    {
        itemCheese = 0,
        itemOreo,
        NotChoose
    }
}
