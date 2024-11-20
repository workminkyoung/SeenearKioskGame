using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketReservation : MonoBehaviour
{
    public GameObject[] popupArray;
    public GameObject[] keypadPopUpTxt;
    public GameObject[] numConfirmPopUpTxt;
    public int searchingNumID = 0;
    private void OnEnable()
    {
        CinemaPageManager.instance.reservationCurrentPageID = (int)ReservationPage.TicketReservationPage;
    }

    private void OnDisable()
    {
        for(int i = 0; i < popupArray.Length; i++)
        {
            popupArray[i].SetActive(false);   
        }

        for(int i = 0; i < keypadPopUpTxt.Length; i++)
        {
            keypadPopUpTxt[searchingNumID].SetActive(false);
            numConfirmPopUpTxt[searchingNumID].SetActive(false);  
        }
        searchingNumID = 0;
    }

    public void SelectNum(int id)
    {
        //팝업 예매번호 or 핸드폰 번호 구분
        for(int i = 0; i < keypadPopUpTxt.Length; i++)
        {
            keypadPopUpTxt[searchingNumID].SetActive(false);
            numConfirmPopUpTxt[searchingNumID].SetActive(false);  
        }
        searchingNumID = id;
    }

    public void NextPopup(int id) // searchingNumID=0 예매번호 조회, searchingNumID=1 휴대폰번호 조회
    {
        popupArray[id].SetActive(true);

        //팝업 예매번호 or 핸드폰 번호 구분
        keypadPopUpTxt[searchingNumID].SetActive(true);
        numConfirmPopUpTxt[searchingNumID].SetActive(true);

        if(id == 2)
        {
            Invoke("SearchReservationInvoke", 3f);
        }
    }
    public void PreviousPopup(int id) // id=0 예매번호 조회, id=1 휴대폰번호 조회
    {
        popupArray[id].SetActive(false);


        if(id == 3) //예매 조회 실패 -> 키패드 팝업으로 이동
        {
            popupArray[id-1].SetActive(false);
            popupArray[id-2].SetActive(false);
        }
    }

    public void SearchReservationInvoke()
    {
        CinemaPageManager.instance.NextPage();
    }
}
