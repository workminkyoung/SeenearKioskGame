using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mcPayMobile : PageBase
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
        NextPage();
    }
}
