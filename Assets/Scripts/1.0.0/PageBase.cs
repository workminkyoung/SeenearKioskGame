using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class PageBase : MonoBehaviour
{
    public Action<Enum> SwitchPage;
    public Action NextPage, PrePage;

    public virtual void Setting()
    {
        
    }

    public virtual void Show(bool state)
    {
        gameObject.SetActive(state);
    }

    public virtual void Switch(int nextpage)
    {

    }

    public virtual void TutoSetting()
    {

    }

    public virtual void TutoNextPage()
    {

    }

    public virtual void TutoInteraction()
    {
        
    }

    public virtual void TutoAllowBtn()
    {
        
    }

}
