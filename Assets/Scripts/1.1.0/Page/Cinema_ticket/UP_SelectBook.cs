using KioskApp.Tutorial;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UP_SelectBook : UP_PageBase
{
    List<Button> _btns = new List<Button>();
    private CINEMA_TYPE _cinemaType;

    [SerializeField]
    private TutorialDataTable _tutoDataReservation;
    [SerializeField]
    private GameObject _tutoHighlightReservation;

    public void SetCinemaType(CINEMA_TYPE type)
    {
        _cinemaType = type;
    }

    public override void Init()
    {
        _btns.AddRange(GetComponentsInChildren<Button>());
        base.Init();
    }

    protected override void BindDelegate()
    {
        base.BindDelegate();
        _navigation.ActivateNavigation(false);
        //TODO : 분기타야함
        switch (_cinemaType)
        {
            case CINEMA_TYPE.TICKET:
                _btns[(int)BUTTON.TICKET].onClick.AddListener(() => NextPage());
                break;
            case CINEMA_TYPE.RESERVATION:
                _btns[(int)BUTTON.RESERVATION].onClick.AddListener(() => NextPage());
                _tutoData = _tutoDataReservation;
                break;
            default:
                break;
        }
    }

    protected override void TutoBindDelegate()
    {
        base.TutoBindDelegate();
    }

    protected override void FreeBindDelegate()
    {
        base.FreeBindDelegate();

    }

    protected override void OnPageEnable()
    {
        base.OnPageEnable();
        _navigation.ActivateNavigation(false);
    }

    protected override void TutoOnPageEnable()
    {
        base.TutoOnPageEnable();

        switch (_cinemaType)
        {
            case CINEMA_TYPE.TICKET:
                _highlightIndex = 0;
                _tutoHighlightReservation.SetActive(false);
                _highlight.SetActive(true);
                break;
            case CINEMA_TYPE.RESERVATION:
                _highlightIndex = 1;
                _tutoHighlightReservation.SetActive(true);
                _highlight.SetActive(false);
                break;
            default:
                break;
        }
    }

    protected override void FreeOnPageEnable()
    {
        base.FreeOnPageEnable();
        _tutoHighlightReservation.SetActive(false);
        _global.OpenFreeGuide();
    }

    protected override void RealOnPageEnable()
    {
        base.RealOnPageEnable();
        _tutoHighlightReservation.SetActive(false);
    }

    enum BUTTON
    {
        TICKET = 0,
        RESERVATION
    }
}
