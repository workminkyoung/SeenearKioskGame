using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class OptionSideButton : MonoBehaviour
{
    private List<Image> _listImg = new List<Image>();
    private TextMeshProUGUI _content;

    public Sprite greyImg;
    public Sprite whiteImg;
    public TMP_FontAsset activedFont;
    public TMP_FontAsset deactivedFont;

    private Color selectedColor, deselectedColor;
    private string _textInit;

    public void Setting()
    {
        selectedColor = ColorExtension.HexToColor("#000000");
        deselectedColor = ColorExtension.HexToColor("#8F8F8F");
        _listImg.AddRange(GetComponentsInChildren<Image>());
        _content = GetComponentInChildren<TextMeshProUGUI>();
        _textInit = _content.text;
    }

    public void BeforeSelected()
    {
        _listImg[(int)eImg.background].sprite = greyImg;
        _listImg[(int)eImg.bar].gameObject.SetActive(false);
        _content.font = deactivedFont;
        _content.color = deselectedColor;
        _content.text = _textInit;
    }

    public void Selected()
    {
        _listImg[(int)eImg.background].sprite = whiteImg;
        _listImg[(int)eImg.bar].gameObject.SetActive(true);
        _content.font = activedFont;
        _content.color = selectedColor;
    }

    public void AfterSelected(string name)
    {
        _listImg[(int)eImg.background].sprite = whiteImg;
        _listImg[(int)eImg.bar].gameObject.SetActive(false);
        _content.font = deactivedFont;
        _content.color = deselectedColor;
        _content.text = _textInit + "\n" + name;
    }

    enum eImg
    {
        background = 0,
        bar
    }
}
