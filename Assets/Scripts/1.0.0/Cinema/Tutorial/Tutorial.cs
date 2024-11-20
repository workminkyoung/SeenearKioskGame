using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject indicator;
    public GameObject successPopup;

    public Text tutorialText;

    public List<string> textList = new List<string>();

    public List<Button> ticketBuyBtn  = new List<Button>();

    public List<Button> cinemaSelectbtn = new List<Button>();
    public List<Toggle> cinemaSelectToggle = new List<Toggle>();

    public List<Button> headCountbtn = new List<Button>();

    public List<Button> seatSelectbtn = new List<Button>();

    public List<Button> orderListbtn = new List<Button>();

    public List<Button> paybtn = new List<Button>();

    public List<Button> reservationTicket = new List<Button>();

    public List<Button> reservationNumberBtn = new List<Button>();

    public List<Button> reservatuinNumberInput = new List<Button>();

    public List<Button> ReservedOrderListbtn = new List<Button>();

    public GameObject[] selectSeatArray;

    void Start()
    {
        NextTutoPage(0);

    }

    void Update()
    {
        
    }

    public void ResetInteractable()
    {
        for (int i = 0; i < ticketBuyBtn.Count; i++)
        {
            ticketBuyBtn[i].interactable = true;
        }
        for (int i = 0; i < cinemaSelectbtn.Count; i++)
        {
            cinemaSelectbtn[i].interactable = true;
        }
        for (int i = 0; i < cinemaSelectToggle.Count; i++)
        {
            cinemaSelectToggle[i].interactable = true;
        }
        for (int i = 0; i < headCountbtn.Count; i++)
        {
            headCountbtn[i].interactable = true;
        }
        for (int i = 0; i < seatSelectbtn.Count; i++)
        {
            seatSelectbtn[i].interactable = true;
        }
        for (int i = 0; i < orderListbtn.Count; i++)
        {
            orderListbtn[i].interactable = true;
        }
        for (int i = 0; i < paybtn.Count; i++)
        {
            paybtn[i].interactable = true;
        }
        for (int i = 0; i < reservationTicket.Count; i++)
        {
             reservationTicket[i].interactable = true;
        }
        for (int i = 0; i < reservationNumberBtn.Count; i++)
        {
            reservationNumberBtn[i].interactable = true;
        }
        for (int i = 0; i < reservatuinNumberInput.Count; i++)
        {
            reservatuinNumberInput[i].interactable = true;
        }
        for (int i = 0; i < ReservedOrderListbtn.Count; i++)
        {
            ReservedOrderListbtn[i].interactable = true;
        }
    }

    public void NextTutoPage(int k)
    {
        switch(k)
        {
            case 0:
                ResetInteractable();

                for (int i = 0; i < ticketBuyBtn.Count; i++)
                {
                    ticketBuyBtn[i].interactable = false;
                }

                ticketBuyBtn[0].interactable = true;
                indicator.transform.localPosition = new Vector3(ticketBuyBtn[0].transform.localPosition.x, ticketBuyBtn[0].transform.localPosition.y - 650, ticketBuyBtn[0].transform.localPosition.z);
                tutorialText.text = null;
                tutorialText.DOText(textList[0], 2f);
                break;

            case 1:
                ResetInteractable();

                for (int i = 0; i < cinemaSelectbtn.Count; i++)
                {
                    cinemaSelectbtn[i].interactable = false;
                }
                for (int i = 0; i < cinemaSelectToggle.Count; i++)
                {
                    cinemaSelectToggle[i].interactable = false;
                }

                cinemaSelectToggle[0].interactable = true;
                indicator.transform.localPosition = new Vector3(cinemaSelectToggle[0].transform.localPosition.x, cinemaSelectToggle[0].transform.localPosition.y - 700, cinemaSelectToggle[0].transform.localPosition.z);
                tutorialText.text = null;
                tutorialText.DOText(textList[1], 2f);
                break;

            case 2:
                cinemaSelectToggle[0].interactable = false;
                cinemaSelectbtn[0].interactable = true;

                indicator.transform.localPosition = new Vector3(cinemaSelectbtn[0].transform.localPosition.x, cinemaSelectbtn[0].transform.localPosition.y - 800, cinemaSelectbtn[0].transform.localPosition.z);
                break;

            case 3:
                ResetInteractable();

                for (int i = 0; i < headCountbtn.Count; i++)
                {
                    headCountbtn[i].interactable = false;
                }

                headCountbtn[3].interactable = true;

                indicator.transform.localPosition = new Vector3(headCountbtn[3].transform.localPosition.x, headCountbtn[3].transform.localPosition.y - 420, headCountbtn[3].transform.localPosition.z);
                tutorialText.text = null;
                tutorialText.DOText(textList[2], 2f);

                break;
            case 4:
                headCountbtn[3].interactable = false;
                headCountbtn[12].interactable = true;

                indicator.transform.localPosition = new Vector3(headCountbtn[12].transform.localPosition.x, headCountbtn[12].transform.localPosition.y - 420, headCountbtn[12].transform.localPosition.z);
                tutorialText.text = null;
                tutorialText.DOText(textList[3], 2f);
                break;
            case 5:
                headCountbtn[12].interactable = false;
                headCountbtn[0].interactable = true;
                indicator.transform.localPosition = new Vector3(headCountbtn[0].transform.localPosition.x, headCountbtn[0].transform.localPosition.y - 500, headCountbtn[0].transform.localPosition.z);
                break;
            case 6:
                ResetInteractable();
                for (int i = 0; i < seatSelectbtn.Count; i++)
                {
                    seatSelectbtn[i].interactable = false;
                }

                seatSelectbtn[68].interactable = true;
                seatSelectbtn[69].interactable = true;

                indicator.transform.localPosition = new Vector3(seatSelectbtn[68].transform.localPosition.x + 168, seatSelectbtn[68].transform.localPosition.y - 860, seatSelectbtn[68].transform.localPosition.z);
                tutorialText.text = null;
                tutorialText.DOText(textList[4], 2f);
                break;
            case 7:
                if (selectSeatArray[0].activeSelf == false && selectSeatArray[1].activeSelf == false)
                {
                    seatSelectbtn[68].interactable = false;
                    seatSelectbtn[69].interactable = false;

                    seatSelectbtn[34].interactable = true;
                    seatSelectbtn[35].interactable = true;

                    indicator.transform.localPosition = new Vector3(seatSelectbtn[34].transform.localPosition.x + 130, seatSelectbtn[34].transform.localPosition.y - 630, seatSelectbtn[34].transform.localPosition.z);
                    tutorialText.text = null;
                    tutorialText.DOText(textList[5], 2f);
                }
                break;
            case 8:
                if (selectSeatArray[2].activeSelf && selectSeatArray[3].activeSelf)
                {
                    seatSelectbtn[34].interactable = false;
                    seatSelectbtn[35].interactable = false;

                    seatSelectbtn[72].interactable = true;

                    indicator.transform.localPosition = new Vector3(seatSelectbtn[72].transform.localPosition.x, seatSelectbtn[72].transform.localPosition.y - 820, seatSelectbtn[72].transform.localPosition.z);
                }
                break;
            case 9:
                ResetInteractable();
                for (int i = 0; i < orderListbtn.Count; i++)
                {
                    orderListbtn[i].interactable = false;
                }

                orderListbtn[0].interactable = true;

                indicator.transform.localPosition = new Vector3(orderListbtn[0].transform.localPosition.x, orderListbtn[0].transform.localPosition.y - 820, orderListbtn[0].transform.localPosition.z);
                tutorialText.text = null;
                tutorialText.DOText(textList[6], 2f);
                break;
            case 10:
                ResetInteractable();
                for (int i = 0; i < paybtn.Count; i++)
                {
                    paybtn[i].interactable = false;
                }

                paybtn[0].interactable = true;

                indicator.transform.localPosition = new Vector3(paybtn[0].transform.localPosition.x, paybtn[0].transform.localPosition.y - 620, paybtn[0].transform.localPosition.z);
                break;
            case 11:
                paybtn[0].interactable = false;
                paybtn[4].interactable = true;

                indicator.transform.localPosition = new Vector3(paybtn[4].transform.localPosition.x, paybtn[4].transform.localPosition.y - 520, paybtn[4].transform.localPosition.z);
                break;
            case 12:
                paybtn[4].interactable = false;
                paybtn[5].interactable = true;

                indicator.transform.localPosition = new Vector3(paybtn[5].transform.localPosition.x, paybtn[5].transform.localPosition.y - 520, paybtn[5].transform.localPosition.z);
                break;
            case 13:
                ResetInteractable();
                for (int i = 0; i < reservationTicket.Count; i++)
                {
                    reservationTicket[i].interactable = false;
                }

                reservationTicket[0].interactable = true;

                indicator.transform.localPosition = new Vector3(reservationTicket[0].transform.localPosition.x, reservationTicket[0].transform.localPosition.y - 620, reservationTicket[0].transform.localPosition.z);
                tutorialText.text = null;
                tutorialText.DOText(textList[7], 2f);
                break;
            case 14:
                ResetInteractable();
                for (int i = 0; i < reservationNumberBtn.Count; i++)
                {
                    reservationNumberBtn[i].interactable = false;
                }

                reservationNumberBtn[0].interactable = true;

                indicator.transform.localPosition = new Vector3(reservationNumberBtn[0].transform.localPosition.x, reservationNumberBtn[0].transform.localPosition.y - 620, reservationNumberBtn[0].transform.localPosition.z);
                tutorialText.text = null;
                tutorialText.DOText(textList[8], 2f);
                break;
            case 15:
                ResetInteractable();
                for (int i = 0; i < reservatuinNumberInput.Count; i++)
                {
                    reservatuinNumberInput[i].interactable = false;
                }

                KeypadTuTo();
                break;
            case 16:
                ResetInteractable();
                for (int i = 0; i < reservatuinNumberInput.Count; i++)
                {
                    reservatuinNumberInput[i].interactable = false;
                }

                reservatuinNumberInput[12].interactable = true;
                indicator.transform.localPosition = new Vector3(reservatuinNumberInput[12].transform.localPosition.x, reservatuinNumberInput[12].transform.localPosition.y - 220, reservatuinNumberInput[12].transform.localPosition.z);
                break;
            case 17:
                reservatuinNumberInput[12].interactable = false;
                reservatuinNumberInput[14].interactable = true;

                indicator.transform.localPosition = new Vector3(reservatuinNumberInput[14].transform.localPosition.x, reservatuinNumberInput[14].transform.localPosition.y - 350, reservatuinNumberInput[14].transform.localPosition.z);
                break;
            case 18:
                ResetInteractable();
                for (int i = 0; i < ReservedOrderListbtn.Count; i++)
                {
                    ReservedOrderListbtn[i].interactable = false;
                }

                ReservedOrderListbtn[1].interactable = true;
                indicator.transform.localPosition = new Vector3(ReservedOrderListbtn[1].transform.localPosition.x, ReservedOrderListbtn[1].transform.localPosition.y - 820, ReservedOrderListbtn[1].transform.localPosition.z);
                tutorialText.text = null;
                tutorialText.DOText(textList[9], 2f);
                break;
            case 19:
                ReservedOrderListbtn[1].interactable = false;
                ReservedOrderListbtn[3].interactable = true;
                indicator.transform.localPosition = new Vector3(ReservedOrderListbtn[3].transform.localPosition.x, ReservedOrderListbtn[3].transform.localPosition.y - 100, ReservedOrderListbtn[3].transform.localPosition.z);
                break;
            case 20:
                ReservedOrderListbtn[3].interactable = false;
                ReservedOrderListbtn[4].interactable = true;
                indicator.transform.localPosition = new Vector3(ReservedOrderListbtn[4].transform.localPosition.x, ReservedOrderListbtn[4].transform.localPosition.y - 100, ReservedOrderListbtn[4].transform.localPosition.z);
                break;
            case 21:
                ResetInteractable();
                indicator.SetActive(false);
                successPopup.SetActive(true);
                break;
        }
    }

    string num = "1234567891";
    int k = -1;

    public void KeypadTuTo()
    {
        if (k == 7)
        {
            NextTutoPage(16);
            return;
        }
        else
        {
            k++;

            for (int i = 0; i < 10; i++)
            {
                reservatuinNumberInput[i].interactable = false;
            }

            reservatuinNumberInput[int.Parse(num[k].ToString())].interactable = true;
            indicator.transform.localPosition = new Vector3(reservatuinNumberInput[int.Parse(num[k].ToString())].transform.localPosition.x,
            reservatuinNumberInput[int.Parse(num[k].ToString())].transform.localPosition.y - 220, reservatuinNumberInput[int.Parse(num[k].ToString())].transform.localPosition.z);
        }
    }
}
