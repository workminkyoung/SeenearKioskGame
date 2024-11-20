using KioskApp.Cinema;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class UC_Check : MonoBehaviour
{
    [SerializeField]
    private GameObject _missionPrefab;

    [SerializeField]
    private Image _startPoint;

    [SerializeField]
    private RectTransform _missionContainer;

    private List<UC_Mission> _missions = new List<UC_Mission>();
    private Button _exitCheck;

    public void Init()
    {
        _exitCheck = GetComponentInChildren<Button>();
        _exitCheck.onClick.AddListener(() => Show(false));
    }

    public void Show(bool state)
    {
        gameObject.SetActive(state);
        if(state)
        {
            _missions.Clear();
            for (int i = 0; i < TextData._questTitle.Length; i++)
            {
                GameObject quest = Instantiate(_missionPrefab, _missionContainer.transform);
                UC_Mission mission = quest.GetComponent<UC_Mission>();
                mission.Init();
                _missions.Add(mission);
            }
            SetMission();
            //CheckAnswer();
        }
    }

    private void SetMission()
    {
        if(Cinema_UserData.inst._cinemaType == KioskApp.Tutorial.CINEMA_TYPE.TICKET)
        {
            _missions[(int)QUEST.TITLE].SetMission(TextData._questTitle[(int)QUEST.TITLE],
                                                   Cinema_UserData.Instance._questMovie._name);
            _missions[(int)QUEST.TIME].SetMission(TextData._questTitle[(int)QUEST.TIME],
                                                   Cinema_UserData.Instance._questMovie._time);
            _missions[(int)QUEST.NUM].SetMission(TextData._questTitle[(int)QUEST.NUM],
                                                   "성인" + Cinema_UserData.Instance._questMovie._adultNum +
                                                   ", 청소년" + Cinema_UserData.Instance._questMovie._teenNum);
            string seats = Cinema_UserData.Instance._questMovie._selecSeats[0];
            for (int i = 1; i < Cinema_UserData.Instance._questMovie._selecSeats.Count; i++)
            {
                seats += ", " + Cinema_UserData.Instance._questMovie._selecSeats[i];
            }
            _missions[(int)QUEST.SEAT].SetMission(TextData._questTitle[(int)QUEST.SEAT], seats);

            for (int i = 0; i < _missions.Count; i++)
            {
                _missions[i].InitAnswer();
            }
        }
        else
        {
            _missions[0].SetMission("예매번호", Cinema_UserData.Instance._questReservation._number);
            for (int i = 1; i < _missions.Count; i++)
            {
                _missions[i].gameObject.SetActive(false);
            }
        }
    }
    public bool CheckAnswer()
    {
        bool isAllRight = true;
        Movie quest = Cinema_UserData.Instance._questMovie;
        Movie answer = Cinema_UserData.Instance._movie;
        quest._selecSeats.Sort();
        answer._selecSeats.Sort();

        if (quest._name == answer._name)
        {
            _missions[(int)QUEST.TITLE].Correct();
        }
        else
        {
            _missions[(int)QUEST.TITLE].Wrong(); 
            isAllRight = false;
        }

        if (quest._time == answer._time)
        {
            _missions[(int)QUEST.TIME].Correct();
        }
        else
        {
            _missions[(int)QUEST.TIME].Wrong();
            isAllRight = false;
        }

        if (quest._adultNum == answer._adultNum &&
           quest._teenNum == answer._teenNum)
        {
            _missions[(int)QUEST.NUM].Correct();
        }
        else
        {
            _missions[(int)QUEST.NUM].Wrong();
            isAllRight = false;
        }

        bool isEqual = true;
        foreach (string seat in quest._selecSeats)
        {
            if (!answer._selecSeats.Contains(seat))
            {
                isEqual = false;
                break;
            }
        }
        if (isEqual)
        {
            _missions[(int)QUEST.SEAT].Correct();
        }
        else
        {
            _missions[(int)QUEST.SEAT].Wrong();
            isAllRight = false;
        }

        return isAllRight;
    }

    public bool CheckAnswer_reservation()
    {
        if(Cinema_UserData.inst._reservation._number ==
           Cinema_UserData.inst._questReservation._number)
        {
            _missions[0].Correct();
            return true;
        }
        else
        {
            _missions[0].Wrong();
            return false;
        }


    }
}
