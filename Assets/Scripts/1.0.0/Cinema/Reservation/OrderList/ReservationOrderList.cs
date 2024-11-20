using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservationOrderList : MonoBehaviour
{
    public GameObject[] popupArray;
    private void OnEnable()
    {
        CinemaPageManager.instance.reservationCurrentPageID = (int)ReservationPage.OderListPage;
    }
    private void OnDisable()
    {
        for(int i = 0; i < popupArray.Length; i++)
        {
            popupArray[i].SetActive(false);
        }
    }

    public void NextPopup(int id) // searchingNumID=0 예매번호 조회, searchingNumID=1 휴대폰번호 조회
    {
        popupArray[id].SetActive(true);
    }
    public void PreviousPopup(int id) // id=0 예매번호 조회, id=1 휴대폰번호 조회
    {
        popupArray[id].SetActive(false);
    }
}
