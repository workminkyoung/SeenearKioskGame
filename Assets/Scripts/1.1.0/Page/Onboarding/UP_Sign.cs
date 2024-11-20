using FreeDraw;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UP_Sign : UP_PageBase
{
    public Button _btn, _btnReset;
    public GameObject _signBox, _signMask;
    public Drawable _drawable;

    public override void Init()
    {
        base.Init();
        _signBox.SetActive(false);
        _signMask.SetActive(false);
    }

    protected override void BindDelegate()
    {
        base.BindDelegate();
        _btn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
        _btnReset.onClick.AddListener(() =>
        {
            _drawable.ResetCanvas();
        });
    }

    protected override void OnPageEnable()
    {
        base.OnPageEnable();

        _signBox.SetActive(true);
        _signMask.SetActive(true);
    }
}
