using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MainPage
{
    Home,
    ticketBuyPage,
    ReservationPage
}

public enum ReservationPage
{
    None,
    TicketReservationPage,
    OderListPage
}

public enum TicketBuyPage
{
    None,
    MovieSelectPage,
    SeatSelectPage,
    OderListPage,
    PaymentPage,
    CouponPage
}

public class CinemaPageManager : MonoBehaviour
{
    public MainPage mainPage;
    public static CinemaPageManager instance = null; 
    public int ticketBuyCurrentPageID = 0;
    public int reservationCurrentPageID = 0;
    public GameObject[] ticketBuyPageArray;
    public GameObject[] reservationPageArray;
        
    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
            Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }

    void Start()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "CinemaTutorial" :
                CinemaDataManager.instance.sceneID = 0;
                break;
            case "Cinema" :
                CinemaDataManager.instance.sceneID = 1;
                break;
            case "CinemaPractice" :
                CinemaDataManager.instance.sceneID = 2;
                CinemaDataManager.instance.cinemaPracticeCase = Random.Range(0,1);
                break; 
        }
        InitializeHome();
    }
    public void Exit()
    {
        SceneManager.LoadScene("MainPage");
    }
    public void Replay()
    {
        SceneManager.LoadScene("CinemaFreeLearn");
    }

    public void InitializeHome() //Home
    {
        mainPage = MainPage.Home;
        ticketBuyCurrentPageID = (int)TicketBuyPage.None;
        reservationCurrentPageID = (int)ReservationPage.None;    
        CinemaDataManager.instance.DataInitialize();
        
        for(int i = 0; i < ticketBuyPageArray.Length; i++)
        {
            ticketBuyPageArray[i].SetActive(false);  
        };

        for(int i = 0; i < reservationPageArray.Length; i++)
        {
            reservationPageArray[i].SetActive(false);
        };
        
        ticketBuyPageArray[0].SetActive(true); // 홈 페이지 켜기   
    }

    public void SelectPage(int id) //id = 0 ticketBuyPage, id = 1 reservationPage
    {
        if(id == 0)
        {
            mainPage = MainPage.ticketBuyPage;
        }
        else
        {
            mainPage = MainPage.ReservationPage;
        }
    }

    public void NextPage()
    {
        if(mainPage == MainPage.ticketBuyPage)
        {
            for(int i = 0; i < ticketBuyPageArray.Length; i++)
            {
                ticketBuyPageArray[i].SetActive(false);  
            };

            ticketBuyPageArray[ticketBuyCurrentPageID + 1].SetActive(true);

        }
        else if(mainPage == MainPage.ReservationPage)
        {
            for(int i = 0; i < reservationPageArray.Length; i++)
            {
                reservationPageArray[i].SetActive(false);
            };

            reservationPageArray[reservationCurrentPageID + 1].SetActive(true);         
        }
    }

    public void PreviousPage()
    {
        if(mainPage == MainPage.ticketBuyPage)
        {
            for(int i = 0; i < ticketBuyPageArray.Length; i++)
            {
                ticketBuyPageArray[i].SetActive(false);  
            };

            ticketBuyPageArray[ticketBuyCurrentPageID - 1].SetActive(true);
        }
        else if(mainPage == MainPage.ReservationPage)
        {
            for(int i = 0; i < reservationPageArray.Length; i++)
            {
                reservationPageArray[i].SetActive(false);
            };

            reservationPageArray[reservationCurrentPageID - 1].SetActive(true);         
        }
    }
}
