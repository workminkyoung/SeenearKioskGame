using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class McBrandBtn : BrandBtnBase
{
    List<Button> buttons = new List<Button>();
    Slider slider;

    public override void Setting()
    {
        base.Setting();
        buttons.AddRange(GetComponentsInChildren<Button>());
    }

    public override void Active(bool state)
    {
        base.Active(state);

    }

    public void SetSlider(int value)
    {

    }
}
