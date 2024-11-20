using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mcHomePage : PageBase
{
    private McTutoList _tuto;
    private List<Button> _listBtn = new List<Button>();
    
    public override void Setting()
    {
        base.Setting();
        _listBtn.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Button>(transform));
        _listBtn[(int)eBtn.Next].onClick.AddListener(() => NextPage());
    }

    public override void TutoSetting()
    {
        base.TutoSetting();
        for (int i = 0; i < _listBtn.Count; i++)
        {
            _listBtn[i].enabled = false;
        }
    }

    enum eBtn
    {
        Next = 0
    }
}
