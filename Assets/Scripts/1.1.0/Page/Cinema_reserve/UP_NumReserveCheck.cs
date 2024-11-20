using DG.Tweening.Plugins;
using KioskApp.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UP_NumReserveCheck : UP_PageBase
{
    private Coroutine _waitRoutine;

    private string[] _notices =
    {
        "잠시만 기다려주세요.",
        "티켓을 출력 중입니다."
    };

    protected override void OnPageEnable()
    {
        base.OnPageEnable();
        _navigation.SetNextAction(() => OnWaitNotice());
        _navigation.ActivateNextButton(true);
        _navigation.ActivateNavigation(true);
        _navigation.SetPosition(0, -500);

        //add popup end action
        _global.PopupEnd.OnPopupCancel = () =>
        {
            if (_waitRoutine != null)
            {
                StopCoroutine(_waitRoutine);
            }
        };
    }

    protected override void OnPageDisable()
    {
        base.OnPageDisable();
        _navigation.ResetNextAction();

        _global.PopupEnd.OnPopupCancel = null;
    }

    private void OnWaitNotice()
    {
        OnTutoInteraction();
        _global.CloseTutoAlert();
        if(_waitRoutine != null)
        {
            StopCoroutine(_waitRoutine);
        }
        _waitRoutine = StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        _global.OpenNotice(_notices[0]);
        yield return new WaitForSecondsRealtime(5f);
        _global.OpenNotice(_notices[1]);
        yield return new WaitForSecondsRealtime(5f);

        //onEnd
        _global.CloseNotice();
        _global.OpenPopupEnd();
        yield return new WaitForSecondsRealtime(5f);
        _global.ClosePopupEnd();
        NextPage();
    }
}
