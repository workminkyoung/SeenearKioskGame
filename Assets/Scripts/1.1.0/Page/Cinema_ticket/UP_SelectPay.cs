using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UP_SelectPay : UP_PageBase
{
    private List<Button> _btns = new List<Button>();
    private UC_PopupPay _popupPay;
    private UC_PopupPrint _popupPrint;
    private UC_PopupEnd _popupEnd;
    private Coroutine _payRoutine;


    public override void Init()
    {
        _btns.AddRange(UtilityExtensions.GetComponentsOnlyInChildren_NonRecursive<Button>(transform));
        _popupPay = GetComponentInChildren<UC_PopupPay>();
        _popupPrint = GetComponentInChildren<UC_PopupPrint>();
        _popupEnd = GetComponentInChildren<UC_PopupEnd>();
        base.Init();

        _popupPay.Init();
        _popupPrint.Init();
        _popupEnd.Init();
    }

    protected override void BindDelegate()
    {
        base.BindDelegate();
        _btns[0].onClick.AddListener(() =>
        {
            StartPayRoutine();
        });
    }

    protected override void OnPageEnable()
    {
        base.OnPageEnable();
        _navigation.ActivateNavigation(false);
        _popupPay.gameObject.SetActive(false);
        _popupPrint.gameObject.SetActive(false);
        _popupEnd.gameObject.SetActive(false);
    }

    protected override void OnPageDisable()
    {
        base.OnPageDisable();
        if(_payRoutine  != null)
            StopCoroutine( _payRoutine );
    }

    protected override void TutoBindDelegate()
    {
        base.TutoBindDelegate();
        _btns[0].onClick.AddListener(() =>
        {
            _highlight.gameObject.SetActive(false);
            OnTutoInteraction();
        });
    }

    private void OnDisable()
    {
        if (_payRoutine != null)
            StopCoroutine(_payRoutine);
    }

    private void StartPayRoutine()
    {
        _payRoutine = StartCoroutine(PayRoutine());
        _global.CloseTutoAlert();
    }

    private IEnumerator PayRoutine()
    {
        _popupPay.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);

        _popupPay.gameObject.SetActive(false);
        _popupPrint.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);

        _popupPrint.gameObject.SetActive(false);
        _popupEnd.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);

        NextPage();
    }
}
