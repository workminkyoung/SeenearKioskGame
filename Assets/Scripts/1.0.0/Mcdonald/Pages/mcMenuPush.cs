using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mcMenuPush : PageBase
{
    private List<Button> _listBtn = new List<Button>();
    private McMenuDetail _itemCheese, _itemOreo;

    public override void Setting()
    {
        base.Setting();
        _listBtn.AddRange(GetComponentsInChildren<Button>());
        _listBtn[(int)eBtn.NotChoose].onClick.AddListener(() => NextPage());
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


    enum eBtn
    {
        itemCheese = 0,
        itemOreo,
        NotChoose
    }
}
