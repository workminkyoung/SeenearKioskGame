using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mcPayCardTuto : PageBase
{
    public override void Show(bool state)
    {
        base.Show(state);
        if (state)
        {
            Invoke("Delay", 5f);
        }
    }

    void Delay()
    {
        SwitchPage(ePage.OrderComplete);
    } 
}
