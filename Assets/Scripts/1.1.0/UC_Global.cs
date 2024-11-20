using KioskApp.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UC_Global : MonoBehaviour
{
    private UC_PopupNotice _notice;
    private UC_PopupEnd _end;
    private UC_TutoAlert _tutoAlert;
    private UC_Quest _quest;
    private UC_FreeGuide _freeGuide;

    public UC_PopupEnd PopupEnd => _end;
    public UC_Quest Quest => _quest;

    public void Init()
    {
        _notice = GetComponentInChildren<UC_PopupNotice>();
        _end = GetComponentInChildren<UC_PopupEnd>();
        _tutoAlert = GetComponentInChildren<UC_TutoAlert>();
        _quest = GetComponentInChildren<UC_Quest>();
        _freeGuide = GetComponentInChildren<UC_FreeGuide>();

        _notice.Init();
        _end.Init();
        _tutoAlert.Init();
        _quest.Init();
        _freeGuide.Init();

        _notice.Show(false);
        _end.Show(false);
        _tutoAlert.Show(false);
        _quest.Show(false);
        _freeGuide.Show(false);
    }

    public void OpenNotice(string text)
    {
        _notice.Show(true);
        _notice.SetText(text);
    }

    public void CloseNotice()
    {
        _notice.Show(false);
    }

    public void OpenPopupEnd()
    {
        _end.Show(true);
    }

    public void ClosePopupEnd()
    {
        _end.Show(false);
    }

    public void OpenTutoAlert(POINT_SIZE size, RectTransform rect, string text = "", POINT_SIZE alertSize = POINT_SIZE.M)
    {
        _tutoAlert.Show(true);
        _tutoAlert.SetActivateHand(size, rect);
        _tutoAlert.SetAlertText(text);
        _tutoAlert.SetAlertBoxSize(alertSize);
    }

    public void CloseTutoAlert()
    {
        _tutoAlert.Show(false);
    }

    public void OpenQuest()
    {
        _quest.Show(true);
        _quest.Open();
    }

    public void CloseQuest()
    {
        _quest.Show(false);
    }

    public void OpenFreeGuide()
    {
        _freeGuide.Show(true);
    }

    public void CloseFreeGuide()
    {
        _freeGuide.Show(false);
    }
}
