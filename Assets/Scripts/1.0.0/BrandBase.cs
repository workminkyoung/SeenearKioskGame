using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrandBase : MonoBehaviour
{
    private protected List<PageBase> listPage = new List<PageBase>();
    private protected int curPage = 0;
    private protected int prePage = 0;

    protected virtual void Awake()
    {
        listPage.AddRange(GetComponentsInChildren<PageBase>(transform));
        for (int i = 0; i < listPage.Count; i++)
        {
            listPage[i].Setting();
            listPage[i].NextPage = NextPage;
            listPage[i].PrePage = PrePage;
            listPage[i].SwitchPage = PageSwitch;
            listPage[i].gameObject.SetActive(false);
        }
        listPage[curPage].Show(true);
    }

    public virtual void PageSwitch<T>(T nextpage) where T : Enum
    {
        listPage[curPage].Show(false);
        prePage = curPage;
        curPage = Convert.ToInt32(nextpage);
        listPage[curPage].Show(true);

        if (Convert.ToInt32(nextpage) == 0)
        {
            curPage = 0;
            prePage = 0;
        }
    }

    public virtual void NextPage()
    {
    }

    public virtual void PrePage()
    {
        
    }
}
