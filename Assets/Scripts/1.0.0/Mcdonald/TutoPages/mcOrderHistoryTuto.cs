using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class mcOrderHistoryTuto : PageBase
{
    public GameObject orderItem;
    List<TextMeshProUGUI> _listText = new List<TextMeshProUGUI>();
    List<Button> _listBtn = new List<Button>();
    List<McOrderPrefab> _listOrder = new List<McOrderPrefab>();
    Transform _menuViwer;
    
    private string json = "McTutoDataHistory";
    private McTutoList _tutoList;
    private McTutoUI _tutoUI;
    private Action Interaction;
    private int _curNarr = 0;

    public override void Setting()
    {
        base.Setting();
        _listText.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<TextMeshProUGUI>(transform));
        _listBtn.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Button>(transform));
        _menuViwer = GetComponentInChildren<ContentSizeFitter>().transform;

        _listBtn[(int)eBtn.Add].onClick.AddListener(() => SwitchPage(ePage.MenuSelect));
        _listBtn[(int)eBtn.Complete].onClick.AddListener(() => NextPage());
        _listBtn[(int)eBtn.Add].enabled = false;

        _tutoList = DataLoader.LoadAndGetMcTutoData(json);
        _tutoUI = GetComponentInChildren<McTutoUI>();
        _tutoUI.Setting(NextNarration);
        Interaction = Interaction_Complete;
    }

    void Init()
    {
        for (int i = 0; i < _listOrder.Count; i++)
        {
            Destroy(_listOrder[i].gameObject);
        }
        _listOrder.Clear();
    }

    public override void Show(bool state)
    {
        base.Show(state);
        if (state)
        {
            Init();

            for (int i = 0; i < McdonaldManager._selectMenu.listSetMenu.Count; i++)
            {
                CreateOrder(McdonaldManager._selectMenu.listSetMenu[i]);
            }
            for (int i = 0; i < McdonaldManager._selectMenu.listSingleMenu.Count; i++)
            {
                CreateOrder(McdonaldManager._selectMenu.listSingleMenu[i]);
            }

            _listText[(int)eText.Cost].text = McdonaldManager.Price.ToString();
            NextNarration();
        }
    }

    void CreateOrder(McMenuDetail menu)
    {
        GameObject clone = Instantiate(orderItem, _menuViwer);
        McOrderPrefab order = clone.GetComponent<McOrderPrefab>();

        order.Setting(menu, () =>
        {
            McdonaldManager.RemoveMenu(menu);
            _listOrder.Remove(order);
            Destroy(clone);
        });
    }

    void CreateOrder(McSetMenuDetail menu)
    {
        GameObject clone = Instantiate(orderItem, _menuViwer);
        McOrderPrefab order = clone.GetComponent<McOrderPrefab>();

        order.Setting(menu, () =>
        {
            McdonaldManager.RemoveMenu(menu);
            _listOrder.Remove(order);
            Destroy(clone);
        });
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

    void Interaction_Complete()
    {
        _tutoUI.SetBgState(false);
    }

    enum eText
    {
        Title = 0,
        Cost
    }

    enum eBtn
    {
        Add = 0,
        Complete
    }
}
