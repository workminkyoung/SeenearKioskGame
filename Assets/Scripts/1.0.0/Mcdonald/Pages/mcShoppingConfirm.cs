using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mcShoppingConfirm : PageBase
{
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
            mcMenuSelectPage.MenuConfirmed(false);
            SwitchPage(ePage.MenuSelect);
        });
        _listBtn[(int)eBtn.AddBag].onClick.AddListener(() =>
        {
            mcMenuSelectPage.MenuConfirmed(true);
            SwitchPage(ePage.MenuSelect);
        });
        _listBtn[(int)eBtn.ChangeOption].onClick.AddListener(() => NextPage());
    }

    public override void Show(bool state)
    {
        base.Show(state);
        if (state)
        {
            if (mcMenuSelectPage.IsSet)
            {
                _listText[(int)eText.Name].text = mcMenuSelectPage._curSelectSetMenu.burger.name;
                int price = mcMenuSelectPage._curSelectSetMenu.burger.price +
                            mcMenuSelectPage._curSelectSetMenu.side.price +
                            mcMenuSelectPage._curSelectSetMenu.drink.price;
                _listText[(int)eText.Price].text = price.ToString();
                _listBtn[(int)eBtn.ChangeOption].gameObject.SetActive(true);
                _rawImage.texture = DataManager.Instance.LoadItemTexture2D_spriteSheet(McdonaldProperties._McBrand,
                    mcMenuSelectPage._curSelectSetMenu.burger.title, mcMenuSelectPage._curSelectSetMenu.burger.index);
                _rawImage.SetNativeSize();
            }
            else
            {
                _listText[(int)eText.Name].text = mcMenuSelectPage._curSelectMenu.name;
                _listText[(int)eText.Price].text = mcMenuSelectPage._curSelectMenu.price.ToString();
                _listBtn[(int)eBtn.ChangeOption].gameObject.SetActive(false);

                _rawImage.texture = DataManager.Instance.LoadItemTexture2D_spriteSheet(McdonaldProperties._McBrand,
                    mcMenuSelectPage._curSelectMenu.title, mcMenuSelectPage._curSelectMenu.index);
                _rawImage.SetNativeSize();

            }
        }
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
