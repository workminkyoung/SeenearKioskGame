using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mcSetOrSingle : PageBase
{
    private List<Button> _listBtn = new List<Button>();
    
    public override void Setting()
    {
        base.Setting();
        _listBtn.AddRange(GetComponentsInChildren<Button>());
        _listBtn[(int)eBtn.Set].onClick.AddListener(() => NextPage());
        _listBtn[(int)eBtn.Single].onClick.AddListener(() => SwitchPage(ePage.ShoppingBasketConfirm));
    }

    enum eBtn
    {
        Set = 0,
        Single
    }
}
