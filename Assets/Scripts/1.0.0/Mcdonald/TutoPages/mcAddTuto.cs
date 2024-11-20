using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class mcAddTuto : PageBase
{
    private List<UpDownOption> _listOption = new List<UpDownOption>();
    private List<Button> _listBtn = new List<Button>();

    public override void Setting()
    {
        base.Setting();
        _listOption.AddRange(GetComponentsInChildren<UpDownOption>());
        _listBtn.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Button>(transform));

        for (int i = 0; i < _listOption.Count; i++)
        {
            _listOption[i].Setting();
        }
        _listBtn[(int)eBtn.NoSave].onClick.AddListener(() => PrePage());
        _listBtn[(int)eBtn.Save].onClick.AddListener(() => PrePage());
    }

    enum eIngredient
    {
        Onion = 0,
        Cheese,
        Lettuce,
        Tomato,
        Egg
    }

    enum eBtn
    {
        NoSave = 0,
        Save
    }
}
