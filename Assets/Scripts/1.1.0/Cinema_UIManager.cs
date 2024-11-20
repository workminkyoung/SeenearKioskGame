using KioskApp.Cinema;
using KioskApp.Tutorial;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinema_UIManager : MonoBehaviour
{
    [SerializeField]
    private CINEMA_TYPE _cinemaType;
    [SerializeField]
    private CONTENT_TYPE _contentType;

    private UC_TopBar _topBar;
    private UC_Navigation _navigation;
    private UC_MissionPopup _missionPopup;
    private UC_Global _global;

    private List<UP_PageBase> _pages = new List<UP_PageBase>();
    [SerializeField]
    private List<UP_PageBase> _ticketPages = new List<UP_PageBase>();
    [SerializeField]
    private List<UP_PageBase> _reservationPages = new List<UP_PageBase>();
    //[SerializeField]
    //private UP_PageBase _selectPage;
    [SerializeField]
    private GameObject _ticketObj;
    [SerializeField]
    private GameObject _reservationObj;

    //private Dictionary<CINEMA_TUTO_TICKET, UP_PageBase> _pageDict = new Dictionary<CINEMA_TUTO_TICKET, UP_PageBase>();
    [SerializeField]
    private int _curPage = 0;

    private void Awake()
    {
        _cinemaType = Cinema_UserData.inst._cinemaType;
        _contentType = Cinema_UserData.inst._contentType;
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _topBar = GetComponentInChildren<UC_TopBar>();
        _topBar.Init();
        _navigation = GetComponentInChildren<UC_Navigation>();
        _navigation.Init();
        _missionPopup = GetComponentInChildren<UC_MissionPopup>();
        _missionPopup.Init();
        _missionPopup.gameObject.SetActive(false);
        _global = GetComponentInChildren<UC_Global>();
        _global.Init();

        _missionPopup.OpenQuest = () => _global.Quest.Show(true);

        switch (_cinemaType)
        {
            case CINEMA_TYPE.TICKET:
                _pages = _ticketPages;
                _reservationObj.SetActive(false);
                break;
            case CINEMA_TYPE.RESERVATION:
                _pages = _reservationPages;
                _ticketObj.SetActive(false);
                break;
            default:
                //_pages.Add(_selectPage);
                //_reservationObj.SetActive(false);
                //_ticketObj.SetActive(false);
                break;
        }

        //_pages.AddRange(GetComponentsInChildren<UP_PageBase>());
        (_pages[0] as UP_SelectBook).SetCinemaType(_cinemaType);
        (_pages[_pages.Count-1] as UP_EndPage).SetCinemaType(_cinemaType);
        for (int i = 0; i < _pages.Count; i++)
        {
            _pages[i].SetGlobal(_global);
            _pages[i].SetContentType(_contentType);
            _pages[i].ChangePage = ChangePage;
            _pages[i].NextPage = NextPage;
            _pages[i].PrePage = PrePage;
            _pages[i].SetTopBar(_topBar);
            _pages[i].SetNavigation(_navigation);
            _pages[i].Init();
        }

        SetContentDetail();
    }

    void SetContentDetail()
    {
        _navigation.ChangePage = ChangePage;
        _navigation.NextPage = NextPage;
        _navigation.PrePage = PrePage;

        ChangePage(CINEMA_TICKET.SELECT_BOOK);
        _navigation.ActivateHomeButton(false);
        _navigation.ActivatePreButton(false);
        _navigation.ActivateNextButton(false);

        _topBar.SetContentType(_contentType);

        switch (_contentType)
        {
            case CONTENT_TYPE.TUTO:
                TutoInit();
                break;
            case CONTENT_TYPE.FREE:
                FreeInit();
                break;
            case CONTENT_TYPE.REAL:
                RealInit();
                break;
            default:
                break;
        }
    }

    public void TutoInit()
    {
        //TODO : content name
        _topBar.SetTopContent("학습", "영화관");
        Cinema_UserData.Instance._answer = QUEST_ANSWER.RIGHT;
    }

    public void FreeInit()
    {
        _topBar.SetTopContent("자유", "영화관");
        Cinema_UserData.Instance._answer = QUEST_ANSWER.RIGHT;
    }

    public void RealInit()
    {
        _topBar.SetTopContent("실전", "영화관");
        _missionPopup.Show(true);

        if (_cinemaType == CINEMA_TYPE.TICKET)
        {
            _missionPopup.SetMission();
            _global.Quest.SetMission();
        }
        else
        {
            _missionPopup.SetMission_reservation();
            _global.Quest.SetMission_reservation();
        }
    }

    public void ChangePage<T>(T tutoPage) where T : Enum
    {
        for (int i = 0; i < _pages.Count; i++)
        {
            if (Convert.ToInt32(tutoPage) == i)
            {
                _pages[i].EnablePage(true);   
                Debug.Log(_pages[i].gameObject.name + "Enabled");
            }
            else
            {
                _pages[i].EnablePage(false);   
                Debug.Log(_pages[i].gameObject.name + "Disabled");
            }
        }

        _curPage = Convert.ToInt32(tutoPage);
    }

    public void NextPage()
    {
        if (_curPage < _pages.Count - 1)
        {
            _curPage++;
            switch (_cinemaType)
            {
                case CINEMA_TYPE.TICKET:
                    ChangePage((CINEMA_TICKET)_curPage);
                    break;
                case CINEMA_TYPE.RESERVATION:
                    ChangePage((CINEMA_RESERVATION)_curPage);
                    break;
            }
        }
    }

    public void PrePage()
    {
        if(_curPage > 0)
        {
            _curPage--;
            switch (_cinemaType)
            {
                case CINEMA_TYPE.TICKET:
                    ChangePage((CINEMA_TICKET)_curPage);
                    break;
                case CINEMA_TYPE.RESERVATION:
                    ChangePage((CINEMA_RESERVATION)_curPage);
                    break;
            }
        }
    }
}
