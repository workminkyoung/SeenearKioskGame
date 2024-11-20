using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class McItemPrefab : MonoBehaviour
{
    private Image _img;
    private Button _button;
    private List<TextMeshProUGUI> _listText = new List<TextMeshProUGUI>();
    private McMenuDetail _menu;

    public void Setting(McMenuDetail item)
    {
        _menu = item;
        _img = UtilityExtensions.GetComponentOnlyInChildren_NonRecursive<Image>(transform);
        _button = GetComponent<Button>();
        _listText.AddRange(GetComponentsInChildren<TextMeshProUGUI>());

        _listText[(int)eText.Name].text = _menu.name;
        _listText[(int)eText.Price].text = _menu.price.ToString();
        _img.sprite = item.image;
        _img.SetNativeSize();
    }

    public void SetAction(Action acton)
    {
        _button.onClick.AddListener(() => acton());
    }

    public void SetBtnState(bool state)
    {
        _button.enabled = state;
    }

    public McMenuDetail Menu
    {
        get { return _menu; }
    }

    enum eText
    {
        Name = 0,
        Price
    }
}
