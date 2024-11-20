using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UP_Promise : UP_PageBase
{
    public Button _btn;

    protected override void BindDelegate()
    {
        base.BindDelegate();
        _btn.onClick.AddListener(() => NextPage());
    }
}
