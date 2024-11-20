using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Payment : MonoBehaviour
{
    public GameObject[] popupArray;
    public GameObject[] resultPageArray;
    private void OnEnable()
    {
        CinemaPageManager.instance.ticketBuyCurrentPageID = (int)TicketBuyPage.PaymentPage; 
    }

    private void OnDisable()
    {
        for(int i = 0; i < popupArray.Length; i++)
        {
            popupArray[i].SetActive(false);
        }
    }

    public void NextPopup(int id) // id=0 카드결제, id=2 결제완료 팝업
    {
        popupArray[id].SetActive(true);
        if(id == 0)
        {
            StartCoroutine(PopupCo());
        }
    }

    IEnumerator PopupCo()
    {
        yield return new WaitForSecondsRealtime(1f);
        popupArray[1].SetActive(true);
    }

    public void PreviousPopup()
    {
        StopAllCoroutines();

        for(int i = 0; i < popupArray.Length; i++)
        {
            popupArray[i].SetActive(false);
        }
    }

    public void ResultShow()
    {
        if(CinemaDataManager.instance.movieThumnailID == 0 && CinemaDataManager.instance.movieInfo.Contains("10:20") && CinemaDataManager.instance.headCount[0] == 2 && 
            CinemaDataManager.instance.headCount[1] == 0 && CinemaDataManager.instance.seatNumber == "D5, D6")
            {
                resultPageArray[0].SetActive(true);
                resultPageArray[1].SetActive(true);
                resultPageArray[2].SetActive(false);
            }
        else
        {
            resultPageArray[0].SetActive(true);
            resultPageArray[1].SetActive(false);
            resultPageArray[2].SetActive(true);
        }
    }

    public void ResultShowFreelearn()
    {
        resultPageArray[0].SetActive(true);
        resultPageArray[1].SetActive(true);
        resultPageArray[2].SetActive(false);
    }
}
