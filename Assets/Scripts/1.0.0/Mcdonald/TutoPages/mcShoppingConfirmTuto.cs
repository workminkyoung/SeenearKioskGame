using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class mcShoppingConfirmTuto : PageBase
{
    private string json = "McTutoDataShopConfirm";
    private McTutoList _tutoList;
    private McTutoUI _tutoUI;
    private Action Interaction;
    private int _curNarr = 0;
    
    private List<Button> _listBtn = new List<Button>();
    private List<TextMeshProUGUI> _listText = new List<TextMeshProUGUI>();
    private RawImage _rawImage;
    
    public override void Setting()
    {
        base.Setting();
        _listBtn.AddRange(GetComponentsInChildren<Button>());
        _listText.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<TextMeshProUGUI>(transform));
        _rawImage = GetComponentInChildren<RawImage>();
        
        _listBtn[(int)eBtn.Cancle].onClick.AddListener(() =>
        {
            mcMenuSelectPageTuto.MenuConfirmed(false);
            SwitchPage(ePage.MenuSelect);
        });
        _listBtn[(int)eBtn.AddBag].onClick.AddListener(() =>
        {
            mcMenuSelectPageTuto.MenuConfirmed(true);
            SwitchPage(ePage.MenuSelect);
        });
        _listBtn[(int)eBtn.ChangeOption].onClick.AddListener(() => NextPage());

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
            if (mcMenuSelectPageTuto.IsSet)
            {
                _listText[(int)eText.Name].text = mcMenuSelectPageTuto._curSelectSetMenu.burger.name;
                int price = mcMenuSelectPageTuto._curSelectSetMenu.burger.price +
                            mcMenuSelectPageTuto._curSelectSetMenu.side.price +
                            mcMenuSelectPageTuto._curSelectSetMenu.drink.price;
                _listText[(int)eText.Price].text = price.ToString();
                _listBtn[(int)eBtn.ChangeOption].gameObject.SetActive(true);
                _rawImage.texture = DataManager.Instance.LoadItemTexture2D_spriteSheet(McdonaldProperties._McBrand,
                    mcMenuSelectPageTuto._curSelectSetMenu.burger.title, mcMenuSelectPageTuto._curSelectSetMenu.burger.index);
                _rawImage.SetNativeSize();
            }
            else
            {
                _listText[(int)eText.Name].text = mcMenuSelectPageTuto._curSelectMenu.name;
                _listText[(int)eText.Price].text = mcMenuSelectPageTuto._curSelectMenu.price.ToString();
                _listBtn[(int)eBtn.ChangeOption].gameObject.SetActive(false);

                _rawImage.texture = DataManager.Instance.LoadItemTexture2D_spriteSheet(McdonaldProperties._McBrand,
                    mcMenuSelectPageTuto._curSelectMenu.title, mcMenuSelectPageTuto._curSelectMenu.index);
                _rawImage.SetNativeSize();

            }
            
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
    }

    enum eBtn
    {
        Cancle = 0,
        AddBag,
        ChangeOption,
        Up,
        Down
    }

    enum eText
    {
        Count = 0,
        Name,
        Price,
        Image
    }
}
