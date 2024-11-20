using System;
using System.Collections;
using System.Collections.Generic;
using KioskApp.Cinema;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UC_Bills : MonoBehaviour
{
    private List<TextMeshProUGUI> _texts = new List<TextMeshProUGUI>();
    private Button _exitBtn;
    
    public void Init()
    {
        _texts.AddRange(GetComponentsInChildren<TextMeshProUGUI>());
        _exitBtn = GetComponentInChildren<Button>();
        _exitBtn.onClick.AddListener(() => Show(false));
    }

    public void Show(bool state)
    {
        gameObject.SetActive(state);
        if (state)
        {
            //TODO : 무명 클래스 받아서 진행가능하게 수정하기
            Movie movie = Cinema_UserData.inst._movie;
            string seatName = "";
            foreach (string seat in movie._selecSeats)
            {
                if (seatName.Length <= 0)
                    seatName += seat;
                else
                    seatName += ", " + seat;
            }
            _texts[(int)TEXT.CUR_DATE].text = DateTime.Now.ToString("yyyy-mm-dd HH:mm") + 
                                              "(KIOSK1)";
            _texts[(int)TEXT.TITLE].text = movie._name;
            _texts[(int)TEXT.TIME].text = "<mark=#000000 padding=\"10, 10, 20, 20\">" + movie._time + "</mark>";
            _texts[(int)TEXT.SEAT].text = seatName;
            _texts[(int)TEXT.NUM].text = string.Format("총인원 {0}명 (일반{1}명, 청소년{2}명)",
                movie._adultNum + movie._teenNum, movie._adultNum, movie._teenNum);
        }
    }

    enum TEXT
    {
        CUR_DATE = 0,
        TITLE,
        TIME,
        SEAT,
        NUM
    }
}
