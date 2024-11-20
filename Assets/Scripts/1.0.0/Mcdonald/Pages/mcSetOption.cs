using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mcSetOption : PageBase
{
    public GameObject itemPrefab;
    private List<OptionSideButton> _listOption = new List<OptionSideButton>();
    private List<McItemPrefab> _listMenuItem = new List<McItemPrefab>();
    private Transform _menuViwer;
    private TextMeshProUGUI _title;
    private eSide _curOption;

    private McSetMenuDetail _setMenu = new McSetMenuDetail();
    
    public override void Setting()
    {
        base.Setting();
        _menuViwer = GetComponentInChildren<ContentSizeFitter>().transform;
        _listOption.AddRange(GetComponentsInChildren<OptionSideButton>());
        _title = UtilityExtensions.GetComponentOnlyInChildren_NonRecursive<TextMeshProUGUI>(transform);
        for (int i = 0; i < _listOption.Count; i++)
        {
            _listOption[i].Setting();
        }
    }

    void Init()
    {
        ClearMenu();
        for (int i = 0; i < _listOption.Count; i++)
        {
            _listOption[i].BeforeSelected();
            _listOption[i].gameObject.SetActive(false);
        }
        _listOption[(int)eSide.Confirm].gameObject.SetActive(true);
        _curOption = eSide.Confirm;
        _setMenu = new McSetMenuDetail();

    }

    void ClearMenu()
    {
        for (int i = 0; i < _listMenuItem.Count; i++)
        {
            Destroy(_listMenuItem[i].gameObject);
        }
        _listMenuItem.Clear();
    }

    public override void Show(bool state)
    {
        base.Show(state);
        if (state)
        {
            Init();
            _title.text = mcMenuSelectPage._curSelectMenu.name;
            NextOption();
        }
    }

    void ShowOptionSet()
    {
        int index = mcMenuSelectPage._curSelectMenu.index;
        McMenuDetail set = FindMenu(index, eMcTitleList.Set);
        McMenuDetail largeSet = FindMenu(index, eMcTitleList.LargeSet, eMcTitleList.Set);
        
        GameObject cloneSet = Instantiate(itemPrefab, _menuViwer);
        McItemPrefab prefabSet = cloneSet.GetComponent<McItemPrefab>();
        prefabSet.Setting(set);
        prefabSet.SetAction(() =>
        {
            _setMenu.burger = prefabSet.Menu;
            NextOption();
        });
        
        GameObject cloneLarge = Instantiate(itemPrefab, _menuViwer);
        McItemPrefab prefabLarge = cloneLarge.GetComponent<McItemPrefab>();
        prefabLarge.Setting(largeSet);
        prefabLarge.SetAction(() =>
        {
            _setMenu.burger = prefabLarge.Menu;
            NextOption();
        });
        
        _listMenuItem.Add(prefabSet);
        _listMenuItem.Add(prefabLarge);
    }
    
    void ShowOption(eMcTitleList title)
    {
        ClearMenu();
        for (int i = 0; i < McdonaldProperties._McMenu.items[(int)title].name.Length; i++)
        {
            GameObject clone = Instantiate(itemPrefab, _menuViwer);
            McItemPrefab prefab = clone.GetComponent<McItemPrefab>();
            if (title == eMcTitleList.SetSide)
            {
                prefab.Setting(FindMenu(i, title));
                prefab.SetAction(() =>
                {
                    _setMenu.side = prefab.Menu;
                    NextOption();
                });
            }
            else if (title == eMcTitleList.SetDrink)
            {
                prefab.Setting(FindMenu(i, title, eMcTitleList.Drinks));
                prefab.SetAction(() =>
                {
                    _setMenu.drink = prefab.Menu;
                    NextOption();
                });
            }
            _listMenuItem.Add(prefab);
        }
        
    }

    McMenuDetail FindMenu(int index, eMcTitleList title)
    {
        McMenuDetail menu = new McMenuDetail();
        menu.title = McdonaldProperties._McMenu.items[(int)title].title;
        menu.name = McdonaldProperties._McMenu.items[(int)title].name[index];
        menu.price = McdonaldProperties._McMenu.items[(int)title].price[index];
        menu.index = index;
        menu.image = DataManager.Instance.LoadItemSprite_spriteSheet(
            McdonaldProperties._McBrand, menu.title, index);

        return menu;
    }

    McMenuDetail FindMenu(int index, eMcTitleList title, eMcTitleList imageTitle)
    {
        McMenuDetail menu = new McMenuDetail();
        menu.title = McdonaldProperties._McMenu.items[(int)title].title;
        menu.name = McdonaldProperties._McMenu.items[(int)title].name[index];
        menu.price = McdonaldProperties._McMenu.items[(int)title].price[index];
        menu.index = index;
        menu.image = DataManager.Instance.LoadItemSprite_spriteSheet(
            McdonaldProperties._McBrand, McdonaldProperties._McMenu.items[(int)imageTitle].title, index);

        return menu;
    }

    void NextOption()
    {
        switch (_curOption)
        {
            case eSide.Confirm:
                ShowOptionSet();
                _listOption[(int)eSide.Confirm].Selected();
                break;
            case eSide.Side:
                ShowOption(eMcTitleList.SetSide);
                for (int i = 0; i < _listOption.Count; i++)
                    _listOption[i].gameObject.SetActive(true);
                _listOption[(int)eSide.Confirm].AfterSelected(_setMenu.burger.name);
                _listOption[(int)eSide.Side].Selected();
                break;
            case eSide.Drink:
                ShowOption(eMcTitleList.SetDrink);
                _listOption[(int)eSide.Side].AfterSelected(_setMenu.side.name);
                _listOption[(int)eSide.Drink].Selected();
                break;
            case eSide.AllConfirm:
                mcMenuSelectPage.SetMenu = _setMenu;
                NextPage();
                return;
        }

        _curOption++;
    }

    enum eSide
    {
        Confirm = 0,
        Side,
        Drink,
        AllConfirm
    }
}
