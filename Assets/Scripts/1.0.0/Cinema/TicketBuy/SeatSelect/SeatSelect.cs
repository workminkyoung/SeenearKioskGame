using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SeatSelect : MonoBehaviour
{
    public GameObject headCountPopup;

    public TMP_Text adultTxt;
    public TMP_Text teenagerTxt;

    public GameObject[] noSeatArray;
    public GameObject[] seatArray;
    public List<Text> seatCurrentList;

    public int seatCount = 0;
    public int seatCurrentCount = 0;
    void Start()
    {
    }

    private void OnEnable()
    {
        CinemaPageManager.instance.ticketBuyCurrentPageID = (int)TicketBuyPage.SeatSelectPage;
        headCountPopup.SetActive(true);

        CinemaDataManager.instance.seatNumber = "";
        CinemaDataManager.instance.headCount[0] = 0;
        CinemaDataManager.instance.headCount[1] = 0;
        
        TextSet(0);
        TextSet(1);
        seatCurrentList.Clear();
    }

    private void OnDisable()
    {
        for(int i = 0; i < seatCurrentList.Count; i++)
        {
            CinemaDataManager.instance.seatNumber += seatCurrentList[i].text;

            if(seatCurrentList.Count - 1 != i)
            {
                CinemaDataManager.instance.seatNumber += ", ";
            }
        }
    }

    public void PopupConfirm()
    {
        headCountPopup.SetActive(false);
        SeatSet();
    }

    public void HeadCountPlus(int id) // id = 0 성인, id = 1 청소년
    {
        CinemaDataManager.instance.headCount[id] += 1;
        TextSet(id);
    }

    public void HeadCountMinus(int id)
    {
        for(int i = 0; i < seatArray.Length; i++)
        {
            seatArray[i].transform.GetChild(1).gameObject.SetActive(false);
        }
        seatCurrentCount = 0;

        CinemaDataManager.instance.headCount[id] -= 1;
        TextSet(id);
    }

    void TextSet(int id)
    {
        seatCount = CinemaDataManager.instance.headCount[0] + CinemaDataManager.instance.headCount[1];

        if(id == 0)
        {
            adultTxt.text = "성 인 " + CinemaDataManager.instance.headCount[id].ToString();;
        }
        else if(id == 1)
        {
            teenagerTxt.text = "청소년 " + CinemaDataManager.instance.headCount[id].ToString();;
        }
    }

    public void SeatSet()
    {
        //팝업 명수 초기 세팅
        seatCurrentCount = 0;
        TextSet(0);
        TextSet(1);

        //선택 좌석 초기화
        for(int i = 0; i < seatArray.Length; i++)
        {
            seatArray[i].transform.GetChild(1).gameObject.SetActive(false);
        }

        //예약좌석 랜덤 세팅
        for(int i = 0; i < noSeatArray.Length; i++)
        {
            int randomNum = Random.Range(0, seatArray.Length);

            while(randomNum == 34 || randomNum == 35 || randomNum == 68 || randomNum == 69)
            {
                randomNum = Random.Range(0, seatArray.Length);
            }

            noSeatArray[i].transform.position = seatArray[randomNum].transform.position;
        } 
    }

    public void SeatSelectBtn()
    {
        if(EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject.activeSelf)
        {
            EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject.SetActive(false);
            seatCurrentCount --;
            seatCurrentList.Remove(EventSystem.current.currentSelectedGameObject.transform.GetChild(2).gameObject.GetComponent<Text>());
        }
        else
        {
            if(seatCurrentCount < seatCount)
            {
                EventSystem.current.currentSelectedGameObject.transform.GetChild(1).gameObject.SetActive(true);
                seatCurrentCount ++;
                seatCurrentList.Add(EventSystem.current.currentSelectedGameObject.transform.GetChild(2).gameObject.GetComponent<Text>());
            }
        }
    }
}
