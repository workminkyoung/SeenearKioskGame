using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public GameObject reservationPopUp;
    private void OnEnable()
    {
        
    }

    public void SpeedTicketSearching()
    {
        reservationPopUp.SetActive(true);
        Invoke("Popup", 2f);
    }

    public void Popup()
    {
        reservationPopUp.SetActive(false);
        CinemaPageManager.instance.SelectPage(1);
        CinemaPageManager.instance.NextPage();
        CinemaPageManager.instance.NextPage();
    }

}
