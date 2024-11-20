using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mcHowToPay : PageBase
{
    private List<Button> _listBtn = new List<Button>();
    
    public override void Setting()
    {
        base.Setting();
        _listBtn.AddRange(GetComponentsInChildren<Button>());
        
        _listBtn[(int)eBtn.Card].onClick.AddListener(() => SwitchPage(ePage.PayCard));
        _listBtn[(int)eBtn.Mobile].onClick.AddListener(() => SwitchPage(ePage.PayMobileGift));
        _listBtn[(int)eBtn.Back].onClick.AddListener(() => PrePage());
    }

    enum eBtn
    {
        Card = 0,
        Mobile,
        Back
    }
}
