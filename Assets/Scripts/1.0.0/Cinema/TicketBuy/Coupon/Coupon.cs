using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coupon : MonoBehaviour
{
    public GameObject[] popupArray;
    private void OnEnable()
    {
        CinemaPageManager.instance.ticketBuyCurrentPageID = (int)TicketBuyPage.CouponPage; 
    }

    private void OnDisable()
    {
        for(int i = 0; i < popupArray.Length; i++)
        {
            popupArray[i].SetActive(false);
        }
    }
    
    public void ShowPopup(int id) // id=0 쿠폰사용, VIP 쿠폰, id=1 문화누리, id=2 결제완료 팝업
    {
        popupArray[id].SetActive(true);
    }

    public void HidePopup(int id)
    {
        popupArray[id].SetActive(false);
    }
}
