using System;
using System.Collections;
using System.Collections.Generic;
using KioskApp.Tutorial;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UP_PageBase : MonoBehaviour
{
    public Action NextPage, PrePage;
    public Action<Enum> ChangePage;

    [SerializeField] protected GameObject _highlight;
    [SerializeField] protected TutorialDataTable _tutoData;
    [SerializeField] protected List<RectTransform> _highlightObjects;
    protected CanvasGroup _canvasGroup = null;
    protected UC_TopBar _topBar;
    protected UC_Navigation _navigation;
    protected UC_Global _global;
    protected CONTENT_TYPE _contentType;
    protected bool _tutoInteractionChecked = false;
    protected Coroutine _tutoInteractionCoroutine;
    protected int _highlightIndex = 0;

    public CanvasGroup canvasGroup => _canvasGroup;

    public void SetTopBar(UC_TopBar topBar)
    {
        _topBar = topBar;
    }

    public void SetNavigation(UC_Navigation navigation)
    {
        _navigation = navigation;
    }

    public void SetGlobal(UC_Global global)
    {
        _global = global;
    }

    public void SetContentType(CONTENT_TYPE type)
    {
        _contentType = type;
    }
    
    public virtual void Init()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        BindDelegate();
    }

    public virtual void ResetPage()
    {
    }

    protected virtual void BindDelegate()
    {
        switch (_contentType)
        {
            case CONTENT_TYPE.TUTO:
                TutoBindDelegate();
                break;
            case CONTENT_TYPE.FREE:
                FreeBindDelegate();
                break;
            case CONTENT_TYPE.REAL:
                RealBindDelegate();
                break;
            default:
                break;
        }
    }

    public virtual void EnablePage(bool isEnable)
    {
        if (!isEnable && this.gameObject.activeInHierarchy)
        {
            OnPageDisable();
        }

        gameObject.SetActive(isEnable);

        if (isEnable)
        {
            OnPageEnable();
        }
    }

    protected virtual void OnPageEnable()
    {
        switch (_contentType)
        {
            case CONTENT_TYPE.TUTO:
                TutoOnPageEnable();
                break;
            case CONTENT_TYPE.FREE:
                FreeOnPageEnable();
                break;
            case CONTENT_TYPE.REAL:
                RealOnPageEnable();
                break;
            default:
                break;
        }
    }

    protected virtual void OnPageDisable()
    {
        switch (_contentType)
        {
            case CONTENT_TYPE.TUTO:
                TutoOnPageDisable();
                break;
            case CONTENT_TYPE.FREE:
                FreeOnPageDisable();
                break;
            case CONTENT_TYPE.REAL:
                RealOnPageDisable();
                break;
            default:
                break;
        }
    }

    #region TUTORIAL
    protected virtual void TutoBindDelegate()
    {
    }

    protected virtual void TutoOnPageDisable()
    {
        _global.CloseTutoAlert();
    }

    protected virtual void TutoOnPageEnable()
    {
        if (_tutoData != null)
        {
            _topBar.SetBubble(_tutoData._textNarration);
        }
        _navigation.ActivateNextButton(false);
        StartCheckInteraction();
    }

    protected virtual void StartCheckInteraction()
    {
        _tutoInteractionChecked = false;

        if (_tutoInteractionCoroutine != null)
            StopCoroutine(_tutoInteractionCoroutine);
        _tutoInteractionCoroutine = StartCoroutine(CheckInteraction());
    }

    protected virtual void StopCheckIntercation()
    {
        if (_tutoInteractionCoroutine != null)
            StopCoroutine(_tutoInteractionCoroutine);
    }

    private IEnumerator CheckInteraction()
    {
        float duration = 5;
        float t = 0;

        while(t < duration)
        {
            if (_tutoInteractionChecked)
                break;
            t += Time.deltaTime;
            yield return null;
        }

        if (!_tutoInteractionChecked)
        {
            TutoAlert();
        }
    }

    protected virtual void OnTutoInteraction()
    {
        _tutoInteractionChecked = true;
    }

    protected virtual void TutoAlert()
    {
        if (_tutoData != null)
        {
            _global.OpenTutoAlert(_tutoData._pointSize, _highlightObjects[_highlightIndex], _tutoData._textAlert);
        }
    }
    #endregion

    #region FREE
    protected virtual void FreeBindDelegate()
    {

    }

    protected virtual void FreeOnPageEnable()
    {
        _navigation.ActivateNavigation(true);
        _navigation.ActivateNextButton(false);
        _navigation.ActivateHomeButton(true);
        _navigation.ActivatePreButton(true);

        if(_highlight != null)
            _highlight.SetActive(false);
    }

    protected virtual void FreeOnPageDisable()
    {

    }
    #endregion

    #region REAL
    protected virtual void RealBindDelegate()
    {

    }

    protected virtual void RealOnPageEnable()
    {
        _navigation.ActivateNavigation(true);
        _navigation.ActivateNextButton(false);
        _navigation.ActivateHomeButton(true);
        _navigation.ActivatePreButton(true);

        if (_highlight != null)
            _highlight.SetActive(false);
    }

    protected virtual void RealOnPageDisable()
    {

    }
    #endregion

    //TODO : setting value by content type
    //protected virtual void SetTuto
}
