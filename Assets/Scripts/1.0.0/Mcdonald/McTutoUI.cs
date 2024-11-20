using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class McTutoUI : MonoBehaviour
{
    [SerializeField]
    private Image _background;
    private SideBar _sideBar;
    private TextBox _textBox;
    private LightPoint _lightPoint;
    private List<TutoMask> _listMask = new List<TutoMask>();
    private Queue<int> _maskQue = new Queue<int>();

    public void Setting(Action Next)
    {
        _background = UtilityExtensions.GetComponentOnlyInChildrenByTag_NonRecursive<Image>(transform, "bg");
        _textBox = GetComponentInChildren<TextBox>();
        _textBox.Setting(Next);

        _lightPoint = GetComponentInChildren<LightPoint>();
        _lightPoint.Setting();

        _sideBar = GetComponentInChildren<SideBar>();
        if (_sideBar != null)
        {
            _sideBar.Setting();
            _sideBar.SetEnableState(false);
            _sideBar.gameObject.SetActive(false);
        }
        
        _listMask.AddRange(GetComponentsInChildren<TutoMask>());
        for (int i = 0; i < _listMask.Count; i++)
        {
            _listMask[i].Activate(false);
        }
    }
    
    public void SetExplain(McTuto tutoData)
    {
        if (tutoData.directaction)
            _textBox.SetBtnState(false);
        else
            _textBox.SetBtnState(true);

        if (tutoData.mask != 999)
        {
            if(_maskQue.Count > 0)
                _listMask[_maskQue.Dequeue()].Activate(false);
            _listMask[tutoData.mask].Activate(true);
            _maskQue.Enqueue(tutoData.mask);
        }
        else
        {
            for (int i = 0; i < _listMask.Count; i++)
            {
                _listMask[i].Activate(false);
            }
        }
        
        _textBox.SetTextInfo(tutoData.sizeVec, tutoData.posVec, tutoData.text);
        _lightPoint.SetState(tutoData.light, tutoData.lposVec, tutoData.ltype);
    }

    public void OnExplainBtn(Action action)
    {
        _textBox.SetBtnAction(action);
    }

    public void SetLightState(bool state)
    {
        _lightPoint.SetState(state);
    }

    public void SetBgState(bool state)
    {
        _background.gameObject.SetActive(state);
    }
}
