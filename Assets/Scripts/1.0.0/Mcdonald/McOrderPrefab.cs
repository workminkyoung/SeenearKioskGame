using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class McOrderPrefab : MonoBehaviour
{
    Button _cancel;
    RawImage _image;
    List<TextMeshProUGUI> _listText = new List<TextMeshProUGUI>();
    UpDownOption _upDownOption;

    public void Setting(McSetMenuDetail menu, Action action)
    {
        _cancel = GetComponentInChildren<Button>();
        _image = GetComponentInChildren<RawImage>();
        _listText.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<TextMeshProUGUI>(transform));
        _upDownOption = GetComponentInChildren<UpDownOption>();

        _cancel.onClick.AddListener(() => action());
        _image.texture = DataManager.Instance.LoadItemTexture2D_spriteSheet(
            McdonaldProperties._McBrand, menu.burger.title, menu.burger.index);
        _listText[(int)eText.Name].text = menu.burger.name;
        _listText[(int)eText.Price].text = menu.burger.price.ToString();//이거 한번확인하기
    }
    public void Setting(McMenuDetail menu, Action action)
    {
        _cancel = GetComponentInChildren<Button>();
        _image = GetComponentInChildren<RawImage>();
        _listText.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<TextMeshProUGUI>(transform));
        _upDownOption = GetComponentInChildren<UpDownOption>();

        _cancel.onClick.AddListener(() => action());
        _image.texture = DataManager.Instance.LoadItemTexture2D_spriteSheet(
            McdonaldProperties._McBrand, menu.title, menu.index);
        _listText[(int)eText.Name].text = menu.name;
        _listText[(int)eText.Price].text = menu.price.ToString();
    }

    enum eText
    {
        Name = 0,
        Price
    }
}
