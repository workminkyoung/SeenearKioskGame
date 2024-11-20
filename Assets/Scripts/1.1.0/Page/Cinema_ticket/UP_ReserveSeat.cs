using KioskApp.Cinema;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UP_ReserveSeat : UP_PageBase
{
    //private Dictionary<string, UC_Seat> _seatDict = new Dictionary<string, UC_Seat>();
    [SerializeField]
    private List<UC_Seat> _tutoSeats = new List<UC_Seat>();
    private List<UC_Seat> _seats = new List<UC_Seat>();
    [SerializeField]
    private List<string> _selectSeats = new List<string>();

    //creat seat
    [SerializeField]
    private string[] _occupiedSeatName;
    [SerializeField]
    private string[] _tutoSeatName;
    [SerializeField]
    private GameObject _seatPrefab;
    private List<LayoutGroup> _seatGroup = new List<LayoutGroup>();
    private string[] _seatName = { "A", "B", "C", "D", "E"};
    private const int _startNumLeft = 1;
    private const int _startNumMiddle = 3;
    private const int _startNuRight = 7;
    private const int _rowMax = 5;

    Dictionary<string, UC_Seat> _selectSeatDict = new Dictionary<string, UC_Seat>();

    public override void Init()
    {
        _seatGroup.AddRange(GetComponentsInChildren<LayoutGroup>());
        CreatSeat();
        _seats.AddRange(GetComponentsInChildren<UC_Seat>());
        base.Init();
    }

    private void CreatSeat()
    {
        //creat left
        for (int i = 0; i < _rowMax; i++)
        {
            string name = _seatName[i];
            for (int j = 0; j < 2; j++)
            {
                string seatName = name + (_startNumLeft + j);

                GameObject seat = Instantiate(_seatPrefab, _seatGroup[(int)SEAT_GROUP.LEFT].transform);
                UC_Seat uc_seat = seat.GetComponent<UC_Seat>();
                uc_seat.Init();
                uc_seat.SetName(seatName);
                uc_seat.SetOccupied(_occupiedSeatName.Contains(seatName));
            }
        }

        //creat middle
        for (int i = 0; i < _rowMax; i++)
        {
            string name = _seatName[i];
            for (int j = 0; j < 4; j++)
            {
                string seatName = name + (_startNumMiddle + j);

                GameObject seat = Instantiate(_seatPrefab, _seatGroup[(int)SEAT_GROUP.MIDDLE].transform);
                UC_Seat uc_seat = seat.GetComponent<UC_Seat>();
                uc_seat.Init();
                uc_seat.SetName(seatName);
                uc_seat.SetOccupied(_occupiedSeatName.Contains(seatName));
            }
        }

        //creat right
        for (int i = 0; i < _rowMax; i++)
        {
            string name = _seatName[i];
            for (int j = 0; j < 2; j++)
            {
                string seatName = name + (_startNuRight + j);

                GameObject seat = Instantiate(_seatPrefab, _seatGroup[(int)SEAT_GROUP.RIGHT].transform);
                UC_Seat uc_seat = seat.GetComponent<UC_Seat>();
                uc_seat.Init();
                uc_seat.SetName(seatName);
                uc_seat.SetOccupied(_occupiedSeatName.Contains(seatName));
            }
        }
    }

    public override void ResetPage()
    {
        base.ResetPage();
        _selectSeats = new List<string>();
    }

    protected override void BindDelegate()
    {
        base.BindDelegate();
        for (int i = 0; i < _seats.Count; i++)
        {
            _seats[i].SelectSeat = SelectSeat;
            _seats[i].DeselectSeat = DeselectSeat;
        }
    }

    protected override void OnPageEnable()
    {
        base.OnPageEnable();
        _navigation.SetNextAction(() =>
        {
            Cinema_UserData.Instance._movie._selecSeats = _selectSeats;
            NextPage();
        });

        if (Cinema_UserData.Instance._movie._selecSeats.Count ==0 && _selectSeatDict.Count > 0)
        {
            foreach (var pair in _selectSeatDict)
            {
                _selectSeatDict[pair.Key].SetOccupied(false);
            }

            _selectSeatDict.Clear();
            _selectSeatDict = new Dictionary<string, UC_Seat>();
            _selectSeats.Clear();
            _selectSeats = new List<string>();
        }
    }

    protected override void OnPageDisable()
    {
        base.OnPageDisable();
    }

    protected override void TutoBindDelegate()
    {
        base.TutoBindDelegate();

        for (int i = 0; i < _seats.Count; i++)
        {
            if (_tutoSeatName.Contains(_seats[i].Text.text))
            {
                _tutoSeats.Add(_seats[i]);
                _highlightObjects.Add(_seats[i].transform.GetComponent<RectTransform>());
            }
        }

        for (int i = 0; i < _tutoSeats.Count; i++)
        {
            _tutoSeats[i].Toggle.onValueChanged.AddListener((state) =>
            {
                if (state)
                {
                    if (_selectSeats.Count >= 2)
                    {
                        _navigation.ActivateNextButton(true);
                    }
                }
            });
        }
    }

    protected override void TutoOnPageEnable()
    {
        base.TutoOnPageEnable();
        for (int i = 0; i < _seats.Count; i++)
        {
            _seats[i].Toggle.enabled = false;
        }
        for (int i = 0; i < _tutoSeats.Count; i++)
        {
            _tutoSeats[i].Toggle.enabled = true;
        }
    }

    protected override void FreeBindDelegate()
    {
        base.FreeBindDelegate();
        for (int i = 0; i < _seats.Count; i++)
        {
            _seats[i].Toggle.onValueChanged.AddListener((state) =>
            {
                int num = Cinema_UserData.Instance._movie._adultNum +
                          Cinema_UserData.Instance._movie._teenNum;

                _navigation.ActivateNextButton(_selectSeats.Count == num ? true : false);
            });
        }
    }

    protected override void RealBindDelegate()
    {
        base.RealBindDelegate();
        for (int i = 0; i < _seats.Count; i++)
        {
            _seats[i].Toggle.onValueChanged.AddListener((state) =>
            {
                int num = Cinema_UserData.Instance._movie._adultNum +
                          Cinema_UserData.Instance._movie._teenNum;

                _navigation.ActivateNextButton(_selectSeats.Count == num ? true : false);
            });
        }
    }

    private void SelectSeat(string seatName, UC_Seat seat)
    {
        _selectSeats.Add(seatName);
        _selectSeatDict.Add(seatName, seat);
        //if (_selectSeats.Count < Cinema_UserData.inst._movie._teenNum + Cinema_UserData.inst._movie._adultNum)
        //{
        //}
    }

    private void DeselectSeat(string seatName, UC_Seat seat)
    {
        if(_selectSeats.Contains(seatName))
            _selectSeats.Remove(seatName);
        if(_selectSeatDict.ContainsKey(seatName))
            _selectSeatDict.Remove(seatName);
    }

    enum SEAT_GROUP
    {
        LEFT = 0,
        MIDDLE,
        RIGHT
    }
}
