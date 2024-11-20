using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UP_SelectCheck : UP_PageBase
{
    private List<Button> _btns = new List<Button>();

    public override void Init()
    {
        _btns.AddRange(GetComponentsInChildren<Button>());
        base.Init();
    }

    protected override void BindDelegate()
    {
        base.BindDelegate();
        
    }

    enum BUTTON
    {
        CHECK = 0,
        PHONE
    }
}
