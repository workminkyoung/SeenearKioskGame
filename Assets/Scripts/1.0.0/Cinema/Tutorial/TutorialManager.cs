using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    // None = 0,
    // TicketBuyPage = 1,
    // MovieSelectPage = 6,
    // MovieTimeSelectPage = 8,
    // HeadCountSelectPage = 10,
    // SeatSelectPage = 12,
    // AnotherSeatSelectPage = 14,
    // SeatCancelPage = 16,
    // BuyOderListPage = 17,
    // PaymentPage = 18,
    // TicketReservationPage = 21,
    // ReservationNumberSearchPage = 23,
    // EnterReservationNumberPage = 24,
    // ConfirmReservationNumberPage = 26,
    // ReConfirmReservationNumberPage = 27,
    // OrderListPage = 28,
    // SuccessPage = 29

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance = null;

    public int currentPopupId = 0;

    public GameObject popupTutorial;

    public GameObject highlightEffect;

    public Button[] TicketBuyPageBtn;

    public Button[] MovieSelectPageBtn;
    public Toggle[] MovieSelectPageToggle;

    public Button[] MovieTimeSelectPageBtn;
    public Toggle[] MovieTimeSelectPageToggle;

    public Button[] HeadCountSelectPageBtn;

    public Button[] SeatSelectPageBtn;

    public Button[] AnotherSeatSelectPageBtn;

    public Button[] SeatCancelPageBtn;

    public Button[] BuyOderListPageBtn;

    public Button[] PaymentPageBtn;

    public Button[] TicketReservationPageBtn;

    public Button[] ReservationNumberSearchPageBtn;

    public Button[] EnterReservationNumberPageBtn;

    public Button[] ConfirmReservationNumberPageBtn;

    public Button[] ReConfirmReservationNumberPageBtn;

    public Button[] OrderListPageBtn;

    public GameObject successPopup;

    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
            Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }

    private void Start()
    {
        highlightEffect.SetActive(false);
    }

    public void HighlightPosSet(string pos)
    {
        highlightEffect.SetActive(false);
        float x = float.Parse(pos.Split(',')[0]);
        float y = float.Parse(pos.Split(',')[1]);
        highlightEffect.transform.localPosition = new Vector3(x, y, 0);
        highlightEffect.SetActive(true);
    }

    public void TutorialOpen()
    {
        popupTutorial.SetActive(true);
        highlightEffect.SetActive(false);
    }

    public void BtnActive(Button btn)
    {
        btn.interactable = true;
    }

    public void PageSet(int id)
    {
        switch (id - 1)
        {
            case 1:
                popupTutorial.SetActive(false);
                HighlightPosSet("-215, -430");
                for (int i = 0; i < TicketBuyPageBtn.Length; i++)
                {
                    TicketBuyPageBtn[i].interactable = false;
                }
                break;
            case 6:
                popupTutorial.SetActive(false);
                HighlightPosSet("-345, 110");

                for (int i = 0; i < MovieSelectPageBtn.Length; i++)
                {
                    MovieSelectPageBtn[i].interactable = false;
                }

                for (int i = 0; i < MovieSelectPageToggle.Length; i++)
                {
                    MovieSelectPageToggle[i].interactable = false;
                }

                break;
            case 8:
                popupTutorial.SetActive(false);
                HighlightPosSet("115, 560");

                for (int i = 0; i < MovieSelectPageBtn.Length; i++)
                {
                    MovieSelectPageBtn[i].interactable = true;
                }

                for (int i = 0; i < MovieSelectPageToggle.Length; i++)
                {
                    MovieSelectPageToggle[i].interactable = true;
                }

                for (int i = 0; i < MovieTimeSelectPageBtn.Length; i++)
                {
                    MovieTimeSelectPageBtn[i].interactable = false;
                }

                for (int i = 0; i < MovieTimeSelectPageToggle.Length; i++)
                {
                    MovieTimeSelectPageToggle[i].interactable = false;
                }

                MovieSelectPageBtn[2].interactable = false;
                break;
            case 10:
                popupTutorial.SetActive(false);
                HighlightPosSet("-270, 70");

                for (int i = 0; i < HeadCountSelectPageBtn.Length; i++)
                {
                    HeadCountSelectPageBtn[i].interactable = false;
                }
                
                HeadCountSelectPageBtn[2].interactable = true;
                break;
            case 12:
                popupTutorial.SetActive(false);
                HighlightPosSet("400, -280");

                for (int i = 0; i < SeatSelectPageBtn.Length; i++)
                {
                    SeatSelectPageBtn[i].interactable = false;
                }
                break;
            case 14:
                popupTutorial.SetActive(false);
                HighlightPosSet("10, -140");

                for (int i = 0; i < SeatSelectPageBtn.Length; i++)
                {
                    SeatSelectPageBtn[i].interactable = true;
                }

                for (int i = 0; i < AnotherSeatSelectPageBtn.Length; i++)
                {
                    AnotherSeatSelectPageBtn[i].interactable = false;
                }
                
                break;
            case 16:
                popupTutorial.SetActive(false);
                HighlightPosSet("400, -280");

                for (int i = 0; i < AnotherSeatSelectPageBtn.Length; i++)
                {
                    AnotherSeatSelectPageBtn[i].interactable = true;
                }

                for (int i = 0; i < SeatCancelPageBtn.Length; i++)
                {
                    SeatCancelPageBtn[i].interactable = false;
                }             
                break;

            case 17:
                popupTutorial.SetActive(false);
                HighlightPosSet("340, -820");
                for (int i = 0; i < BuyOderListPageBtn.Length; i++)
                {
                    BuyOderListPageBtn[i].interactable = false;
                }
                break;
            case 18:
                popupTutorial.SetActive(false);
                HighlightPosSet("-230, -100");
                for (int i = 0; i < PaymentPageBtn.Length; i++)
                {
                    PaymentPageBtn[i].interactable = false;
                }
                break;
            case 21:
                TicketBuyBtnReset();
                popupTutorial.SetActive(false);
                HighlightPosSet("250, -430");
                for (int i = 0; i < TicketReservationPageBtn.Length; i++)
                {
                    TicketReservationPageBtn[i].interactable = false;
                }
                break;
            case 23:
                popupTutorial.SetActive(false);
                HighlightPosSet("-230, -25");
                for (int i = 0; i < ReservationNumberSearchPageBtn.Length; i++)
                {
                    ReservationNumberSearchPageBtn[i].interactable = false;
                }
                break;
            case 24:
                popupTutorial.SetActive(false);
                for (int i = 0; i < EnterReservationNumberPageBtn.Length; i++)
                {
                    EnterReservationNumberPageBtn[i].interactable = false;
                }
                
                KeypadTuTo();
                break;
            case 26:
                popupTutorial.SetActive(false);
                HighlightPosSet("0, -275");
                EnterReservationNumberPageBtn[13].interactable = true;
                for (int i = 0; i < ConfirmReservationNumberPageBtn.Length; i++)
                {
                    ConfirmReservationNumberPageBtn[i].interactable = false;
                }
                break;
            case 27:
                popupTutorial.SetActive(false);
                HighlightPosSet("-150, -265");
                for (int i = 0; i < ReConfirmReservationNumberPageBtn.Length; i++)
                {
                    ReConfirmReservationNumberPageBtn[i].interactable = false;
                }
                break;
            case 28:
                popupTutorial.SetActive(false);
                HighlightPosSet("340, -820");

                for (int i = 0; i < OrderListPageBtn.Length; i++)
                {
                    OrderListPageBtn[i].interactable = false;
                }
                break;
            case 29:
                popupTutorial.SetActive(false);
                break;
        }
    }

    string num = "1234567891";
    int k = -1;

    public void KeypadTuTo()
    {
        if(k == 9)
        {
            for (int i = 0; i < EnterReservationNumberPageBtn.Length; i++)
            {
                EnterReservationNumberPageBtn[i].interactable = false;
            }

            TutorialOpen();
            return;
        }
        else
        {
            k++;

            for (int i = 0; i < 10; i++)
            {
                EnterReservationNumberPageBtn[i].interactable = false;
            }

            EnterReservationNumberPageBtn[int.Parse(num[k].ToString())].interactable = true;
                
            highlightEffect.SetActive(false);
            highlightEffect.transform.position = EnterReservationNumberPageBtn[int.Parse(num[k].ToString())].transform.position;
            highlightEffect.SetActive(true);
        }
    }

    public void TicketBuyBtnReset()
    {
        for (int i = 0; i < TicketBuyPageBtn.Length; i++)
        {
            TicketBuyPageBtn[i].interactable = true;
        }
        for (int i = 0; i < MovieSelectPageBtn.Length; i++)
        {
            MovieSelectPageBtn[i].interactable = true;
        }
        for (int i = 0; i < MovieSelectPageToggle.Length; i++)
        {
            MovieSelectPageToggle[i].interactable = true;
        }
        for (int i = 0; i < MovieTimeSelectPageBtn.Length; i++)
        {
            MovieTimeSelectPageBtn[i].interactable = true;
        }
        for (int i = 0; i < MovieTimeSelectPageToggle.Length; i++)
        {
            MovieTimeSelectPageToggle[i].interactable = true;
        }
        for (int i = 0; i < HeadCountSelectPageBtn.Length; i++)
        {
            HeadCountSelectPageBtn[i].interactable = false;
        }
        for (int i = 0; i < SeatSelectPageBtn.Length; i++)
        {
            SeatSelectPageBtn[i].interactable = false;
        }
        for (int i = 0; i < AnotherSeatSelectPageBtn.Length; i++)
        {
            AnotherSeatSelectPageBtn[i].interactable = false;
        }
        for (int i = 0; i < SeatCancelPageBtn.Length; i++)
        {
            SeatCancelPageBtn[i].interactable = false;
        }
        for (int i = 0; i < BuyOderListPageBtn.Length; i++)
        {
            BuyOderListPageBtn[i].interactable = false;
        }
        for (int i = 0; i < PaymentPageBtn.Length; i++)
        {
            PaymentPageBtn[i].interactable = false;
        }
    }
}
