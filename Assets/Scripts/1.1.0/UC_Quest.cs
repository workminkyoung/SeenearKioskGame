using KioskApp.Cinema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UC_Quest : MonoBehaviour
{
    //public GameObject _missionPrefab;
    [SerializeField]
    private Image _bg;
    private List<Button> _btns = new List<Button>();
    private LayoutGroup _container;
    private List<UC_Mission> _missions = new List<UC_Mission>();

    public void Init()
    {
        _btns.AddRange(GetComponentsInChildren<Button>());
        _container = GetComponentInChildren<LayoutGroup>();
        _missions.AddRange(GetComponentsInChildren<UC_Mission>());

        _btns[(int)BUTTON.OPEN].onClick.AddListener(Open);
        _btns[(int)BUTTON.CLOSE].onClick.AddListener(Close);

        for (int i = 0; i < _missions.Count; i++)
        {
            _missions[i].Init();
        }

        Close();
    }

    public void Open()
    {
        _container.gameObject.SetActive(true);
        _btns[(int)BUTTON.CLOSE].gameObject.SetActive(true);
        _bg.gameObject.SetActive(true);
    }

    public void Close()
    {
        _container.gameObject.SetActive(false);
        _btns[(int)BUTTON.CLOSE].gameObject.SetActive(false);
        _bg.gameObject.SetActive(false);
    }

    public void Show(bool state)
    {
        gameObject.SetActive(state);
    }

    public void SetMission()
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

    public void SetMission_reservation()
    {
        _missions[0].SetMission("예매번호", Cinema_UserData.Instance._questReservation._number);
        for (int i = 1; i < _missions.Count; i++)
        {
            _missions[i].gameObject.SetActive(false);
        }
    }

    enum BUTTON
    {
        OPEN = 0,
        CLOSE
    }
}
