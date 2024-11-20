using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SideBar : MonoBehaviour
{
    public float showPos, hidePos;
    public Action OnComplete = null;
    private List<Button> _listBtn = new List<Button>();
    private RectTransform _rect;
    private bool _isMoving = false;
    private bool _isShow = false;

    public void Setting()
    {
        _rect = GetComponent<RectTransform>();
        _listBtn.AddRange(GetComponentsInChildren<Button>());
        _listBtn[(int)eBtn.Active].onClick.AddListener(() =>
        {
            if(_isShow) Hide();
            else Show();
        });
        _listBtn[(int)eBtn.Quiz].gameObject.SetActive(false);
    }

    /// <summary>
    /// tutorial functions
    /// </summary>
    public void SetEnableState(bool state)
    {
        _listBtn[(int)eBtn.Active].enabled = state;
    }

    public void Show()
    {
        if(_isMoving)
            return;
        
        _isShow = true;
        _isMoving = true;
        _rect.DOAnchorPosX(showPos, 1).OnComplete(() =>
        {
            _isMoving = false;
            OnComplete?.Invoke();
            OnComplete = null;
        });
    }

    public void Hide()
    {
        if(_isMoving)
            return;
        
        _isShow = false;
        _isMoving = true;
        _rect.DOAnchorPosX(hidePos, 1).OnComplete(() =>
        {
            _isMoving = false;
            OnComplete?.Invoke();
            OnComplete = null;
        });
    }

    enum eBtn
    {
        Active = 0,
        Quiz,
        Play,
        Replay,
        Exit
    }
}

