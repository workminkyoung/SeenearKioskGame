using KioskApp.Cinema;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UC_MissionPopup : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMissionTitle;
    private LayoutGroup _missionContainer;
    private Button _btnStart;

    private List<UC_Mission> _missions = new List<UC_Mission>();
    public Action OpenQuest;

    //[SerializeField]
    //private MissionDict _missionDict;
    //[Serializable]
    //private class MissionDict : SerializableDictionaryBase<UC_Mission, string> { };

    public void Init()
    {
        _missionContainer = GetComponentInChildren<LayoutGroup>();
        _missions.AddRange(GetComponentsInChildren<UC_Mission>());
        _btnStart = GetComponentInChildren<Button>();
        _btnStart.onClick.AddListener(() => 
        {
            OpenQuest?.Invoke();
            gameObject.SetActive(false); 
        });

        for (int i = 0; i < _missions.Count; i++)
        {
            _missions[i].Init();
        }
    }

    public void SetTitle(string title)
    {
        _textMissionTitle.text = title;
    }

    public void SetMission()
    {
        RandomMission();

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

    private void RandomMission()
    {
        Movie movie = new Movie();
        
        //check random repetition
        int adultNum = Random.Range(0, TextData._numMax);
        int teenNum = Random.Range(0, TextData._numMax);
        while (adultNum == 0 && teenNum == 0)
        {
            adultNum = Random.Range(0, TextData._numMax);
            teenNum = Random.Range(0, TextData._numMax);
        }
        int seatAlphabetIndex = Random.Range(0, TextData._seatAlphabet.Length);
        string seatAlphabet = TextData._seatAlphabet[seatAlphabetIndex];
        //string teenSeat = TextData._seatAlphabet[Random.Range(0, TextData._seatAlphabet.Length)];
        //while (teenSeat == adultSeat)
        //{
        //    teenSeat = TextData._seatAlphabet[Random.Range(0, TextData._seatAlphabet.Length)];
        //}
        int seatStart = Random.Range(1, TextData._seatMax - adultNum);
        //int teenSeatStart = Random.Range(1, TextData._seatMax - teenNum);

        //set mission
        movie._name = TextData._names[Random.Range(0, TextData._names.Length)];
        movie._time = TextData._times[Random.Range(0, TextData._times.Length)];
        movie._adultNum = adultNum;
        movie._teenNum = teenNum;
        for (int i = 0; i < movie._teenNum + movie._adultNum; i++)
        {
            //if(seatStart > 8)
            //{
            //    seatStart = 1;
            //    seatNum++;
            //}

            //if (seatNum >= TextData._seatAlphabet.Length)
            //{
            //    seatNum = 0;
            //}
            seatAlphabet = TextData._seatAlphabet[seatAlphabetIndex];
            string nSeat = seatAlphabet + seatStart.ToString();
            while (TextData._occupiedSeats.Contains(nSeat) ||
                   movie._selecSeats.Contains(nSeat))
            {
                seatStart++;

                if (seatStart > 8)
                {
                    seatStart = 1;
                    seatAlphabetIndex++;
                    if (seatAlphabetIndex >= TextData._seatAlphabet.Length)
                    {
                        seatAlphabetIndex = 0;
                    }
                }

                seatAlphabet = TextData._seatAlphabet[seatAlphabetIndex];
                nSeat = seatAlphabet + seatStart.ToString();
            }

            movie._selecSeats.Add(nSeat);

            seatStart++;
            if (seatStart > 8)
            {
                seatStart = 1;
                seatAlphabetIndex++;
                if (seatAlphabetIndex >= TextData._seatAlphabet.Length)
                {
                    seatAlphabetIndex = 0;
                }
            }
        }
        //for (int i = teenSeatStart; i < teenSeatStart + movie._teenNum; i++)
        //{
        //    movie._selecSeats.Add(seat + i.ToString());
        //}

        Cinema_UserData.Instance._questMovie = movie;
        Debug.Log("set random mission");
    }

    public void SetMission_reservation()
    {
        int randomNum = Random.Range(00000000, 99999999);
        Cinema_UserData.Instance._questReservation._number = randomNum.ToString();
        _missions[0].SetMission("예매번호", Cinema_UserData.Instance._questReservation._number);
        for (int i = 1; i < _missions.Count; i++)
        {
            _missions[i].gameObject.SetActive(false);
        }
    }

    public void Show(bool state)
    {
        gameObject.SetActive(state);
        if (state)
        {
            //for (int i = 0; i < _missions.Count; i++)
            //{
            //    _missions[i].InitAnswer();
            //}
        }
    }
}
