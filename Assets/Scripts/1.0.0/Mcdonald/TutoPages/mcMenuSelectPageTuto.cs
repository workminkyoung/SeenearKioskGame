using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mcMenuSelectPageTuto : PageBase
{
    private string json = "McTutoDataMenu";
    private McTutoList _tutoList;
    private McTutoUI _tutoUI;
    private Action Interaction;
    private int _curNarr = 0;
    public Camera cam;
    public GameObject lightPrefab;

    public GameObject itemPrefab;
    private List<Transform> _listArea = new List<Transform>();
    private List<Transform> _listMenuObj = new List<Transform>();
    private List<Button> _listBtn = new List<Button>();
    private List<McItemPrefab> _listMenuItem = new List<McItemPrefab>();
    private List<TextMeshProUGUI> _listText = new List<TextMeshProUGUI>();
    private GameObject _menuViewer;
    private mcLightBox _lightBox;
    
    private int _selectMenuCount = 0;
    private int _selectMenuPrice = 0;
    
    public static McMenuDetail _curSelectMenu;
    public static McSetMenuDetail _curSelectSetMenu;
    public static bool isSet = false;
    public static bool hasOrderChanged = false;

    [SerializeField]
    private RectTransform _itemRect;

    public static McMenuDetail Menu
    {
        get { return _curSelectMenu; }
        set { _curSelectMenu = value;
            _curSelectSetMenu = null;
            isSet = false;
        }
    }
    public static McSetMenuDetail SetMenu
    {
        get { return _curSelectSetMenu; }
        set
        {
            _curSelectMenu = null;
            _curSelectSetMenu = value;
            isSet = true;
        }
    }
    public static bool IsSet
    {
        get { return isSet; }
    }
    
    public override void Setting()
    {
        base.Setting();
        _listArea.AddRange(
            UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Transform>(transform));
        _listMenuObj.AddRange(
            UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Transform>(_listArea[(int)eArea.MiddleRange]));
        _listBtn.AddRange(GetComponentsInChildren<Button>());
        _menuViewer = GetComponentInChildren<ContentSizeFitter>().gameObject;
        _listText.AddRange(
            UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<TextMeshProUGUI>(_listArea[(int)eArea.BottomRange]));
        _lightBox = GetComponentInChildren<mcLightBox>();
        
        _lightBox.Setting();
        _listBtn[(int)eBtn.Home].onClick.AddListener(Init);
        _listBtn[(int)eBtn.BestMenu].onClick.AddListener(Init);
        for (int i = (int)eBtn.Burger; i < (int)eBtn.HappyMeal+1; i++)
        {
            int index = i - (int)eBtn.Burger;
            _listBtn[i].onClick.AddListener(() =>
            {
                ShowSelecMenu(index);
            });
        }
        
        _listBtn[(int)eBtn.MBestMenu].onClick.AddListener(Init);
        _listBtn[(int)eBtn.MBurger].onClick.AddListener(() => ShowSelecMenu((int)eMcTitleList.Burger));
        _listBtn[(int)eBtn.MHappySnack].onClick.AddListener(() => ShowSelecMenu((int)eMcTitleList.HappySnack));
        _listBtn[(int)eBtn.MCoffee].onClick.AddListener(() => ShowSelecMenu((int)eMcTitleList.Coffee));
        _listBtn[(int)eBtn.CheckOrder].onClick.AddListener(() => SwitchPage(ePage.ShoppingMenuPush));
        
        _curSelectMenu = null;
        _curSelectSetMenu = null;

        _tutoList = DataLoader.LoadAndGetMcTutoData(json);
        _tutoUI = GetComponentInChildren<McTutoUI>();
        _tutoUI.Setting(NextNarration);
        Interaction = Interaction_SelectBurger;
    }

    private void Start()
    {
        _listBtn[(int)eBtn.PBigmac].onClick.AddListener(() =>
        {
            Menu = McdonaldProperties.FindMenu(eMcTitleList.Burger, "빅맥");
            SwitchPage(ePage.SetOrSingle);
        });
        _listBtn[(int)eBtn.PCola].onClick.AddListener(() =>
        {
            Menu = McdonaldProperties.FindMenu(eMcTitleList.Drinks, "레몬 맥피즈 콜라 미디엄");
            SwitchPage(ePage.ShoppingBasketConfirm);
        });
        _listBtn[(int)eBtn.PSun].onClick.AddListener(() =>
        {
            Menu = McdonaldProperties.FindMenu(eMcTitleList.Dessert, "딸기 선데이 아이스크림");
            SwitchPage(ePage.ShoppingBasketConfirm);
        });
        _listBtn[(int)eBtn.PHappyBurger].onClick.AddListener(() =>
        {
            Menu = McdonaldProperties.FindMenu(eMcTitleList.HappyMeal, "해피밀 불고기버거");
            SwitchPage(ePage.ShoppingBasketConfirm);
        });
    }

    public override void Show(bool state)
    {
        base.Show(state);
        if (state)
        {
            for (int i = 0; i < _listBtn.Count; i++)
            {
                _listBtn[i].enabled = false;
            }
            Init();
            ShowSelectMenuPrice();
            NextNarration();
        }
    }

    void Init()
    {
        _listMenuObj[(int)eMenuObj.Main].gameObject.SetActive(true);
        _listMenuObj[(int)eMenuObj.Select].gameObject.SetActive(false);
        for (int i = 0; i < _listMenuItem.Count; i++)
        {
            Destroy(_listMenuItem[i].gameObject);
        }
        _listMenuItem.Clear();
    }

    public static void MenuConfirmed(bool isOrder)
    {
        if (isOrder)
        {
            if(_curSelectMenu != null)
                McdonaldManager.AddMenu(_curSelectMenu);
            if(_curSelectSetMenu != null)
                McdonaldManager.AddMenu(_curSelectSetMenu);

            hasOrderChanged = true;
            Debug.Log("add menu to manager");
        }
        _curSelectMenu = null;
        _curSelectSetMenu = null;
        isSet = false;
    }

    void ShowSelecMenu(int type)
    {
        Init();
        _listMenuObj[(int)eMenuObj.Main].gameObject.SetActive(false);
        _listMenuObj[(int)eMenuObj.Select].gameObject.SetActive(true);
        McItem item = McdonaldProperties._McMenu.items[type];
        for (int i = 0; i < item.name.Length; i++)
        {
            //here find burger and add light point as child,
            //and make it enabled
            //and add action that goes to next page
            
            McMenuDetail itemDetail = new McMenuDetail();
            itemDetail.title = item.title;
            itemDetail.name = item.name[i];
            itemDetail.price = item.price[i];
            itemDetail.index = i;
            itemDetail.image = DataManager.Instance.LoadItemSprite_spriteSheet(
                McdonaldProperties._McBrand, item.title, i);
            
            GameObject clone = Instantiate(itemPrefab, _menuViewer.transform);
            McItemPrefab menu = clone.GetComponent<McItemPrefab>();
            
            _listMenuItem.Add(menu);
            menu.Setting(itemDetail);
            menu.SetBtnState(false);

            if(itemDetail.name == "불고기 버거")
            {
                _itemRect = clone.GetComponent<RectTransform>();
                GameObject lightClone = Instantiate(lightPrefab, clone.transform);
                lightClone.SetActive(true);
                //LightSelf light = lightClone.GetComponent<LightSelf>();
                menu.SetBtnState(true);

                menu.SetAction(() =>
                {
                    Menu = menu.Menu;
                    Destroy(lightClone);
                    //maybe button disable could be needed 
                    SwitchPage(ePage.SetOrSingle);
                });

                continue;
            }else if (itemDetail.name == "맥너겟 4조각")
            {
                _itemRect = clone.GetComponent<RectTransform>();
                GameObject lightClone = Instantiate(lightPrefab, clone.transform);
                lightClone.SetActive(true);
                //LightSelf light = lightClone.GetComponent<LightSelf>();
                menu.SetBtnState(true);

                menu.SetAction(() =>
                {
                    Menu = menu.Menu;
                    Destroy(lightClone);
                    NextNarration();
                });
                continue;
            }


            if (type == (int)eMcTitleList.Burger)
            {
                menu.SetAction(() =>
                {
                    Menu = menu.Menu;
                    SwitchPage(ePage.SetOrSingle);
                });
            }
            else
            {
                menu.SetAction(() =>
                {
                    Menu = menu.Menu;
                    SwitchPage(ePage.ShoppingBasketConfirm);
                });
            }
        }
    }

    //manager데이터와 비교 show할때
    void ShowSelectMenuPrice()
    {
        _selectMenuCount = 0;
        _selectMenuPrice = 0;
        for (int i = 0; i < McdonaldManager._selectMenu.listSetMenu.Count; i++)
        {
            _selectMenuCount++;
            _selectMenuPrice += McdonaldManager._selectMenu.listSetMenu[i].burger.price;
            _selectMenuPrice += McdonaldManager._selectMenu.listSetMenu[i].side.price;
            _selectMenuPrice += McdonaldManager._selectMenu.listSetMenu[i].drink.price;
            //세트용 메뉴리스트랑 가격 따로 빼놓기..
        }
        for (int i = 0; i < McdonaldManager._selectMenu.listSingleMenu.Count; i++)
        {
            _selectMenuCount++;
            _selectMenuPrice += McdonaldManager._selectMenu.listSingleMenu[i].price;
        }
        _listText[(int)eText.Count].text = _selectMenuCount.ToString();
        _listText[(int)eText.Cost].text = _selectMenuPrice.ToString();

        if (hasOrderChanged)
        {
            McdonaldManager.Price = _selectMenuPrice;
            _lightBox.ShowPrice(_selectMenuPrice);
            _lightBox.Show();
            hasOrderChanged = false;
        }
    }

    /// <summary>
    /// tutorial
    /// </summary>
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
        _listBtn[(int)eBtn.Burger].enabled = true;
        _listBtn[(int)eBtn.Burger].onClick.AddListener(SelectedBurger);
    }

    void SelectedBurger()
    {
        Debug.Log("select burger category");
        _tutoUI.SetBgState(true);
        NextNarration();
        _listBtn[(int)eBtn.Burger].onClick.RemoveListener(SelectedBurger);
        Interaction = Interaction_Drag;
    }

    void Interaction_Drag()
    {
        Debug.Log("check burger");
        _tutoUI.SetBgState(false);
        StartCoroutine(FindBurgerVisible());
    }

    IEnumerator FindBurgerVisible()
    {
        while (!_itemRect.IsFullyVisibleFrom(cam))
        {
            yield return null;
        }
        
        Interaction = Interaction_FinalSelect;
        Debug.Log("find burger ui");
        _tutoUI.SetBgState(true);
        NextNarration();
    }

    void Interaction_FinalSelect()
    {
        _tutoUI.SetBgState(false);
        Interaction = Interaction_SelectSide;
    }

    void Interaction_SelectSide()
    {
        _tutoUI.SetBgState(false);
        _listBtn[(int)eBtn.Side].enabled = true;
        Interaction = Interaction_Buy;
    }

    void Interaction_Buy()
    {
        _tutoUI.SetBgState(false);
        _listBtn[(int)eBtn.CheckOrder].enabled = true;
    }

    
    enum eArea
    {
        Logo = 0,
        SideBar,
        MiddleRange,
        BottomRange
    }

    enum eMenuObj
    {
        Main = 0,
        Select
    }
    
    enum eBtn
    {
        Home = 0,
        BestMenu,
        Burger,
        HappySnack,
        Side,
        Coffee,
        Dessert,
        Drinks,
        HappyMeal,
        
        //middle btns
        MBestMenu,
        MBurger,
        MHappySnack,
        MCoffee,
        
        PBigmac,
        PCola,
        PSun,
        PHappyBurger,
        
        up,
        down,
        
        CheckOrder
    }

    enum eText
    {
        Count = 0,
        Cost
    }
}
