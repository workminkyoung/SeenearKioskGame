using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class TicketBuyOrderList : MonoBehaviour
{
    public TMP_Text payTxt;
    public TMP_Text movieInfoTxt;
    public Sprite[] movieThumnailArray;
    public Image movieThumnail;

    private void OnEnable()
    {
        CinemaPageManager.instance.ticketBuyCurrentPageID = (int)TicketBuyPage.OderListPage; 
        SetOderList();
    }

    private void OnDisable()
    {
        payTxt.text = null;
        movieInfoTxt.text = null;
    }

    public void SetOderList()
    {
        movieThumnail.sprite = movieThumnailArray[CinemaDataManager.instance.movieThumnailID];

        payTxt.text = CinemaDataManager.instance.payAmount.ToString() + "원";
        movieInfoTxt.text = CinemaDataManager.instance.movieInfo + "\n" + "\n";

        if(CinemaDataManager.instance.headCount[0] != 0)
        {
            movieInfoTxt.text +="성인" + CinemaDataManager.instance.headCount[0] + " 매 ";
        }

        if(CinemaDataManager.instance.headCount[1] != 0)
        {
            movieInfoTxt.text +="청소년" + CinemaDataManager.instance.headCount[1] + " 매";  
        }

        int sumHeadcount = CinemaDataManager.instance.headCount[1] + CinemaDataManager.instance.headCount[0];
        movieInfoTxt.text += "\n" + CinemaDataManager.instance.seatNumber + "(" + sumHeadcount + ")"; 

    }
}
