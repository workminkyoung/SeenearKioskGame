using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UP_Start : UP_PageBase
{
    //2214
    public List<GameObject> _contents = new List<GameObject>();
    public List<Button> _btns = new List<Button>();

    public override void Init()
    {
        base.Init();
    }

    protected override void BindDelegate()
    {
        base.BindDelegate();

        _btns[(int)BUTTON.START].onClick.AddListener(() =>
        {
            LoadRule();
        });

        _btns[(int)BUTTON.START_GAME].onClick.AddListener(() =>
        {
            NextPage();
        });
    }

    protected override void OnPageEnable()
    {
        base.OnPageEnable();
        _contents[0].gameObject.SetActive(true);
        _contents[1].gameObject.SetActive(false);
    }

    private void LoadRule()
    {
        _contents[0].gameObject.SetActive(false);
        _contents[1].gameObject.SetActive(true);
    }


    enum BUTTON
    {
        START = 0,
        START_GAME
    }
}
