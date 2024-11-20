using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class McdonalTutodManager : BrandBase
{
    //public static McMenuDetail _curSelectMenu;
    public static McSelectMenu _selectMenu;
    public static int _menuPrice;
    public bool _isTuto = false;
    public McTutoManager _tutoManager;
    public List<Canvas> _listCanvas;
    private SideBar _sideBar;

    public static int Price
    {
        get { return _menuPrice; }
        set { _menuPrice = value; }
    }
    
    public bool SetIsTuto
    {
        set { _isTuto = value; }
    }
    
    protected override void Awake()
    {
        base.Awake();
        _sideBar = UtilityExtensions.GetComponentOnlyInChildren_NonRecursive<SideBar>(transform);
        _sideBar.Setting();
        InitMenu();
    }

    public override void PageSwitch<ePage>(ePage nextpage)
    {
        base.PageSwitch(nextpage);
    }

    public override void NextPage()
    {
        base.NextPage();
        if(curPage < listPage.Count-1)
            PageSwitch((ePage)curPage+1);
    }

    public override void PrePage()
    {
        base.PrePage();
        PageSwitch((ePage)curPage-1);
    }

    public void Tuto_curPageInteraction()
    {
        listPage[curPage].TutoInteraction();
    }

    public void Tuto_curPageNext()
    {
        
    }

    public static void AddMenu(McMenuDetail menu, bool isAddPrice = false)
    {
        _selectMenu.listSingleMenu.Add(menu);

        if (isAddPrice)
        {
            _menuPrice += menu.price;
        }
    }

    public static void AddMenu(McSetMenuDetail menu, bool isAddPrice = false)
    {
        _selectMenu.listSetMenu.Add(menu);
        
        if (isAddPrice)
        {
            _menuPrice += menu.burger.price;
            _menuPrice += menu.side.price;
            _menuPrice += menu.drink.price;
        }
    }

    public static void RemoveMenu(McMenuDetail menu)
    {
        _selectMenu.listSingleMenu.Remove(menu);
    }
    public static void RemoveMenu(McSetMenuDetail menu)
    {
        _selectMenu.listSetMenu.Remove(menu);
    }

    public static void InitMenu()
    {
        _selectMenu = new McSelectMenu();
        _selectMenu.listSingleMenu = new List<McMenuDetail>();
        _selectMenu.listSetMenu = new List<McSetMenuDetail>();
    }
    
}
