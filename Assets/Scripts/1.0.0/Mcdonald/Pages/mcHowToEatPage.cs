using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mcHowToEatPage : PageBase
{
    private List<Button> _listBtn = new List<Button>();
    public override void Setting()
    {
        base.Setting();
        _listBtn.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Button>(transform));
        for (int i = 0; i < _listBtn.Count; i++)
        {
            _listBtn[i].onClick.AddListener((() => NextPage()));
        }
    }
}
